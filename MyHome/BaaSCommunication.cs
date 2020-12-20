using MyHome.Data;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome
{
    public class BaaSCommunication
    {
        public BaaSCommunication()
        {
            // Parse Initlization
            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId = Settings.ProgrammSettings.ApplicationId,
                WindowsKey = Settings.ProgrammSettings.NetKey,
                Server = Settings.ProgrammSettings.ServerURL
            });
        }

        public async Task<ObservableCollection<MessageData>> GetMessagesAsync()
        {
            var query = ParseObject.GetQuery("Message");
            IEnumerable<ParseObject> results = await query.FindAsync();
            ObservableCollection<MessageData> list = new ObservableCollection<MessageData>();
            foreach (var item in results)
            {
                string myMessage = item.Get<string>("Message");
                string myUserName = item.Get<string>("UserName");
                DateTime myDate = (DateTime) item.CreatedAt;

                MessageData myMessageData = new MessageData();
                myMessageData.Message = myMessage;
                myMessageData.Date = myDate;
                myMessageData.UserName = myUserName;

                list.Add(myMessageData);
                
            }
            return list;
        }



    }
}
