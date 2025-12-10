using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Exceptions;

namespace UnitTestsKlantenSimulator;

public class UnitTestStad
{
    private Land _validLand;

    public UnitTestStad()
    {
        _validLand = new Land(1, "Belguim");
    }


    [Theory]
    [InlineData("Brussel")]
    [InlineData("New York City")]
    [InlineData("Sint-Niklaas")]
    public void SetNaamValid(string naam)
    {
        Stad stad = new Stad(1, _validLand, "Belguim");
        stad.Naam = naam;
        Assert.Equal(naam, stad.Naam);
    }

    [Fact]
    public void SetNaamInvalidEmpty()
    {
        const string OriginalName = "Gent";
        Stad stad = new Stad(1, _validLand, OriginalName);
        Assert.Throws<KlantenException>(() => { stad.Naam = ""; });
        Assert.Equal(stad.Naam, OriginalName);
    }

    [Fact]
    public void SetLandValid()
    {
        Stad stad = new Stad(1, _validLand, "Belguim");
        Land land = new Land(1, "Nederland");
        stad.Land = land;
        Assert.Equal(land, stad.Land);
    }

    [Fact]
    public void SetLandInvalidNull()
    {
        const string OriginalName = "Gent";
        Stad stad = new Stad(1, _validLand, OriginalName);
        Assert.Throws<KlantenException>(() => { stad.Land = null; });
        Assert.Equal(stad.Naam, OriginalName);
    }

    [Fact]
    public void SetNaamInvalidNull()
    {
        const string OriginalName = "Gent";
        Stad stad = new Stad(1, _validLand, OriginalName);
        Assert.Throws<KlantenException>(() => { stad.Naam = null; });
        Assert.Equal(stad.Naam, OriginalName);
    }

    [Theory]
    [InlineData("France")]
    [InlineData("UK")]
    [InlineData("Bosnia and Herzegovina")]
    public void ConstructorNaamValid(string naam)
    {
        Stad stad = new Stad(1, _validLand, naam);
        Assert.Equal(naam, stad.Naam);
    }
    [Fact]
    public void ConstructorLandValid()
    {
        Stad stad = new Stad(1, _validLand, "Oostende");
        Assert.Equal(_validLand, stad.Land);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ConstructorNaamInvalidEmpty(string naam)
    {
        Assert.Throws<KlantenException>(() => { Stad stad = new Stad(1,_validLand,naam); });
    }

    [Fact]
    public void ConstructorNaamInvalidNull()
    {
        Assert.Throws<KlantenException>(() => { Stad stad = new Stad(1, _validLand, null); });

    }

    [Fact]
    public void ConstructorLandInvalidNull()
    {
        Assert.Throws<KlantenException>(() => { Stad stad = new Stad(1, null, "Denderleeuw"); });

    }
}
