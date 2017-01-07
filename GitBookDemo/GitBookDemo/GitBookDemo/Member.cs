using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBookDemo
{
    public class Member
    {
      [JsonProperty(PropertyName="id")]
      public string ID { get; set; }

        [JsonProperty(PropertyName ="username")]
        public string username { get; set; }

        [JsonProperty(PropertyName ="password")]
        public string password { get; set; }

        [JsonProperty(PropertyName ="membername")]
        public string membername { get; set; }
    }
}
