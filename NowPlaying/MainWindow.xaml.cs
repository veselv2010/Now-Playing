﻿using System.Windows;
using System.Threading.Tasks;
using NowPlaying.ApiResponses;

namespace NowPlaying
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var browserWindow = new OAuth.BrowserWindow();
            browserWindow.ShowDialog();

            AppInfo.SpotifyAccessToken = browserWindow.ResultToken;

            this.Show();
        }

        private void ButtonDo_Click(object sender, RoutedEventArgs e)
        {
            var getTrackTask = new Task<CurrentTrackResponse>(() => Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken));
            getTrackTask.Start();
            getTrackTask.Wait();

            CurrentTrackResponse resp = getTrackTask.Result;

            if (resp == null)
                return;

            string originalTrackName = $"{resp.GetArtistsString()} - {resp.TrackName}";
            string formattedTrackName = TrackNameFormatter.FormatForWriting(originalTrackName);

            ButtonDo.Content = $"{originalTrackName} | {resp.Progress}/{resp.Duration}";
            LabelFormatted.Content = formattedTrackName;
        }
    }
}
