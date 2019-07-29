using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MangoAppsLoginApi.Model
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string api_key { get; set; }
        public string current_version { get; set; }
        public string device_id { get; set; }
        public string device_token { get; set; }
        public string client_id { get; set; }
        public string app_id { get; set; }
    }
    public class MsRequest
    {
        public User user { get; set; }
    }
    public class FormData
    {
        public MsRequest ms_request { get; set; }
    }
}
