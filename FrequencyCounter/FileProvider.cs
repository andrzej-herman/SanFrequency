using System;
using Microsoft.Extensions.Configuration;

namespace FrequencyCounter
{
	public class FileProvider
	{
        public IEnumerable<string> GetFiles()
        {
            var configuration = GetConfiguration();
            var path = configuration.GetSection("FilePath").Value;
            return Directory.GetFiles(path);
        }

        public int GetNumberOfLectures()
        {
            var configuration = GetConfiguration();
            return int.Parse(configuration.GetSection("NumberOfLectures").Value);
        }

        IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}

