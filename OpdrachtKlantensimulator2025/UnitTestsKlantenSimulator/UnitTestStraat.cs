using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Exceptions;

namespace UnitTestsKlantenSimulator;


public class UnitTestStraat
{
    private Stad _validStad;
    private Land _validLand;

    public UnitTestStraat()
    {

        _validLand = new Land(1, "Belguim");
        _validStad = new Stad(1,_validLand, "Brugge");
    }


    [Theory]
    [InlineData("Noordlaan")]
    [InlineData("5de Markt")]
    [InlineData("Kerk Straat")]
    public void SetNaamValid(string naam)
    {
        Straat straat = new Straat(1, _validStad, "Belguim");
        straat.Naam = naam;
        Assert.Equal(naam, straat.Naam);
    }

    [Fact]
    public void SetNaamInvalidEmpty()
    {
        const string OriginalName = "Noordlaan";
        Straat straat = new Straat(1, _validStad, OriginalName);
        Assert.Throws<KlantenException>(() => { straat.Naam = ""; });
        Assert.Equal(straat.Naam, OriginalName);
    }

    [Fact]
    public void SetStadValid()
    {
        Straat straat = new Straat(1, _validStad, "Noordlaan");
        Stad stad = new Stad(_validLand, "Gent");
        straat.Stad = stad;
        Assert.Equal(stad, straat.Stad);
    }

    [Fact]
    public void SetStadInvalidNull()
    {
        const string OriginalName = "Noordlaan";
        Straat straat = new Straat(1, _validStad, OriginalName);
        Assert.Throws<KlantenException>(() => { straat.Stad = null; });
        Assert.Equal(straat.Naam, OriginalName);
    }

    [Fact]
    public void SetNaamInvalidNull()
    {
        const string OriginalName = "Noordlaan";
        Straat straat = new Straat(1, _validStad, OriginalName);
        Assert.Throws<KlantenException>(() => { straat.Naam = null; });
        Assert.Equal(straat.Naam, OriginalName);
    }

    [Theory]
    [InlineData("Noordlaan")]
    [InlineData("5de Markt")]
    [InlineData("Kerk Straat")]
    public void ConstructorNaamValid(string naam)
    {
        Straat straat = new Straat(1, _validStad, naam);
        Assert.Equal(naam, straat.Naam);
    }
    [Fact]
    public void ConstructorStadValid()
    {
        Straat straat = new Straat(1, _validStad, "Noordlaan");
        Assert.Equal(_validStad, straat.Stad);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ConstructorNaamInvalidEmpty(string naam)
    {
        Assert.Throws<KlantenException>(() => { Straat straat = new Straat(1, _validStad, naam); });
    }

    [Fact]
    public void ConstructorNaamInvalidNull()
    {
        Assert.Throws<KlantenException>(() => { Straat straat = new Straat(1, _validStad, null); });

    }

    [Fact]
    public void ConstructorStadInvalidNull()
    {
        Assert.Throws<KlantenException>(() => { Straat straat = new Straat(1, null, "Noordlaan"); });

    }
}
