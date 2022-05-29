using System;
namespace FrequencyCounter
{
	public class DataProvider
	{
		public Dictionary<string, List<DateTime>> GetData(IEnumerable<string> files, out string[] invalidFiles)
        {
            var errors = new List<string>();
			var result = new Dictionary<string, List<DateTime>>();
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
                            var dateString = lineData[2].Split(",");
                            var date = Convert.ToDateTime(dateString[0]);
                            result.Add(lineData[0].Trim(), new List<DateTime> { date });
                        }
                        else
                        {
                            var dateString = lineData[2].Split(",");
                            var date = Convert.ToDateTime(dateString[0]);
                            if (!result[lineData[0].Trim()].Contains(date))
                                result[lineData[0].Trim()].Add(date);
                        }
                    }
                }
            }

            invalidFiles = errors.ToArray();
            return result;
        }

        public List<DateTime> GetReferenceDates(Dictionary<string, List<DateTime>> data, int numberOfMeetings)
        {
            var candidate = data.Select(x => x.Value).FirstOrDefault(x => x.Count() == numberOfMeetings);
            if (candidate == null) return null;
            return candidate.OrderBy(x => x).ToList();
        }

        public List<FrequencyItem> GetFrequencyRaport(Dictionary<string, List<DateTime>> data, List<DateTime> referenceDates)
        {
            var totalMeetings = referenceDates.Count;
            int id = 1;
            return data.Select(x => new FrequencyItem
            {
                Id = id++,
                Student = x.Key,
                NumberOfLectures = $"{x.Value.Count}/{totalMeetings}",
                PresenceRate = (Convert.ToDouble(x.Value.Count) / Convert.ToDouble(totalMeetings)) * 100.0,
                Absences = referenceDates.Where(r => !x.Value.Contains(r)).ToList(),
            }).ToList();
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

