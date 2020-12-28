﻿using NowPlaying.Core.Api;
using NowPlaying.Core.Config;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.InputSender;
using NowPlaying.Core.Steam;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace NowPlaying.Wpf
{
    public partial class MainWindow : Window
    {
        private ISteamService _steamService;
        private PathResolver _pathResolver;
        private ConfigWriter _configWriter;
        private GameProcess _gameProcess;
        private IInputSender _keySender;
        private SteamContext _userContext;
        private SpotifyRequestsManager _spotify;
        private IDictionary<string, int> _accounts;

        private Timer _trackUpdateTimer;
        public MainWindow()
        {
            InitializeComponent();

            HeaderBlock.CloseButton.MouseLeftButtonDown += 
                new MouseButtonEventHandler((o, sender) => Application.Current.Shutdown());
            HeaderBlock.CollapseButton.MouseLeftButtonDown += 
                new MouseButtonEventHandler((o, s) => WindowState = WindowState.Minimized);

            _spotify = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
                "29bd9ec2676c4bf593f3cc2858099838", @"http://localhost:8888/");
            _steamService = new SteamServiceWindows();
            _pathResolver = new PathResolver();
            _keySender = new InputSenderWindows();
            _userContext = _steamService.GetSteamContext();

            _accounts = _userContext.GetAccounts();

            _gameProcess = new GameProcess();
            _gameProcess.Start();

            int steamid3 = _accounts[_userContext.LastAccount];
            string writePath = _pathResolver.GetWritePath(_gameProcess.CurrentProcess, _userContext.UserdataPath, steamid3.ToString());
            _configWriter = new ConfigWriter(writePath);

            _trackUpdateTimer = new Timer(1000);
            _trackUpdateTimer.AutoReset = true;
            _trackUpdateTimer.Elapsed += UpdateTrackInfo;

            ConsolePaste.Text = "bind \"key\" \"exec audio.cfg\"";
            UserSettingsBlock.CurrentAccountText.Text = _userContext.LastAccount;
        }

        private string lastTrackId;
        private async void UpdateTrackInfo(object sender, ElapsedEventArgs e)
        {
            var currentTrack = await _spotify.GetCurrentTrack();

            if (currentTrack == null)
                return;

            PlayingTrackControl.Update(currentTrack);

            string gameName = _gameProcess.CurrentProcess?.WindowName ?? "";
            UserSettingsBlock.Update(_userContext.LastAccount, gameName);

            if (lastTrackId == currentTrack.Id)
                return;

            BackgroundCover.Update(currentTrack.CoverUrl);
            lastTrackId = currentTrack.Id;

            if (!_gameProcess.IsValid)
                return;

            _configWriter.RewriteKeyBinding(currentTrack);

            if(UserSettingsBlock.AutosendCheck.IsToggled)
                _keySender.SendSystemInput(UserSettingsBlock.CurrentVirtualKey);
        }

        private async Task<string> AskCode()
        {
            using (var auth = new AuthServer(@"http://localhost:8888/"))
            {
                string authUrl = _spotify.GetAuthUrl().Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {authUrl}") { CreateNoWindow = true });
                return await auth.GetAuthCode();
            }
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            AcrylicMaterial.EnableBlur(this);
            this.Hide();
            string code = await AskCode();

            if (code == default)
                Application.Current.Shutdown();

            await _spotify.StartTokenRequests(code);
            _trackUpdateTimer.Start();
            this.Show();
        }

        private void HeaderBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OnMouseLeftButtonDown(e);
                DragMove();
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _trackUpdateTimer.Stop();
            _trackUpdateTimer.Dispose();

            _spotify.Dispose();
        }
    }
}
