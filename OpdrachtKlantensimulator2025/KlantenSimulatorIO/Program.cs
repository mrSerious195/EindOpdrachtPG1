using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Services;
using KlantenSimulatorSimUtils;

string databankpad = @"Data Source=ROWALD\SQLEXPRESS; Initial Catalog = klantenGenerator; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False";
SimulatieService serv =KlantenSimulatieFactory.GeefServise(databankpad);

SimulatieSetupDataDTO data = new(1,2, 18, 100, 25, 5, 5000, 0.1f, "Ilias", new());
Simulatie sim = serv.genereerSimulatie(data);


Console.WriteLine($"simuleerde {sim.Klanten.Count} klanten");
/*foreach (Klant k in sim.Klanten)
{
    Console.WriteLine($"de {k.Geslacht} {k.VoorNaam} {k.Familienaam}, wonende {k.Adress.Straat.Naam} {k.Adress.HuisNr} {k.Adress.Straat.Stad.Naam}");
}*/