

//DataDefinieren
using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DomeinControlers;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorInitialisationUtils.DTO.ConstructorStraatJsonLezerDTO;
using KlantenSimulatorInitialisationUtils.DTO.NaamConstructerInfo;
using KlantenSimulatorInitialisationUtils.DTO.StadConstructorInfo;
using KlantenSimulatorUtils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

string databankpad = @"Data Source=ROWALD\SQLEXPRESS; Initial Catalog = klantenGenerator; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False";


Console.WriteLine("Wat is de naam van het Land?");
string landnaam = Console.ReadLine();

Console.WriteLine("Wat is de naam van de dataset?");
string datasetnaam = Console.ReadLine();



IConstructorStreetLezerDTO StratenPad = VraagWegen();

bool Achternaamvraagbeantwoord = false;
bool zijnAchternamenOnzijdig= false;
while (Achternaamvraagbeantwoord == false)
{
    Console.WriteLine("Zijn de achternamen onzijdig? [y/n]");
    string antwoord = Console.ReadLine().ToLower();

    switch (antwoord)
    {
        case "y":
            zijnAchternamenOnzijdig = true;
            Achternaamvraagbeantwoord = true;
            break;
        case "n":
            zijnAchternamenOnzijdig = false;
            Achternaamvraagbeantwoord = true;
            break;
            default:
            Console.WriteLine("Alleen antwoorden met y of n");break;
    }

}
InitialisationFactory factory;

if (zijnAchternamenOnzijdig)
{
    IConstructorNaamLezerDTO voornaamM = Stelvraag("mannen voornamen");
    IConstructorNaamLezerDTO voornaamV = Stelvraag("vrouwen voornamen");
    IConstructorNaamLezerDTO AchterNamen = Stelvraag("AchterNamen");


     factory = new InitialisationFactory(datasetnaam, landnaam, StratenPad, voornaamV, voornaamM, AchterNamen, databankpad);
}
else
{
    IConstructorNaamLezerDTO voornaamM = Stelvraag("mannen voornamen");
    IConstructorNaamLezerDTO AchterNamenM = Stelvraag("mannen AchterNamen");
    IConstructorNaamLezerDTO voornaamV = Stelvraag("vrouwen voornamen");
    IConstructorNaamLezerDTO AchterNamenV = Stelvraag("vrouwen AchterNamen");
 


     factory = new InitialisationFactory(datasetnaam, landnaam, StratenPad, voornaamV, voornaamM, AchterNamenV, AchterNamenM, databankpad);
}

    Initialisatieservice inService = factory.MaakDomeinControler();
 inService.SaveDataSet();
Console.WriteLine("de dataset zit in het systeem");
//LandDataSetDTO data = inService.GeefDataSetDTO();

//Console.WriteLine($"dataset has {data.VoornamenV.Count()} vrouwenNamen, {data.VoornamenM.Count()} mannenNamen, {data.AchternamenV.Count()} achterNamenV ,, {data.AchternamenM.Count()} achterNamenM, , {data.StadDTOs.Count()} Steden");
//inService.SaveDataSet();



  IConstructorNaamLezerDTO Stelvraag(string vraagobject)
    {
        try
        {
            Console.WriteLine($"Geef het pad naar de {vraagobject}");
            string pad = Console.ReadLine().ToLower();
            if (pad.EndsWith(".txt") || pad.EndsWith(".csv"))
            {
                Console.WriteLine($"In welke colom staan de {vraagobject}");
                int colom = int.Parse(Console.ReadLine().ToLower());

                Console.WriteLine($"In welke colom staat het aantal {vraagobject}");
                int freccolom = int.Parse(Console.ReadLine().ToLower());

                Console.WriteLine($"Vanaf welke rij beginnen de {vraagobject}");
                int row = int.Parse(Console.ReadLine().ToLower());

                Console.WriteLine($"welk karakter is gebruikt als seperator");
                char seperator = char.Parse(Console.ReadLine().ToLower());

                return new ConstructorNaamTextLezerDTO(pad, colom, freccolom, row, seperator);
            }
            else if (pad.EndsWith(".json"))
            {
                Console.WriteLine($"geef de punt notatie van de {vraagobject} binnen de Json (parameter1.parameter2.parameter3)");
                string puntnotatie = Console.ReadLine();
                return new ConstructorNaamJsonLezerDTO(pad, puntnotatie);
            }
            throw new Exception($"pad has to end on .json , .txt or .csv");
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return Stelvraag(vraagobject);
        }
    }

IConstructorStreetLezerDTO VraagWegen()
{
    try
    {
        Console.WriteLine($"Geef het pad naar de wegen");
        string pad = Console.ReadLine().ToLower();
        if (pad.EndsWith(".txt") || pad.EndsWith(".csv"))
        {

            return new ConstructorStraatTXTLezerDTO(pad);
        }
        else if (pad.EndsWith(".json"))
        {
            Console.WriteLine($"geef de punt notatie van de STRATEN binnen de Json (parameter1.parameter2.parameter3)");
            string puntnotatiestraten = Console.ReadLine();
            Console.WriteLine($"geef de punt notatie van de STEDEN binnen de Json (parameter1.parameter2.parameter3)");
            string puntnotatiesteden = Console.ReadLine();
            return new ConstructorStraatJsonLezerDTO(pad, puntnotatiesteden, puntnotatiestraten);
        }
        throw new Exception($"pad has to end on .json , .txt or .csv");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return VraagWegen();
    }
}