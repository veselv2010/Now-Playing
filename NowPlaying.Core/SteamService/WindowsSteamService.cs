﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace NowPlaying.Core.SteamService
{
    class WindowsSteamService : SteamAccountInfo, ISteamService
    {
        private const string steamRegistryPath = @"HKEY_CURRENT_USER\Software\Valve\Steam";
        public string GetSteamFullPath()
        {
            var path = Registry.GetValue(steamRegistryPath, "SteamPath", "") as string;

            if (string.IsNullOrEmpty(path))
                throw new DirectoryNotFoundException("Unable to locate the steam folder");

            return path;
        }

        public string GetSteamLastAccount()
        {
            var account = Registry.GetValue(steamRegistryPath, "AutoLoginUser", "") as string;

            if (string.IsNullOrEmpty(account))
                throw new DirectoryNotFoundException("Unable to locate last logged-on account");

            return account;
        }
    }
}
