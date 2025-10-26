using System.Diagnostics;

namespace ProcessFileService.Service;

/// <summary>
/// Provides helper methods to run FFmpeg for transcoding and generating HLS outputs.
/// </summary>
public static class FfmpegService
{
    /// <summary>
    /// Represents a video quality variant used for multi-bitrate HLS.
    /// </summary>
    public record Quality(string Name, int Width, int Height, int Bitrate);

    /// <summary>
    /// Preset resolutions and bitrates used for generating HLS variants.
    /// </summary>
    public static readonly Quality[] Qualities =
    [
        new("1080p", 1920, 1080, 5000000),
        new("720p", 1280, 720, 3000000),
        new("480p", 854, 480, 1500000),
        new("360p", 640, 360, 800000),
    ];

    /// <summary>
    /// Converts any input video to a standard MP4 with H.264/AAC encoding.
    /// </summary>
    /// <param name="input">Path to input video file.</param>
    /// <param name="output">Destination output file path.</param>
    public static void ConvertToMp4(string input, string output)
    {
        string args = $"-i \"{input}\" -c:v libx264 -crf 23 -preset fast -c:a aac -strict -2 \"{output}\"";
        RunFfmpeg(args);
    }

    /// <summary>
    /// Generates multiple HLS variants (480p/720p/etc.) and a master playlist for adaptive streaming.
    /// </summary>
    /// <param name="input">Input video file.</param>
    /// <param name="outputDir">Directory where HLS files will be generated.</param>
    public static void GenerateMultiBitrateHls(string input, string outputDir)
    {
        Directory.CreateDirectory(outputDir);

        var variantPlaylists = new List<Quality>();

        foreach (var quality in Qualities)
        {
            string variantOutput = Path.Combine(outputDir, quality.Name);
            Directory.CreateDirectory(variantOutput);

            string variantM3u8 = Path.Combine(variantOutput, "index.m3u8");

            string args = $"-i \"{input}\" -vf scale={quality.Width}:{quality.Height} -c:a aac -ar 48000 -c:v h264 -profile:v main " +
                          $"-crf 20 -sc_threshold 0 -g 48 -keyint_min 48 -b:v {quality.Bitrate} -maxrate {quality.Bitrate} -bufsize {quality.Bitrate * 2} " +
                          $"-hls_time 10 -hls_playlist_type vod -f hls -hls_segment_filename \"{variantOutput}/%03d.ts\" " +
                          $"\"{variantM3u8}\"";

            RunFfmpeg(args);
            variantPlaylists.Add(new(quality.Name, quality.Width, quality.Height, quality.Bitrate));
        }

        string masterPlaylistPath = Path.Combine(outputDir, "master.m3u8");
        GenerateMasterPlaylist(masterPlaylistPath, variantPlaylists);
    }

    /// <summary>
    /// Writes a HLS master playlist (.m3u8) referencing all generated variant playlists.
    /// </summary>
    private static void GenerateMasterPlaylist(string path, List<Quality> variants)
    {
        using var writer = new StreamWriter(path);
        writer.WriteLine("#EXTM3U");

        foreach (var quality in variants)
        {
            writer.WriteLine($"#EXT-X-STREAM-INF:BANDWIDTH={quality.Bitrate},RESOLUTION={quality.Width}x{quality.Height}");
            writer.WriteLine($"./{quality.Name}/index.m3u8");
        }
    }

    /// <summary>
    /// Runs FFmpeg synchronously and throws if it exits with a non-zero code.
    /// </summary>
    /// <param name="arguments">Command line arguments passed to FFmpeg.</param>
    /// <exception cref="Exception">Thrown if ffmpeg process fails.</exception>
    private static void RunFfmpeg(string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = arguments,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception("FFmpeg error: " + error);
        }
    }
}
