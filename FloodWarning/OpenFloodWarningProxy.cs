using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FloodWarning
{

    //Creating the call to the API

    public class OpenFloodWarningProxy
    {
        //Note this the string must be same as the URL query and the info in the Mainpage.xaml.cs code too.

        public async static Task<RootObject> GetFloodWarnings(double lat, double lon, int dist)
        {
            var http = new HttpClient();

            //Note that the url has a search value. This can be altered, but you must also alter the Mainpage.xaml.cs code too.

            var url = String.Format("http://environment.data.gov.uk/flood-monitoring/id/floods?lat={0}&long={1}&dist={2}", lat, lon, dist);

            var response = await http.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }


    }

    //List of the API returns in c sharp. Remember to blank out any calls that return errors.

    [DataContract]
    public class Meta
    {
        [DataMember]
        public string publisher { get; set; }

        [DataMember]
        public string licence { get; set; }

        [DataMember]
        public string documentation { get; set; }

        [DataMember]
        public string version { get; set; }

        [DataMember]
        public string comment { get; set; }

        [DataMember]
        public List<string> hasFormat { get; set; }
    }

    [DataContract]
    public class FloodArea
    {
        /*
        [DataMember]
        public string __invalid_name__@id { get; set; }
        */
        [DataMember]
        public string county { get; set; }

        [DataMember]
        public string notation { get; set; }

        [DataMember]
        public string polygon { get; set; }

        [DataMember]
        public string riverOrSea { get; set; }
    }

    [DataContract]
    public class Item
    {
        /*
        [DataMember]
        public string __invalid_name__@id { get; set; }
        */
        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string eaAreaName { get; set; }

        [DataMember]
        public string eaRegionName { get; set; }

        [DataMember]
        public FloodArea floodArea { get; set; }

        [DataMember]
        public string floodAreaID { get; set; }

        [DataMember]
        public bool isTidal { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string severity { get; set; }

        [DataMember]
        public int severityLevel { get; set; }

        [DataMember]
        public string timeMessageChanged { get; set; }

        [DataMember]
        public string timeRaised { get; set; }

        [DataMember]
        public string timeSeverityChanged { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        /*
        [DataMember]
        public string __invalid_name__@context { get; set; }
        */
        [DataMember]
        public Meta meta { get; set; }

        [DataMember]
        public List<Item> items { get; set; }
    }
}
