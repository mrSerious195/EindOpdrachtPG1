// See https://aka.ms/new-console-template for more information
using KlantenSimulatorBL.Domein;
using KlantenSimulatorDatabaseInitialiserDL;
Console.WriteLine("Hello, World!");
string path = @"C:\Users\rowal\Downloads\SourceData\SourceData\Finland\finland_streets2.csv";
WegTextLezer wtl = new(path,"Finland");
List<Straat> straten = wtl.GeefAlleStraten();

Console.WriteLine($"loaded {straten.Count} straten");

for(int i = 0;i<100 ; i++)
{
   Straat s = straten[i];
    Console.WriteLine($"De straat {s.Naam} in {s.Stad.Naam} in {s.Stad.Land.Naam}");
}


