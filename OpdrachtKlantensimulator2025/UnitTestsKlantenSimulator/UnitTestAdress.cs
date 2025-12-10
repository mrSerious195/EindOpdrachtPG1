using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Exceptions;

namespace UnitTestsKlantenSimulator;

public class UnitTestAdress
{
   private Land _validLand;
    private Stad _validStad;
    private Straat _validStraat;

    public UnitTestAdress() {
        _validLand = new Land(1, "Belgie");
        _validStad = new Stad(1, _validLand, "Gent");
        _validStraat = new Straat(1, _validStad, "Noordlaan");
    }
    [Fact]
    public void SetStraatValid()
    {
        Adress adress = new Adress("12", _validStraat);
        Straat straat = new Straat(1, _validStad, "WestLaan");
        adress.Straat= straat;
        Assert.Equal(straat,adress.Straat);
    }
    [Fact]
    public void SetStraatInvalidNull()
    {
        Adress adress = new Adress("12", _validStraat);
        Assert.Throws<KlantenException>(() => { adress.Straat = null; });
        Assert.Equal(_validStraat, adress.Straat);
    }
    [Theory]
    [InlineData("15")]
    [InlineData("15A")]
    [InlineData("15 A")]
    public void SetHuisNummerValid(string huisnummer)
    {
        Adress adress = new Adress("12", _validStraat);
       
        adress.HuisNr = huisnummer;
        Assert.Equal(huisnummer, adress.HuisNr);
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetHuisNummerInvalid(string huisnummer)
    {
        string startNr = "5";
        Adress adress = new Adress(startNr, _validStraat);
        Assert.Throws<KlantenException>(() => { adress.HuisNr = huisnummer; });
        Assert.Equal(startNr, adress.HuisNr);
    }
    [Theory]
    [InlineData("15")]
    [InlineData("15A")]
    [InlineData("15 A")]
    public void ConstructorValid(string huisnummer)
    {
        Adress adress = new Adress(huisnummer, _validStraat);
        Assert.Equal(huisnummer, adress.HuisNr);
        Assert.Equal(_validStraat, adress.Straat);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData(null)]
    public void ConstructorInValidNaam(string huisnummer)
    {
        Assert.Throws<KlantenException>(() => { Adress adress = new Adress(huisnummer, _validStraat); });
    }
  [Fact]
    public void ConstructorInValidStraatNull()
    {
        Assert.Throws<KlantenException>(() => { Adress adress = new Adress("12", null); });
    }
}
