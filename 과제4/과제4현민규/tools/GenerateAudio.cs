// Generates original PCM audio assets; no external recordings are used.
using System;
using System.IO;
public static class TorchAudioGenerator
{
    const int Rate = 22050;
    static double Sin(double hz, double t) => Math.Sin(2 * Math.PI * hz * t);
    static void Write(string path, double seconds, Func<double, double> sample)
    {
        int count = (int)(Rate * seconds);
        using var w = new BinaryWriter(File.Create(path));
        w.Write(System.Text.Encoding.ASCII.GetBytes("RIFF")); w.Write(36 + count * 2);
        w.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt ")); w.Write(16);
        w.Write((short)1); w.Write((short)1); w.Write(Rate); w.Write(Rate * 2);
        w.Write((short)2); w.Write((short)16);
        w.Write(System.Text.Encoding.ASCII.GetBytes("data")); w.Write(count * 2);
        for (int i = 0; i < count; i++)
            w.Write((short)(Math.Clamp(sample((double)i / Rate), -0.95, 0.95) * 32767));
    }
    public static void Generate(string directory)
    {
        Directory.CreateDirectory(directory);
        Write(Path.Combine(directory, "click.wav"), 0.16, t =>
            0.24 * Math.Sin(Math.PI * t / 0.16) * Math.Exp(-t * 35) *
            (Sin(760, t) + 0.35 * Sin(1520, t)));
        Write(Path.Combine(directory, "start.wav"), 0.7, t =>
            0.18 * Math.Sin(Math.PI * t / 0.7) * Math.Exp(-t * 4) *
            (Sin(220, t) + 0.5 * Sin(330, t) + 0.3 * Sin(554, t)));
        // Integer cycles over 24 seconds allow a seamless ambient loop.
        Write(Path.Combine(directory, "dark-ambient.wav"), 24, t => {
            double swell = 0.75 + 0.25 * Sin(1.0 / 24, t);
            double drone = 0.09 * Sin(55, t) + 0.055 * Sin(55.125, t)
                + 0.035 * Sin(82.5, t) + 0.018 * Sin(116.5416666667, t);
            double air = 0.012 * Sin(311, t) * Sin(0.125, t)
                + 0.008 * Sin(466.125, t) * Sin(1.0 / 24, t);
            double bell = 0;
            double[] notes = { 440, 415.3047, 329.6276, 311.127 };
            for (int n = 0; n < notes.Length; n++) {
                double age = (t - (2 + n * 6) + 24) % 24;
                if (age < 4) bell += 0.025 * Math.Sin(Math.PI * age / 4)
                    * Math.Exp(-age * 1.1) * Sin(notes[n], age);
            }
            return drone * swell + air + bell;
        });
    }
}
