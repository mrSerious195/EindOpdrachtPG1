

//DataDefinieren
using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DomeinControlers;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorInitialisationUtils.DTO;
using KlantenSimulatorUtils;
using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

string databankpad = @"Data Source=ROWALD\SQLEXPRESS; Initial Catalog = klantenGenerator; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False";

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
    @"C:\Users\rowal\Downloads\SourceData\SourceData\België\Familienamen_2024_Belgie.csv",
    GramaticaalGeslacht.Onzijdig,
    NaamSoort.AchterNaam,
    1,
    2,
    0,
    ';'
    );/*

string StratenPad = @"C:\Users\rowal\Downloads\SourceData\SourceData\Polen\poland_streets2.csv";
string databankpad = @"Data Source=ROWALD\SQLEXPRESS; Initial Catalog = klantenGenerator; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False";

ConstructorNaamJsonLezerDTO voornaamM = new ConstructorNaamJsonLezerDTO(@"C:\Users\rowal\Downloads\SourceData\SourceData\Polen\pl.locale.json",
    GramaticaalGeslacht.Man,
    NaamSoort.Voornaam,
    "name.first_name_male"
    );
ConstructorNaamJsonLezerDTO voornaamV = new ConstructorNaamJsonLezerDTO(@"C:\Users\rowal\Downloads\SourceData\SourceData\Polen\pl.locale.json",
    GramaticaalGeslacht.Vrouw,
    NaamSoort.Voornaam,
    "name.first_name_female"
    );
ConstructorNaamJsonLezerDTO AchterNamen = new ConstructorNaamJsonLezerDTO(@"C:\Users\rowal\Downloads\SourceData\SourceData\Polen\pl.locale.json",
    GramaticaalGeslacht.Onzijdig,
    NaamSoort.AchterNaam,
    "name.last_name"
    );*/

InitialisationFactory factory = new InitialisationFactory("Belgie (niet polen)","Belgie", StratenPad, [voornaamM, voornaamV, AchterNamen], databankpad);

Initialisatieservice inService = factory.MaakDomeinControler();
inService.SaveDataSet();
//LandDataSetDTO data = inService.GeefDataSetDTO();

//Console.WriteLine($"dataset has {data.VoornamenV.Count()} vrouwenNamen, {data.VoornamenM.Count()} mannenNamen, {data.AchternamenV.Count()} achterNamenV ,, {data.AchternamenM.Count()} achterNamenM, , {data.StadDTOs.Count()} Steden");
//inService.SaveDataSet();

LandDataSet dataset =  inService.testrepo(1);
Console.WriteLine($"dataset has {dataset.VoornamenV.Count()} vrouwenNamen, {dataset.VoornamenM.Count()} mannenNamen, {dataset.AchternamenV.Count()} achterNamenV ,, {dataset.AchternamenM.Count()} achterNamenM, {dataset.Straten.Count()} straten ");


