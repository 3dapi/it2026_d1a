// -------------------------------------------------------------------------------------------------------------------------------------------------------------
// Author: 3dapi (https://github.com/3dapi)
// -------------------------------------------------------------------------------------------------------------------------------------------------------------
using NAudio.Wave;
using Vortice.XAudio2;

class G2AudioMp3 : IDisposable
{
	// MP3 파일 데이터를 저장하는 내부 클래스.
	private class Mp3Data
	{
		public Vortice.Multimedia.WaveFormat Format = null!;
		public Vortice.XAudio2.AudioBuffer Buffer = null!;
		public int Count;
	}
	private static readonly Dictionary<string, Mp3Data> SoundList = new();

	public string FilePath { get; }

	// MP3 파일 데이터와 관련된 필드.
	private Mp3Data? _mp3Data;
	private readonly Vortice.XAudio2.AudioBuffer _playBuffer;
	private readonly IXAudio2SourceVoice _sourceVoice;

	public G2AudioMp3(string filePath)
	{
		if (string.IsNullOrWhiteSpace(filePath))
		{
			throw new ArgumentException("G2AudioMp3::파일 경로가 비어 있습니다.", nameof(filePath));
		}
		var audio = G2AudioContext.Instance?.Audio ?? throw new InvalidOperationException("G2AudioMp3::G2AudioContext instance is not initialized.");
		FilePath = G2Util.FindFilePath(filePath);
		if (SoundList.TryGetValue(FilePath, out Mp3Data? mp3Data))
		{
			mp3Data.Count++;
			_mp3Data = mp3Data;
		}
		else
		{
			if (!File.Exists(FilePath))
			{
				throw new FileNotFoundException($"G2AudioMp3::{FilePath} 파일을 찾을 수 없습니다.");
			}
			using Mp3FileReader reader = new(FilePath);
			int dataLength = checked((int)reader.Length);
			byte[] pcmData = new byte[dataLength];
			int offset = 0;
			while (offset < pcmData.Length)
			{
				int read = reader.Read(pcmData, offset, pcmData.Length - offset);
				if (read == 0)
				{
					break;
				}
				offset += read;
			}
			if (offset != pcmData.Length)
			{
				Array.Resize(ref pcmData, offset);
			}
			Vortice.Multimedia.WaveFormat format = new(reader.WaveFormat.SampleRate, reader.WaveFormat.BitsPerSample,reader.WaveFormat.Channels);
			_mp3Data = new Mp3Data
			{
				Format = format,
				Buffer = new Vortice.XAudio2.AudioBuffer(pcmData),
				Count = 1
			};
			SoundList.Add(FilePath, _mp3Data);
		}
		_playBuffer = new Vortice.XAudio2.AudioBuffer(_mp3Data.Buffer.AudioDataPointer, _mp3Data.Buffer.AudioBytes, _mp3Data.Buffer.Flags);
		_sourceVoice = audio.CreateSourceVoice(_mp3Data.Format);
	}

	public void Play(bool isLooping = true)
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
		_mp3Data!.Count--;
		if (_mp3Data.Count == 0)
		{
			_mp3Data.Buffer.Dispose();
			SoundList.Remove(FilePath);
		}
		_mp3Data = null;
	}
}
