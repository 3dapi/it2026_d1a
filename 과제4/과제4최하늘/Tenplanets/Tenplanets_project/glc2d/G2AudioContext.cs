using Vortice.XAudio2;

class G2AudioContext : IDisposable
{
    public static G2AudioContext? Instance { get; private set; }

    public IXAudio2? Audio { get; }
    public IXAudio2MasteringVoice? MasteringVoice { get; }

    public G2AudioContext()
    {
        if (Instance != null)
        {
            throw new InvalidOperationException(
                "G2AudioContext instance already exists.");
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

    protected bool IsAudioOutputAvailable()
    {
        try
        {
            using var enumerator =
                new NAudio.CoreAudioApi.MMDeviceEnumerator();

            var devices = enumerator.EnumerateAudioEndPoints(
                NAudio.CoreAudioApi.DataFlow.Render,
                NAudio.CoreAudioApi.DeviceState.Active);

            return devices.Count > 0;
        }
        catch
        {
            return false;
        }
    }
}