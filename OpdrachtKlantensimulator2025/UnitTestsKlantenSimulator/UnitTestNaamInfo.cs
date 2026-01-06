using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;

namespace UnitTestsKlantenSimulator;

public class UnitTestNaamInfo
{


    [Theory]
    [InlineData("Ilias")]
    [InlineData("De Winter")]
    public void SetNaamValid(string naam)
    {
        NaamInfo naamdata = new NaamInfo("Rowald",5,GramaticaalGeslacht.Man,NaamSoort.Voornaam);
        naamdata.Naam = naam;
        Assert.Equal(naam, naamdata.Naam);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("  ")]
    public void SetNaaminValid(string naam)
    {
        string beginnaam = "Rowald";
        NaamInfo naamdata = new NaamInfo(beginnaam, 5, GramaticaalGeslacht.Man, NaamSoort.Voornaam);
        Assert.Throws<KlantenException>(() => { naamdata.Naam = naam; });
        Assert.Equal(beginnaam, naamdata.Naam);
    }


    [Theory]
    [InlineData(1)]
    [InlineData(525)]
    public void SetFrequencyValid(int frequency)
    {
        NaamInfo naamdata = new NaamInfo("Rowald", 5, GramaticaalGeslacht.Man, NaamSoort.Voornaam);
        naamdata.Frequency = frequency;
        Assert.Equal(frequency, naamdata.Frequency);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-80)]
    public void SetFrequencyinValid(int frequency)
    {
        int beginwaarde = 5;
        NaamInfo naamdata = new NaamInfo("Rowald", beginwaarde, GramaticaalGeslacht.Man, NaamSoort.Voornaam);
        Assert.Throws<KlantenException>(() => { naamdata.Frequency = frequency; });
        Assert.Equal(beginwaarde, naamdata.Frequency);
    }

    [Theory]
    [InlineData("Rowald",5,GramaticaalGeslacht.Man,NaamSoort.Voornaam)]
    [InlineData("Pissens", 20, GramaticaalGeslacht.Onzijdig, NaamSoort.AchterNaam)]
    [InlineData("Dudayev", 200, GramaticaalGeslacht.Vrouw, NaamSoort.AchterNaam)]
    public void constructorValid(string naam,int frequency, GramaticaalGeslacht geslacht, NaamSoort naamSoort )
    {
        NaamInfo naamdata = new NaamInfo(naam, frequency, geslacht, naamSoort);
        Assert.Equal(naam, naamdata.Naam);
        Assert.Equal(frequency, naamdata.Frequency);
        Assert.Equal(geslacht, naamdata.Geslacht);
        Assert.Equal(naamSoort, naamdata.NaamSoort);
    }

    [Theory]
    [InlineData(" ", 5, GramaticaalGeslacht.Man, NaamSoort.Voornaam)]
    [InlineData(null, 20, GramaticaalGeslacht.Onzijdig, NaamSoort.AchterNaam)]
    [InlineData("Dudayev", 0, GramaticaalGeslacht.Vrouw, NaamSoort.AchterNaam)]
    [InlineData("Dudayev", -5, GramaticaalGeslacht.Vrouw, NaamSoort.AchterNaam)]
    public void constructorInValid(string naam, int frequency, GramaticaalGeslacht geslacht, NaamSoort naamSoort)
    {
        Assert.Throws<KlantenException>(() =>
        {
            NaamInfo naamdata = new NaamInfo(naam, frequency, geslacht, naamSoort);
        });
    }
}
