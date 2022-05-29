using FrequencyCounter;
using System.Diagnostics;

Console.WriteLine("Pobieranie plików ...");
var fileProvider = new FileProvider();
var files = fileProvider.GetFiles();
Console.WriteLine("Odczyt plików");
var dataProvider = new DataProvider();
var data = dataProvider.GetData(files, out string[] invalidFiles).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value); ;
if(invalidFiles.Any())
{
    Console.WriteLine("Znaleziono nieprawidłowe pliki:");
    Console.WriteLine(String.Join(",", invalidFiles));
    return;
}
Console.WriteLine("Analizowanie danych o frekwencji");
var numberOfLectures = fileProvider.GetNumberOfLectures();
var referenceDates = dataProvider.GetReferenceDates(data, numberOfLectures);
var report = dataProvider.GetFrequencyRaport(data, referenceDates);
Console.WriteLine("Zapis pliku z frekwencją");
if (!fileProvider.SaveReport(report, out string file, out string error))
    Console.WriteLine($"Wystapił problem z zapisem pliku: {error}");
else
{
    Console.WriteLine("Plik został pomyślnie zapisany");
}

Console.ReadLine();
    