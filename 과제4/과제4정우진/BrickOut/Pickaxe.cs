// 입력과 렌더링에서 분리한 곡괭이 조작 상태입니다.
sealed class Pickaxe
{
	public const float SwingDuration = 0.28f;
	private float _swingRemaining;
	private bool _strongSwing;
	public float X { get; private set; }
	public bool IsStrongMode { get; private set; }
	public bool IsSwinging => _swingRemaining > 0;
	public bool IsStrongSwing => IsSwinging && _strongSwing;
	public bool UsesStrongPose => IsStrongMode || IsStrongSwing;
	public float StrongLift => UsesStrongPose
		? (IsSwinging ? 1 - MathF.Sin((1 - _swingRemaining / SwingDuration) * MathF.PI) : 1)
		: 0;

	public Pickaxe(float initialX) => X = initialX;

	public void Update(float deltaTime, float mouseX, float width, float margin)
	{
		X = Math.Clamp(mouseX, margin, width - margin);
		_swingRemaining = Math.Max(0, _swingRemaining - Math.Max(0, deltaTime));
	}

	public bool ToggleMode()
	{
		if (IsSwinging) return false;
		IsStrongMode = !IsStrongMode;
		return true;
	}

	public bool TrySwing()
	{
		if (IsSwinging) return false;
		_strongSwing = IsStrongMode;
		IsStrongMode = false; // 헛스윙도 강타 1회를 소모합니다.
		_swingRemaining = SwingDuration;
		return true;
	}

	public float Angle
	{
		get
		{
			// 일반은 기존 강타 자세. 새 강타 이미지는 이미 I자로 세워져 있습니다.
			float restAngle = UsesStrongPose ? 0 : -MathF.PI / 4;
			if (!IsSwinging) return restAngle;
			float progress = 1 - _swingRemaining / SwingDuration;
			float arc = MathF.Sin(progress * MathF.PI) * (_strongSwing ? 0.25f : 1.0f);
			return restAngle + arc;
		}
	}
}
