// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------

using Vortice.Multimedia;
using Vortice.XAudio2;

class G2AudioSound : IDisposable
{
	// WAV 파일 데이터를 저장하는 내부 클래스.
	private class WavData
	{
		public WaveFormat Format = null!;
		public AudioBuffer Buffer = null!;
		public int Count;
	}
	private static readonly Dictionary<string, WavData> SoundList = new();

	public string FilePath { get; }

	// WAV 파일 데이터와 관련된 필드.
	private WavData? _wavData;
	private readonly AudioBuffer _playBuffer;
	private readonly IXAudio2SourceVoice _sourceVoice;

	public G2AudioSound(string filePath)
	{
		if (string.IsNullOrWhiteSpace(filePath))
		{
			throw new ArgumentException("G2AudioSound::파일 경로가 비어 있습니다.", nameof(filePath));
		}
		var audio = G2AudioContext.Instance?.Audio ?? throw new InvalidOperationException("G2AudioSound::G2AudioContext instance is not initialized.");
		FilePath = G2Util.FindFilePath(filePath);
		if (SoundList.TryGetValue(FilePath, out WavData? wavData))
		{
			wavData.Count++;
			_wavData = wavData;
		}
		else
		{
			if (!File.Exists(FilePath))
			{
				throw new FileNotFoundException($"G2AudioSound::{FilePath} 파일을 찾을 수 없습니다.");
			}
			using SoundStream stream = new SoundStream(File.OpenRead(FilePath));
			if (stream.Format == null)
			{
				throw new InvalidDataException($"G2AudioSound::{FilePath} 파일의 포맷 정보를 읽을 수 없습니다.");
			}
			using var dataStream = stream.ToDataStream();

			_wavData = new WavData
			{
				Format = stream.Format,
				Buffer = new AudioBuffer(dataStream),
				Count = 1
			};
			SoundList.Add(FilePath, _wavData);
		}
		_playBuffer = new AudioBuffer(_wavData.Buffer.AudioDataPointer, _wavData.Buffer.AudioBytes, _wavData.Buffer.Flags);
		_sourceVoice = audio.CreateSourceVoice(_wavData.Format);
	}

	public void Play(bool isLooping = false)
	{
		Stop();
		_playBuffer.LoopCount = isLooping ? (uint)XAudio2.LoopInfinite : 0;
		_sourceVoice.SubmitSourceBuffer(_playBuffer);
		_sourceVoice.Start();
	}

	public bool IsPlaying()
	{
		return _sourceVoice.State.BuffersQueued > 0;
	}

	public void Stop()
	{
		_sourceVoice.Stop();
		_sourceVoice.FlushSourceBuffers();
	}

	public void Dispose()
	{
		_sourceVoice.Dispose();
		_wavData!.Count--;
		if (_wavData.Count == 0)
		{
			_wavData.Buffer.Dispose();
			SoundList.Remove(FilePath);
		}
		_wavData = null;
	}
}
