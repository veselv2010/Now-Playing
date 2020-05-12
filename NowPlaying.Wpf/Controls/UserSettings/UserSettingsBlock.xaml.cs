﻿using ReactiveUI;
using NowPlaying.Wpf.Controls.Common;
using NowPlaying.Wpf.Controls.UserSettings.Controls;
using System.Reactive.Disposables;

namespace NowPlaying.Wpf.Controls.UserSettings
{
    public partial class UserSettingsBlockBase : ReactiveUserControl<UserSettingsBlockViewModel>
    {

    }

    public partial class UserSettingsBlock : UserSettingsBlockBase
    {
        public UserSettingsBlock()
        {
            ViewModel = new UserSettingsBlockViewModel();
            InitializeComponent();
        }
    }
}
