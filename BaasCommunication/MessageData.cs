using System;
using Windows.UI.Xaml.Media.Imaging;

namespace BaaSCommunication
{
    public class MessageData
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public BitmapImage BitmapImage { get; set; }

        public MessageData()
        {

        }
    }
}
