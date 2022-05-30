using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FrequencyCounter
{
	public class FileProvider
	{
        public IEnumerable<string> GetFiles()
        {
            var configuration = GetConfiguration();
            string path;
            var os = configuration.GetSection("OsType").Value;
            path = os == "Win" ? configuration.GetSection("FilePathWin").Value
                : configuration.GetSection("FilePathMac").Value;
            return Directory.GetFiles(path);
        }

        public int GetNumberOfLectures()
        {
            var configuration = GetConfiguration();
            return int.Parse(configuration.GetSection("NumberOfLectures").Value);
        }

        public string GetOS()
        {
            var configuration = GetConfiguration();
            return configuration.GetSection("OsType").Value;
        }

        public string GetFileType()
        {
            var configuration = GetConfiguration();
            return configuration.GetSection("FileType").Value;
        }

        IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            return builder.Build();
        }

        public bool SaveReport(List<FrequencyItem> report, out string file, out string error)
        {
            try
            {
                var destinationPath = string.Empty;
                var configuration = GetConfiguration();
                string path;
                var os = configuration.GetSection("OsType").Value;
                var fileType = configuration.GetSection("FileType").Value;
                path = os == "Win" ? configuration.GetSection("FilePathWin").Value
                    : configuration.GetSection("FilePathMac").Value;
                string[] parts;
                parts = os == "Win" ? path.Split("\\") : path.Split('/');
                var fileName = $"{parts[parts.Length - 1]}.{fileType}";
                destinationPath = os == "Win" ? $"{path}\\{fileName}" : $"{path}/{fileName}"; 
                StringBuilder sb = new StringBuilder();
                foreach (var item in report)
                    sb.AppendLine(item.ToString());

                File.WriteAllText(destinationPath, sb.ToString());         
                error = null;
                file = destinationPath;
                return true;
            }
            catch (Exception ex)
            {
                file = null;
                error = ex.Message;
                return false;
            }      
        }
    }
}

