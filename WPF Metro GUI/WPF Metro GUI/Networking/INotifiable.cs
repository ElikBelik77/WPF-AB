using Newtonsoft.Json.Linq;

namespace WPF_Metro_GUI.Networking
{
    public interface INotifiable
    {
        void Notify(JObject response, string id);
    }
}