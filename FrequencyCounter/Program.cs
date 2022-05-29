using FrequencyCounter;
Console.WriteLine("Pobieranie plików ...");
var fileProvider = new FileProvider();
var files = fileProvider.GetFiles();
Console.WriteLine("Odczyt plików");
var data = new DataProvider().GetData(files, out string[] invalidFiles).OrderBy(x => x.Key);
if(invalidFiles.Any())
{
    Console.WriteLine("Znaleziono nieprawidłowe pliki:");
    Console.WriteLine(String.Join(",", invalidFiles));
    return;
}

Console.WriteLine("Analizowanie danych o frekwencji");


