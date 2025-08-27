using System;
namespace Infrastructure
{
	public class ConfigurateSQLServer : IConfigurateSQLServer
	{
        public string ConnectionString { get; set; }
    }
    public interface IConfigurateSQLServer
    {
        string ConnectionString { get; set; }
    }
}

