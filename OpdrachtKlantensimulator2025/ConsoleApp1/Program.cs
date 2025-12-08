// See https://aka.ms/new-console-template for more information
using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorDatabaseInitialiserDL;
/*Console.WriteLine("Hello, World!");
string path = @"C:\Users\rowal\Downloads\SourceData\SourceData\Finland\finland_streets2.csv";
WegTextLezer wtl = new(path,"Finland");
List<Straat> straten = wtl.GeefAlleStraten();

Console.WriteLine($"loaded {straten.Count} straten");

for(int i = 0;i<100 ; i++)
{
   Straat s = straten[i];
    Console.WriteLine($"De straat {s.Naam} in {s.Stad.Naam} in {s.Stad.Land.Naam}");
}
*/


string path = @"C:\Users\rowal\Downloads\SourceData\SourceData\Finland\etunimitilasto-2025-08-13-dvv_miehet_ens.txt";

NaamTextLezer ntl = new(path, '	', 0, 1, true, Geslacht.Man);

List<NaamInfo> namen = ntl.GeefAlleNamen();

Console.WriteLine($"loaded {namen.Count} namen (press enter)");
Console.ReadLine();

for (int i = 0; i < namen.Count&&i<100; i++)
{
    NaamInfo s = namen[i];
    Console.WriteLine($"naam {s.Naam}        count: {s.Frequency}");
}

