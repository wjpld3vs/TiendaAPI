namespace TiendaAPI.Settings
{
    public interface ITiendaSettings
    {
        string Server { get; set; }
        string Database { get; set; }

        string Token { get; set; }
    }
}
