using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome
{
    public class BaaSCommunication
    {
        // Individual Keys
        //const string applicationId = "ZaP3heCxmOgl5ulgGosDyuG6SgHbf8b8EUfWOf0i";
        //const string serverURL = "https://parseapi.back4app.com/";
        //const string netKey = "Wz3paUpU3i2S6xBbkk3w9eAc5EchAx2NSY5MREuS";

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

        public Task<List<MessageData>> GetMessagesAsync()
        {

            return null;
            //var query = ParseObject.GetQuery("Message").WhereEqualTo("UserName", Settings.UserName);
            //IEnumerable<ParseObject> results = await query.FindAsync();
        }



    }

    public class MessageData
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public MessageData()
        {

        }
    }
}
