using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Utils
{
    internal class RequestQueryBuilder : Dictionary<string, string>
    {
        public override string ToString()
        {
            string query = null;

            for(int i = 0; i < Keys.Count; i++)
            {
                if (i == 0)
                {
                    query = String.Concat(Keys.ElementAt(i), "=", Values.ElementAt(i));
                }
                else
                {
                    query = String.Concat(query, "&", Keys.ElementAt(i), "=", Values.ElementAt(i));
                }
            }

            return query;
        }
    }
}