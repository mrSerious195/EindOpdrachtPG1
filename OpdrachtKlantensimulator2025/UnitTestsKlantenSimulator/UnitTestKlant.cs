using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;

namespace UnitTestsKlantenSimulator;

public class UnitTestKlant
{
    private Adress _validAdress;

    public UnitTestKlant()
    {
        Land land = new Land(1, "Belgie");
        Stad stad = new Stad(1, land, "Gent");
        Straat straat = new Straat(stad, "Noordlaan");
        _validAdress = new Adress("15A", straat);
    }

    [Theory]
    [InlineData("Rowald")]
    [InlineData("X1S&")]
    [InlineData("Bosnia and Herzegovina")]
    public void SetVoorNaamValid(string naam)
    {
        Klant klant = new Klant("Rowald", "Pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        klant.VoorNaam = naam;
        Assert.Equal(naam, klant.VoorNaam);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void SetVoorNaamInvalid(string naam)
    {
        string beginnaam = "Rowald";
        Klant klant = new Klant(beginnaam, "Pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        Assert.Throws<KlantenException>(() => { klant.VoorNaam = naam; });
        Assert.Equal(beginnaam, klant.VoorNaam);
    }
    [Theory]
    [InlineData("Janssens")]
    [InlineData("X1S&")]
    [InlineData("de Bakker")]
    public void SetFamilienaamValid(string naam)
    {
        Klant klant = new Klant("Rowald", "Pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        klant.Familienaam = naam;
        Assert.Equal(naam, klant.Familienaam);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void SetFamilienaaminValid(string naam)
    {
        string beginnaam = "Pissens";
        Klant klant = new Klant("Rowald", beginnaam, _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        Assert.Throws<KlantenException>(() => { klant.Familienaam = naam; });
        Assert.Equal(beginnaam, klant.Familienaam);
    }

    [Fact]
    public void SetAdressValid()
    {
        Land land = new Land(1, "Belgie");
        Stad stad = new Stad(1, land, "Gent");
        Straat straat = new Straat(stad, "Noordlaan");
        Adress secondvalidAdress = new Adress("15A", straat);

        Klant klant = new Klant("Rowald", "Pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        klant.Adress = secondvalidAdress;
        Assert.Equal(secondvalidAdress, klant.Adress);
    }

    [Fact]
    public void SetAdressInvalid()
    {
        Klant klant = new Klant("Rowald", "Pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));

        Assert.Throws<KlantenException>(() => { klant.Adress = null; });
        Assert.Equal(_validAdress, klant.Adress);
    }

    [Fact]
    public void SetGeslachtValid()
    {
        Klant klant1 = new Klant("Rowald", "Pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        Klant klant2 = new Klant("Innes", "Pissens", _validAdress, Geslacht.Vrouw, DateTime.Now.AddYears(-20));
        klant1.Geslacht = Geslacht.Vrouw;
        Assert.Equal(Geslacht.Vrouw, klant1.Geslacht);
        klant2.Geslacht = Geslacht.Man;
        Assert.Equal(Geslacht.Man, klant2.Geslacht);
    }

    [Fact]
    public void SetGeboortedatumValidNow()
    {

        Klant klant = new Klant("Rowald", "pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        DateTime now = DateTime.Now;
        klant.GeboorteDatum = now;
        Assert.Equal(now, klant.GeboorteDatum);
    }

    [Fact]
    public void SetGeboortedatumValid()
    {

        Klant klant = new Klant("Rowald", "pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(-20));
        DateTime now = DateTime.Now.AddYears(-50);
        klant.GeboorteDatum = now;
        Assert.Equal(now, klant.GeboorteDatum);
    }

    [Fact]
    public void SetGeboortedatuminValidFuture()
    {
        DateTime begindatum = DateTime.Now.AddYears(-20);
        Klant klant = new Klant("Rowald", "pissens", _validAdress, Geslacht.Man, begindatum);
        DateTime futureTime = DateTime.Now.AddYears(50);
        Assert.Throws<KlantenException>(() =>
        {
            klant.GeboorteDatum = futureTime;
        });
        Assert.Equal(begindatum, klant.GeboorteDatum);
    }

    [Fact]
    public void SetGeboortedatuminValidDefault()
    {
        DateTime begindatum = DateTime.Now.AddYears(-20);
        Klant klant = new Klant("Rowald", "pissens", _validAdress, Geslacht.Man, begindatum);
        Assert.Throws<KlantenException>(() =>
        {
            klant.GeboorteDatum = default;
        });
        Assert.Equal(begindatum, klant.GeboorteDatum);
    }

    [Theory]
    [InlineData("Rowald", "pissens", Geslacht.Man)]
    [InlineData("Vanessa", "Janssens", Geslacht.Vrouw)]
    public void ConstructorValid(string voornaam, string achternaam, Geslacht geslacht)
    {
        Klant klant = new Klant(voornaam, achternaam, _validAdress, geslacht, DateTime.Now.AddYears(-20));
        Assert.Equal(voornaam, klant.VoorNaam);
        Assert.Equal(achternaam, klant.Familienaam);
        Assert.Equal(geslacht, klant.Geslacht);
    }

    [Theory]
    [InlineData(" ", "pissens", Geslacht.Man)]
    [InlineData(null, "Janssens", Geslacht.Vrouw)]
    [InlineData("Rowald", " ", Geslacht.Man)]
    [InlineData("Vanessa", null, Geslacht.Vrouw)]
    public void ConstructorinValid(string voornaam, string achternaam, Geslacht geslacht)
    {

        Assert.Throws<KlantenException>(() =>
        {
            Klant klant = new Klant(voornaam, achternaam, _validAdress, geslacht, DateTime.Now.AddYears(-20));
        });
    }

    [Fact]
    public void ConstructorinValidTime()
    {
        Assert.Throws<KlantenException>(() =>
        {
            Klant klant1 = new Klant("Rowald", "Pissens", _validAdress, Geslacht.Man, DateTime.Now.AddYears(20));
        });
        Assert.Throws<KlantenException>(() =>
        {
            Klant klant1 = new Klant("Rowald", "Pissens", _validAdress, Geslacht.Man, default);
        });
    }

    [Fact]
    public void ConstructorinValidAdressIsNull()
    {
        Assert.Throws<KlantenException>(() =>
        {
            Klant klant1 = new Klant("Rowald", "Pissens", null, Geslacht.Man, DateTime.Now.AddYears(-20));
        });
       
    }
}
  
