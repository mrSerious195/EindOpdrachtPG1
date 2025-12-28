using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Services;
using KlantenSimulatorSimUtils;

string databankpad = @"Data Source=ROWALD\SQLEXPRESS; Initial Catalog = klantenGenerator; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False";
SimulatieService serv =KlantenSimulatieFactory.geefServise(databankpad);

SimulatieSetupDataDTO data = new(1, 18, 100, 25, 5, 100000, 0.1f, "Rowald", new());
Simulatie sim = serv.genereerSimulatie(data);


Console.WriteLine($"simuleerde {sim.Klanten.Count} klanten");
/*foreach (Klant k in sim.Klanten)
{
    Console.WriteLine($"de {k.Geslacht} {k.VoorNaam} {k.Familienaam}, wonende {k.Adress.Straat.Naam} {k.Adress.HuisNr} {k.Adress.Straat.Stad.Naam}");
}*/