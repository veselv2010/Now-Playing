﻿using System;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace NowPlaying
{
    public class ProcessWorker
    {
        public static string GetTrackFromSpotifyTitle()
        {
            Process spotifyProcess = Process.GetProcessesByName("Spotify")
                              .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

            if (spotifyProcess == null)
                return "Spotify is not running!";

            if (string.Equals(spotifyProcess.MainWindowTitle, "Spotify", StringComparison.InvariantCultureIgnoreCase))
                return "Paused";

            return spotifyProcess.MainWindowTitle;
        }

        public static bool IsCSGORunning()
        {
            Process CSGOprocess = Process.GetProcessesByName("csgo")
                                            .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
            return (CSGOprocess != null);

        }

        public static bool IsSpotifyRunning()
        {
            Process spotifyProcess = Process.GetProcessesByName("Spotify")
                                  .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

            return (spotifyProcess != null);
        }

        public static bool IsSourceKeysFileExists()
        {
            if (File.Exists("SourceKeys.txt"))
            {
                Process.Start("SourceKeys.txt");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}