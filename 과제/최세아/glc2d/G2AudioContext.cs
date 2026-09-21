// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.XAudio2;

class G2AudioContext : IDisposable
{
	public static G2AudioContext? Instance { get; private set; }

	// XAudio2 인스턴스.
	public IXAudio2? Audio { get; }
	// XAudio2 마스터링 보이스 공유.
	public IXAudio2MasteringVoice? MasteringVoice { get; }

	public G2AudioContext()
	{
		if (Instance != null)
		{
			throw new InvalidOperationException("G2AudioContext instance already exists.");
		}
		if (IsAudioOutputAvailable())
		{
			IXAudio2? audio = null;
			IXAudio2MasteringVoice? masteringVoice = null;
			try
			{
				audio = XAudio2.XAudio2Create();
				masteringVoice = audio.CreateMasteringVoice();
				Audio = audio;
				MasteringVoice = masteringVoice;
			}
			catch
			{
				masteringVoice?.Dispose();
				audio?.Dispose();
				throw;
			}
		}
		Instance = this;
	}

	public void Dispose()
	{
		MasteringVoice?.Dispose();
		Audio?.Dispose();
		Instance = null;
	}

	// 활성화된 오디오 출력 장치 확인
	protected bool IsAudioOutputAvailable()
	{
		try
		{
			// Core Audio API 장치 열거자 생성.
			var enumerator = new NAudio.CoreAudioApi.MMDeviceEnumerator();
			// 출력(Render) 장치 중 활성화(Active) 상태 장치 리스트.
			var devices = enumerator.EnumerateAudioEndPoints(
				NAudio.CoreAudioApi.DataFlow.Render,
				NAudio.CoreAudioApi.DeviceState.Active);
			// 활성화된 스피커가 1개 이상이면 true
			return devices.Count > 0;
		}
		catch
		{
		}
		return false;
	}
}
