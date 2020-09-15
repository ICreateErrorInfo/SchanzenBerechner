using Berechnung;
using Berechnung.Einheiten;

using NUnit.Framework;

namespace BerechnungTests {

    [TestFixture]
    public class SchanzenBerechnungTests {

        [Test]
        public void Test1() {

            var winkel     = Winkel.FromDeg(19);
            var höhe       = Länge.FromCentimeter(16);
            var berechnung = Schanze.Create(höhe, winkel);

            Assert.That(berechnung,                    Is.Not.Null);
            Assert.That(berechnung.Höhe.Meter,         Is.EqualTo(höhe.Meter));
            Assert.That(berechnung.Absprungwinkel.Rad, Is.EqualTo(winkel.Rad));
            Assert.That(berechnung.Absprungwinkel.Deg, Is.EqualTo(winkel.Deg));
            Assert.That(berechnung.Länge.Meter,        Is.EqualTo(0.956).Within(0.001));
            Assert.That(berechnung.Radius.Meter,       Is.EqualTo(2.936).Within(0.001));
        }

    }

}