using MyHome.Settings;
using System;
using Windows.UI.Xaml;

namespace MyHome.ViewModels
{
    public class MultiViewPictureShowControlViewModel : PictureShowControlViewModel
    {
        private readonly Random random;
        private readonly Random random2;
        private Visibility image1Visibility = Visibility.Collapsed;
        private Visibility image2Visibility = Visibility.Collapsed;
        private Visibility image3Visibility = Visibility.Collapsed;
        private Visibility image4Visibility = Visibility.Collapsed;
        private string selectedItem1;
        private string selectedItem2;
        private string selectedItem3;
        private string selectedItem4;
        public MultiViewPictureShowControlViewModel()
        {
            random = new Random();
            random2 = new Random();
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        public Visibility Image1Visibility
        {
            get => image1Visibility;
            set => Set(ref image1Visibility, value);
        }

        public Visibility Image2Visibility
        {
            get => image2Visibility;
            set => Set(ref image2Visibility, value);
        }

        public Visibility Image3Visibility
        {
            get => image3Visibility;
            set => Set(ref image3Visibility, value);
        }
        public Visibility Image4Visibility
        {
            get => image4Visibility;
            set => Set(ref image4Visibility, value);
        }
        public string SelectedItem1
        {
            get => selectedItem1;
            set => Set(ref selectedItem1, value);
        }

        public string SelectedItem2
        {
            get => selectedItem2;
            set => Set(ref selectedItem2, value);
        }

        public string SelectedItem3
        {
            get => selectedItem3;
            set => Set(ref selectedItem3, value);
        }

        public string SelectedItem4
        {
            get => selectedItem4;
            set => Set(ref selectedItem4, value);
        }

        // <summary>
        /// Wird ausgelöst, wenn ein Bildwechsel stattfinden soll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TimerTickAsync(object sender, object e)
        {
            if (timer.Interval.Seconds != ProgrammSettings.TimeInSecondToChangePicture)
                timer.Interval = TimeSpan.FromSeconds(ProgrammSettings.TimeInSecondToChangePicture);
            int number = random2.Next(1, 5);
            switch (number)
            {
                case 1:
                    {
                        int selectedIndex1 = random.Next(ItemsList.Count);
                        SelectedItem1 = ItemsList[selectedIndex1];
                        Image1Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        int selectedIndex2 = random.Next(ItemsList.Count);
                        SelectedItem2 = ItemsList[selectedIndex2];
                        Image2Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        int selectedIndex3 = random.Next(ItemsList.Count);
                        SelectedItem3 = ItemsList[selectedIndex3];
                        Image3Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        int selectedIndex4 = random.Next(ItemsList.Count);
                        SelectedItem4 = ItemsList[selectedIndex4];
                        Image4Visibility = Visibility.Visible;
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
