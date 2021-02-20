﻿using Windows.UI.Xaml.Controls;
using MyHome.ViewModels;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class PictureShowControlCarousel : UserControl
    {
        public SingleViewPictureShowControlViewModel ViewModel { get; set; }

        public PictureShowControlCarousel()
        {
            this.InitializeComponent();
            ViewModel = new SingleViewPictureShowControlViewModel();
        }
    }
}