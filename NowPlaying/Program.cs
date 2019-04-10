﻿using System.Windows;
using System;

namespace NowPlaying
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            SteamIdLooker.UpdateSteamConfigPaths();
            SteamIdLooker.UpdateAccountsInfo();
            new Application().Run(new MainWindow());
        }
    }
}