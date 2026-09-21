using NAudio.Wave;

internal enum BgmTrack
{
    Title,
    Battle,
    GameOver
}

internal sealed class BgmManager : IDisposable
{
    private static readonly IReadOnlyDictionary<BgmTrack, string> TrackPaths =
        new Dictionary<BgmTrack, string>
        {
            [BgmTrack.Title] = "resource/sound/BGMTitle.wav",
            [BgmTrack.Battle] = "resource/sound/BGMBattle.wav",
            [BgmTrack.GameOver] = "resource/sound/BGMGameOver.wav"
        };

    private WaveOut? _outputDevice;
    private LoopingWaveStream? _loopingStream;
    private BgmTrack? _currentTrack;

    public void Play(BgmTrack track)
    {
        if (_currentTrack == track &&
            _outputDevice?.PlaybackState == PlaybackState.Playing)
        {
            return;
        }

        Stop();

        WaveOut? outputDevice = null;
        LoopingWaveStream? loopingStream = null;

        try
        {
            AudioFileReader audioFile = new(FindFilePath(TrackPaths[track]));
            loopingStream = new LoopingWaveStream(audioFile);
            outputDevice = new WaveOut();
            outputDevice.Init(loopingStream);
            outputDevice.Play();

            _loopingStream = loopingStream;
            _outputDevice = outputDevice;
            _currentTrack = track;
        }
        catch
        {
            outputDevice?.Dispose();
            loopingStream?.Dispose();
            throw;
        }
    }

    public void Stop()
    {
        _outputDevice?.Stop();
        _outputDevice?.Dispose();
        _loopingStream?.Dispose();

        _outputDevice = null;
        _loopingStream = null;
        _currentTrack = null;
    }

    public void Dispose()
    {
        Stop();
    }

    private static string FindFilePath(string relativePath)
    {
        string currentDirectoryPath = Path.GetFullPath(relativePath);
        if (File.Exists(currentDirectoryPath))
        {
            return currentDirectoryPath;
        }

        DirectoryInfo? directory = new(AppContext.BaseDirectory);
        while (directory != null)
        {
            string candidatePath = Path.Combine(directory.FullName, relativePath);
            if (File.Exists(candidatePath))
            {
                return candidatePath;
            }

            directory = directory.Parent;
        }

        throw new FileNotFoundException(
            $"BGM 파일을 찾을 수 없습니다: {relativePath}");
    }

    private sealed class LoopingWaveStream : WaveStream
    {
        private readonly WaveStream _sourceStream;

        public LoopingWaveStream(WaveStream sourceStream)
        {
            _sourceStream = sourceStream;
        }

        public override WaveFormat WaveFormat => _sourceStream.WaveFormat;
        public override long Length => _sourceStream.Length;

        public override long Position
        {
            get => _sourceStream.Position;
            set => _sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = _sourceStream.Read(
                    buffer,
                    offset + totalBytesRead,
                    count - totalBytesRead);

                if (bytesRead == 0)
                {
                    if (_sourceStream.Position == 0)
                    {
                        break;
                    }

                    _sourceStream.Position = 0;
                    continue;
                }

                totalBytesRead += bytesRead;
            }

            return totalBytesRead;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sourceStream.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
