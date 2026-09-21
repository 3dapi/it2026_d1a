// 산소 비율(0~1)을 받아 저산소 상태가 유지된 동안만 5초 간격을 셉니다.
sealed class OxygenBreathTimer
{
	public const double IntervalSeconds = 5;
	private double _elapsed;

	public void Reset() => _elapsed = 0;

	public bool Update(double deltaTime, float oxygenRatio, bool enabled)
	{
		if (!enabled || !float.IsFinite(oxygenRatio) || oxygenRatio <= 0 || oxygenRatio > 0.2f)
		{
			Reset();
			return false;
		}
		if (!double.IsFinite(deltaTime) || deltaTime <= 0) return false;
		_elapsed += deltaTime;
		if (_elapsed < IntervalSeconds) return false;
		// 긴 프레임 뒤에도 여러 호흡을 몰아서 재생하지 않습니다.
		_elapsed = 0;
		return true;
	}
}
