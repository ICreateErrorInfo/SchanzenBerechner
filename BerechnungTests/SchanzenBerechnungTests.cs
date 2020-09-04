using Berechnung;

using NUnit.Framework;

namespace BerechnungTests {

    [TestFixture]
    public class SchanzenBerechnungTests {

        [Test]
        public void Test1() {

            var winkelDeg  = 19;
            var winkelRad  = Winkel.ToRad(winkelDeg);
            var höhe       = 0.16;
            var berechnung = Schanze.Create(höhe, winkelRad);

            Assert.That(berechnung,                   Is.Not.Null);
            Assert.That(berechnung.Höhe,              Is.EqualTo(höhe));
            Assert.That(berechnung.AbsprungwinkelRad, Is.EqualTo(winkelRad));
            Assert.That(berechnung.AbsprungwinkelDeg, Is.EqualTo(winkelDeg));
            Assert.That(berechnung.Länge,             Is.EqualTo(0.956).Within(0.001));
            Assert.That(berechnung.Radius,            Is.EqualTo(2.936).Within(0.001));
        }

    }

}