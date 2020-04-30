﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace NowPlaying
{
    public class SteamIdLooker
    {
        private string _steamFullPathCached;
        private string SteamFullPath
        {
            get
            {
                if (_steamFullPathCached != null)
                    return _steamFullPathCached;

                var path = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "") as string;

                if (string.IsNullOrEmpty(path))
                    throw new DirectoryNotFoundException("Unable to locate the steam folder");

                return _steamFullPathCached = path;
            }
        }

        public string SteamLastLoggedOnAccount
        {
            get
            {
                var account = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "AutoLoginUser", "") as string;

                if (string.IsNullOrEmpty(account))
                    throw new DirectoryNotFoundException("Unable to locate last logged-on account");

                return account;
            }
        }

        public string UserdataPath => SteamFullPath + @"\userdata";

        public void UpdateAccountsInfo()
        {
            var loginUsersPath = SteamFullPath + @"\config\loginusers.vdf";

            string[] loginUsersFile = File.ReadAllLines(loginUsersPath);

            var userdataNumbers = new List<int>();

            var regexSteamId64 = new Regex(@"(765611)\d+");
            var regexAcc = new Regex(@"AccountName""\s*""(\w+)");

            for (int line = 2; line < loginUsersFile.Length - 1; line++) //id64 + userdata(steamid3/32)
            {
                var steamId64Match = regexSteamId64.Match(loginUsersFile[line]);

                if (steamId64Match.Success)
                {
                    long steamId64 = long.Parse(steamId64Match.Value);
                    int steamId32 = GetSteamId32(steamId64);

                    userdataNumbers.Add(steamId32);
                }

                var accMatch = regexAcc.Match(loginUsersFile[line]);

                if (accMatch.Success)
                {
                    AppInfo.State.AccountNames.Add(accMatch.Groups[1].Value);
                }
            }

            for (int i = 0; i < AppInfo.State.AccountNames.Count; i++)
            {
                AppInfo.State.AccountNameToSteamId3.Add(AppInfo.State.AccountNames[i], userdataNumbers[i]);
            }
        }

        private int GetSteamId32(long steamId64) //steamid64 - 76561197960265728 = steamid3/32
        {
            return (int)(steamId64 - 76561197960265728);
        }
    }
}
