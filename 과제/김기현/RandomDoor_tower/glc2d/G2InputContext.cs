// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class G2InputContext : IDisposable
{
	public static G2InputContext? Instance { get; private set; }

	public enum InputState : byte
	{
		None = 0,
		Down = 1,
		Up = 2,
		Press = 3
	}
	public PointF MousePosition => _mousePosition;
	public PointF MouseDelta => _mouseDelta;
	public int MouseWheelDelta { get; private set; }

	private const int MaxInputKey = 256;
	private const int MaxInputButton = 8;

	private readonly byte[] _keyCur = new byte[MaxInputKey];
	private readonly byte[] _keyOld = new byte[MaxInputKey];
	private readonly InputState[] _keyMap = new InputState[MaxInputKey];

	private readonly byte[] _buttonCur = new byte[MaxInputButton];
	private readonly byte[] _buttonOld = new byte[MaxInputButton];
	private readonly InputState[] _buttonMap = new InputState[MaxInputButton];

	private readonly Form _targetForm;

	private PointF _mousePosition;
	private PointF _mouseOldPosition;
	private PointF _mouseDelta;
	private int _wheelAccumulated;

	[StructLayout(LayoutKind.Sequential)]
	private struct NativePoint
	{
		public int X;
		public int Y;
	}

	[DllImport("user32.dll")]
	private static extern bool GetKeyboardState(byte[] keyState);

	[DllImport("user32.dll")]
	private static extern bool GetCursorPos(out NativePoint point);

	[DllImport("user32.dll")]
	private static extern bool ScreenToClient(IntPtr hWnd, ref NativePoint point);

	public G2InputContext(Form form)
	{
		if (Instance != null)
		{
			throw new InvalidOperationException("G2InputContext instance already exists.");
		}
		_targetForm = form ?? throw new ArgumentNullException(nameof(form));
		_targetForm.MouseWheel += OnMouseWheel;
		_mouseOldPosition = _mousePosition;
		Instance = this;
	}

	internal void Update()
	{
		UpdateKeyboard();
		UpdateMouseButton();
		UpdateMousePosition();
		MouseWheelDelta = _wheelAccumulated;
		_wheelAccumulated = 0;
	}

	private void UpdateKeyboard()
	{
		Array.Copy(_keyCur, _keyOld, MaxInputKey);
		if(GetKeyboardState(_keyCur))
		{
			for (int i = 0; i < MaxInputKey; i++)
			{
				_keyCur[i] = (_keyCur[i] & 0x80) != 0 ? (byte)1 : (byte)0;
				_keyMap[i] = GetInputState(_keyOld[i], _keyCur[i]);
			}
		}
		else
		{
			Array.Clear(_keyCur, 0, MaxInputKey);
			Array.Clear(_keyMap, 0, MaxInputKey);
		}
	}

	private void UpdateMouseButton()
	{
		Array.Copy(_buttonCur, _buttonOld, MaxInputButton);
		_buttonCur[0] = _keyCur[(int)Keys.LButton];
		_buttonCur[1] = _keyCur[(int)Keys.RButton];
		_buttonCur[2] = _keyCur[(int)Keys.MButton];
		_buttonCur[3] = _keyCur[(int)Keys.XButton1];
		_buttonCur[4] = _keyCur[(int)Keys.XButton2];
		for (int i = 0; i < MaxInputButton; i++)
		{
			_buttonMap[i] = GetInputState(_buttonOld[i],_buttonCur[i]);
		}
	}

	private void UpdateMousePosition()
	{
		_mouseDelta = PointF.Empty;
		if (!GetCursorPos(out NativePoint point))
		{
			return;
		}
		if (!ScreenToClient(_targetForm.Handle, ref point))
		{
			return;
		}
		var scaleX = G2AppBase.ScreenScaleX;
		var scaleY = G2AppBase.ScreenScaleY;

		_mousePosition = new PointF(point.X / scaleX, point.Y / scaleY);
		_mouseDelta = new PointF(_mousePosition.X - _mouseOldPosition.X, _mousePosition.Y - _mouseOldPosition.Y);
		_mouseOldPosition = _mousePosition;
	}

	private static InputState GetInputState(byte oldState, byte currentState)
	{
		if (oldState == 0 && currentState == 1)
		{
			return InputState.Down;
		}
		if (oldState == 1 && currentState == 0)
		{
			return InputState.Up;
		}
		if (oldState == 1 && currentState == 1)
		{
			return InputState.Press;
		}
		return InputState.None;
	}

	private void OnMouseWheel(object? sender, MouseEventArgs e)
	{
		_wheelAccumulated += e.Delta;
	}

	public bool IsKeyDown(Keys key)
	{
		return _keyMap[GetKeyIndex(key)] == InputState.Down;
	}

	public bool IsKeyUp(Keys key)
	{
		return _keyMap[GetKeyIndex(key)] == InputState.Up;
	}

	public bool IsKeyPress(Keys key)
	{
		return _keyMap[GetKeyIndex(key)] == InputState.Press;
	}

	public InputState KeyState(Keys key)
	{
		return _keyMap[GetKeyIndex(key)];
	}

	public bool IsButtonDown(MouseButtons button)
	{
		return _buttonMap[GetButtonIndex(button)] == InputState.Down;
	}

	public bool IsButtonUp(MouseButtons button)
	{
		return _buttonMap[GetButtonIndex(button)] == InputState.Up;
	}

	public bool IsButtonPress(MouseButtons button)
	{
		return _buttonMap[GetButtonIndex(button)] == InputState.Press;
	}

	public InputState ButtonState(MouseButtons button)
	{
		return _buttonMap[GetButtonIndex(button)];
	}

	public void Reset()
	{
		Array.Clear(_keyCur, 0, MaxInputKey);
		Array.Clear(_keyOld, 0, MaxInputKey);
		Array.Clear(_keyMap, 0, MaxInputKey);
		Array.Clear(_buttonCur, 0, MaxInputButton);
		Array.Clear(_buttonOld, 0, MaxInputButton);
		Array.Clear(_buttonMap, 0, MaxInputButton);
		MouseWheelDelta = 0;
		_wheelAccumulated = 0;
		UpdateMousePosition();
		_mouseOldPosition = _mousePosition;
		_mouseDelta = PointF.Empty;
	}

	private static int GetKeyIndex(Keys key)
	{
		return (int)(key & Keys.KeyCode);
	}

	private static int GetButtonIndex(MouseButtons button)
	{
		return button switch
		{
			MouseButtons.Left => 0,
			MouseButtons.Right => 1,
			MouseButtons.Middle => 2,
			MouseButtons.XButton1 => 3,
			MouseButtons.XButton2 => 4,
			_ => throw new ArgumentOutOfRangeException($"GetButtonIndex:: invalid:: {button} :: {nameof(button)}")
		};
	}

	public void Dispose()
	{
		_targetForm.MouseWheel -= OnMouseWheel;
		Instance = null;
	}
}
