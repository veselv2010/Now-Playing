﻿using System.Diagnostics;
using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.Header
{
    public partial class HeaderBlock : UserControl
    {
        public HeaderBlock()
        {
            InitializeComponent();
        }

        private void CloseButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();   
        }

        private void HelpTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/veselv2010/NowPlaying");
        }
    }
}
