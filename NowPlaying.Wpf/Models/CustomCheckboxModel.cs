﻿namespace NowPlaying.Wpf.Models
{
    class CustomCheckboxModel : PropertyNotifier
    {
        private bool _isToggled;
        public bool IsToggled
        {
            get
            {
                return _isToggled;
            }
            set
            {
                _isToggled = value;
                OnPropertyChanged(nameof(IsToggled));
            }
        }
    }
}
