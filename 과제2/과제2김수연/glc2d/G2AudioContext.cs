// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.XAudio2;

class G2AudioContext : IDisposable
{
	public static G2AudioContext? Instance { get; private set; }

	// XAudio2 인스턴스.
	public IXAudio2 Audio { get; }
	// XAudio2 마스터링 보이스 공유.
	public IXAudio2MasteringVoice MasteringVoice { get; }

	public G2AudioContext()
	{
		if (Instance != null)
		{
			throw new InvalidOperationException("G2AudioContext instance already exists.");
		}
		IXAudio2? audio = null;
		IXAudio2MasteringVoice? masteringVoice = null;
		try
		{
			audio = XAudio2.XAudio2Create();
			//masteringVoice = audio.CreateMasteringVoice();
			Audio = audio;
			MasteringVoice = masteringVoice;
		}
		catch
		{
			masteringVoice?.Dispose();
			audio?.Dispose();
			throw;
		}
		Instance = this;
	}

	public void Dispose()
	{
		MasteringVoice.Dispose();
		Audio.Dispose();
		Instance = null;
	}
}
