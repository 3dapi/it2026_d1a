enum GameSound
{
	Swing, StrongSwing, Mode, RockBreak, Bounce, Oxygen, Breath, Cheer, Cave
}

// 실제 게임 이벤트와 데모 미리듣기가 공통으로 사용하는 오디오입니다.
sealed class GameAudio : IDisposable
{
	private readonly Dictionary<GameSound, G2AudioSound> _sounds = new();
	private readonly OxygenBreathTimer _breathTimer = new();
	private bool _canPlay;
	private bool _hasFocus;
	public bool IsMuted { get; private set; }
	public bool IsAvailable => _sounds.Count > 0;

	public GameAudio()
	{
		if (G2AudioContext.Instance?.Audio == null) return;
		try
		{
			Load(GameSound.Swing, "sfx_swing.wav");
			Load(GameSound.StrongSwing, "sfx_swing_strong.wav");
			Load(GameSound.Mode, "sfx_mode.wav");
			Load(GameSound.RockBreak, "sfx_rock_break.wav");
			Load(GameSound.Bounce, "sfx_bounce.wav");
			Load(GameSound.Oxygen, "sfx_oxygen.wav");
			Load(GameSound.Breath, "sfx_breath.wav");
			Load(GameSound.Cheer, "sfx_cheer.wav");
			Load(GameSound.Cave, "amb_cave.wav");
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	private void Load(GameSound sound, string file) =>
		_sounds.Add(sound, new G2AudioSound($"resource/sound/{file}"));

	public void ToggleMute()
	{
		IsMuted = !IsMuted;
		_breathTimer.Reset();
		if (IsMuted)
		{
			_canPlay = false;
			StopAll();
		}
	}

	public void Update(double deltaTime, float oxygenRatio, bool isPlaying, bool hasFocus)
	{
		bool lostFocus = _hasFocus && !hasFocus;
		_hasFocus = hasFocus;
		bool enabled = IsAvailable && !IsMuted && isPlaying && hasFocus;
		if (!enabled)
		{
			if (_canPlay || lostFocus) StopAll();
			_canPlay = false;
			_breathTimer.Reset();
			return;
		}
		_canPlay = true;
		if (!_sounds[GameSound.Cave].IsPlaying()) _sounds[GameSound.Cave].Play(isLooping: true);
		if (!(oxygenRatio > 0 && oxygenRatio <= 0.2f) && _sounds[GameSound.Breath].IsPlaying())
			_sounds[GameSound.Breath].Stop();
		if (_breathTimer.Update(deltaTime, oxygenRatio, enabled)) Play(GameSound.Breath);
	}

	public void Play(GameSound sound)
	{
		// Play 밖의 결과 화면에서도 승리 효과음을 1회 재생할 수 있습니다.
		if (!_hasFocus || IsMuted || !_sounds.TryGetValue(sound, out G2AudioSound? clip)) return;
		// 연속 충돌이나 미리듣기 연타가 같은 소리를 계속 잘라내지 않게 합니다.
		if (sound is not (GameSound.Swing or GameSound.StrongSwing or GameSound.Mode) && clip.IsPlaying()) return;
		if (sound == GameSound.Oxygen)
		{
			_sounds[GameSound.Breath].Stop();
			_breathTimer.Reset();
		}
		clip.Play();
	}

	private void StopAll()
	{
		foreach (G2AudioSound sound in _sounds.Values) sound.Stop();
	}

	public void Dispose()
	{
		foreach (G2AudioSound sound in _sounds.Values) sound.Dispose();
		_sounds.Clear();
		_canPlay = false;
		_hasFocus = false;
	}
}
