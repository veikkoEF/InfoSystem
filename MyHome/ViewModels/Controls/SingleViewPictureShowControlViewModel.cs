using MyHome.Settings;
using System;

namespace MyHome.ViewModels
{
    public class SingleViewPictureShowControlViewModel : PictureShowControlViewModel
    {
        private int selectedIndex;
        private string selectedItem;
        private readonly Random random;

        public SingleViewPictureShowControlViewModel()
        {
            // Initailisierungen
            random = new Random();
        }

        /// <summary>
        /// Wird ausgelöst, wenn ein Bildwechsel stattfinden soll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void TimerTickAsync(object sender, object e)
        {
            switch (ProgrammSettings.PictureSeletionMode)
            {
                // Auswahl der Bilder der Reihe nach
                case PictureSeletionMode.ByNumber:
                default:
                    {
                        selectedIndex++;
                        if (selectedIndex > ItemsList.Count - 1)
                            selectedIndex = 0;
                    }
                    break;
                // Auswahl der Bilder nach Zufall
                case PictureSeletionMode.ByRandom:
                    {
                        selectedIndex = random.Next(ItemsList.Count);
                    }
                    break;
                // Auswahl der Bilder nach Datum
                case PictureSeletionMode.ByDate:

                    break;
            }
            if ((ItemsList != null) && (selectedIndex <= ItemsList.Count - 1))
            {
                SelectedItem = ItemsList[selectedIndex];
            }
            else
                SelectedItem = null;
        }

        public string SelectedItem
        {
            get => selectedItem;
            set => Set(ref selectedItem, value);
        }
    }
}