using System;
namespace FrequencyCounter
{
	public class DataProvider
	{
		public Dictionary<string, List<string>> GetData(IEnumerable<string> files, out string[] invalidFiles)
        {
            var errors = new List<string>();
			var result = new Dictionary<string, List<string>>();
            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);
                if (!ValidateFile(lines))
                {
                    errors.Add(Path.GetFileName(file));
                    continue;
                }

                foreach (var line in lines)
                {
                    var lineData = line.Split("\t");
                    if(ValidateLine(lineData))
                    {
                        if (!result.ContainsKey(lineData[0].Trim()))
                        {
                            var date = lineData[2].Split(",");
                            result.Add(lineData[0].Trim(), new List<string> { date[0] });
                        }
                        else
                        {
                            var date = lineData[2].Split(",");
                            if(!result[lineData[0].Trim()].Contains(date[0]))
                                result[lineData[0].Trim()].Add(date[0]);
                        }
                    }
                }
            }

            invalidFiles = errors.ToArray();
            return result;
        }

        bool ValidateFile(string[] lines)
        {
            return lines[0].StartsWith("Imię") && lines[1].StartsWith("Herman");
        }

        bool ValidateLine(string[] lineData)
        {
            return lineData.Length == 3
                && !lineData[0].StartsWith("Imię")
                && !lineData[0].StartsWith("Herman Andrzej")
                && lineData[1].StartsWith("Dołączył"); 
        }
	}
}

