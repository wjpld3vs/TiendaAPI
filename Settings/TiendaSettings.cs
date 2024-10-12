namespace TiendaAPI.Settings
{
    public class TiendaSettings : ITiendaSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Token { get; set; }
    }
}
