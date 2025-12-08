using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Exceptions;

namespace UnitTestsKlantenSimulator
{
    public class UnitTestLand
    {
        [Theory]
        [InlineData("France")]
        [InlineData("UK")]
        [InlineData("Bosnia and Herzegovina")]
        public void SetNaamValid(string naam)
        {
            Land land = new Land(1, "Belguim");
            land.Naam = naam;
            Assert.Equal(naam, land.Naam);
        }

        [Fact]
        public void SetNaamInvalidEmpty()
        {
            const string OriginalName ="Belguim";
            Land land = new Land(1, OriginalName);
            Assert.Throws<KlantenException>(() => { land.Naam = ""; });
            Assert.Equal(land.Naam, OriginalName);
        }

        [Fact]
        public void SetNaamInvalidNull()
        {
            const string OriginalName = "Belguim";
            Land land = new Land(1, OriginalName);
            Assert.Throws<KlantenException>(() => { land.Naam = null; });
            Assert.Equal(land.Naam, OriginalName);
        }

        [Theory]
        [InlineData("France")]
        [InlineData("UK")]
        [InlineData("Bosnia and Herzegovina")]
        public void ConstructorNaamValid(string naam)
        {
            Land land = new Land(1, naam);
            Assert.Equal(naam, land.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ConstructorNaamInvalidEmpty(string naam)
        {
            Assert.Throws<KlantenException>(() => { Land land = new Land(naam); });
        }

        [Fact]
        public void ConstructorNaamInvalidNull()
        {
            const string naamnull = null;
            Assert.Throws<KlantenException>(() => { Land land = new Land(naamnull); });
        
        }
    }
}
