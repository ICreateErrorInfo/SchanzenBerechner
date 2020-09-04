using Berechnung;

using NUnit.Framework;

namespace BerechnungTests {

    [TestFixture]
    public class SchanzenBerechnungTests {

        [Test]
        public void Test1() {

            var winkel     = Winkel.FromDeg(19);
            var höhe       = 0.16;
            var berechnung = Schanze.Create(höhe, winkel);

            Assert.That(berechnung,                    Is.Not.Null);
            Assert.That(berechnung.Höhe,               Is.EqualTo(höhe));
            Assert.That(berechnung.Absprungwinkel.Rad, Is.EqualTo(winkel.Rad));
            Assert.That(berechnung.Absprungwinkel.Deg, Is.EqualTo(winkel.Deg));
            Assert.That(berechnung.Länge,              Is.EqualTo(0.956).Within(0.001));
            Assert.That(berechnung.Radius,             Is.EqualTo(2.936).Within(0.001));
        }

    }

}