using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyCounter
{
    public class FrequencyItem
    {
        public int Id { get; set; }
        public string Student { get; set; }
        public string NumberOfLectures { get; set; }
        public double PresenceRate { get; set; }
        public List<DateTime> Absences { get; set; }
        public string PresenceRateString => $"{Math.Round(PresenceRate, 1)}%";
        public string AbsencesString => String.Join(", ", Absences.Select(x => x.ToString("yyyy-MM-dd")));
        public override string ToString()
        {
            return String.Join("\t", $"{Id}.", Student, $"{NumberOfLectures} ({PresenceRateString})", AbsencesString);
        }
    }
}
