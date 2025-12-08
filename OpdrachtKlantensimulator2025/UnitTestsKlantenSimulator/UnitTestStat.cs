using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Exceptions;

namespace UnitTestsKlantenSimulator;

public class UnitTestStat
{
    private Land _validLand;

    public UnitTestStat()
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
        const string naamnull = null;
        Assert.Throws<KlantenException>(() => { Stad stad = new Stad(1, _validLand,naamnull); });

    }
}
