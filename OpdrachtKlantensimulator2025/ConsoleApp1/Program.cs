

//DataDefinieren
using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DomeinControlers;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorInitialisationUtils.DTO;
using KlantenSimulatorUtils;

string StratenPad = @"C:\Users\rowal\Downloads\SourceData\SourceData\België\belgium_streets2.csv";

ConstructorNaamTextLezerDTO voornaamM = new ConstructorNaamTextLezerDTO(
    @"C:\Users\rowal\Downloads\SourceData\SourceData\België\mannennamen_belgie.csv",
    GramaticaalGeslacht.Man,
    NaamSoort.Voornaam,
    1,
    2,
    0,
    ';'
    );

ConstructorNaamTextLezerDTO voornaamV = new ConstructorNaamTextLezerDTO(
    @"C:\Users\rowal\Downloads\SourceData\SourceData\België\vrouwennamen_belgie.csv",
    GramaticaalGeslacht.Vrouw,
    NaamSoort.Voornaam,
    1,
    2,
    0,
    ';'
    );

ConstructorNaamTextLezerDTO AchterNamen = new ConstructorNaamTextLezerDTO(
    @"C:\Users\rowal\Downloads\SourceData\SourceData\België\vrouwennamen_belgie.csv",
    GramaticaalGeslacht.Onzijdig,
    NaamSoort.AchterNaam,
    1,
    2,
    0,
    ';'
    );

InitialisationFactory factory = new InitialisationFactory("Belgie", StratenPad, [voornaamM, voornaamV, AchterNamen]);

InitialisatieDC dc = factory.MaakDomeinControler();

LandDataSet data = dc.GetDataSet();

Console.WriteLine($"dataset has {data.VoornamenV.Count} vrouwenNamen, {data.VoornamenM.Count} mannenNamen, {data.AchterNamenV.Count} achterNamenV ,, {data.AchterNamenM.Count} achterNamenM, , {data.Straten.Count} Straten");


