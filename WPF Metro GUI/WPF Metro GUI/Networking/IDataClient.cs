using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Metro_GUI.Networking
{
    public interface IDataClient
    {
        string PostRequest(INotifiable sender, JObject request);
    }
}
