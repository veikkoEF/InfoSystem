using MyHome.Data;
using Parse;
using System.Collections.Generic;
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

        public Task<List<MessageData>> GetMessagesAsync()
        {

            return null;
            //var query = ParseObject.GetQuery("Message").WhereEqualTo("UserName", Settings.UserName);
            //IEnumerable<ParseObject> results = await query.FindAsync();
        }



    }
}
