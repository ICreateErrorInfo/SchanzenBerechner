using Berechnung;

using NUnit.Framework;

using System;

namespace BerechnungTests {

    [TestFixture]
    public class SchanzenBerechnungTests {

        [Test]
        public void Test1() {

            var winkel     = 19;
            var rad        = winkel * Math.PI / 180;
            var höhe       = 0.16;
            var berechnung = SchanzenBerechnung.Berechne(höhe, winkel * Math.PI / 180);

            Assert.That(berechnung,                   Is.Not.Null);
            Assert.That(berechnung.Höhe,              Is.EqualTo(höhe));
            Assert.That(berechnung.AbsprungwinkelRad, Is.EqualTo(rad));
            Assert.That(berechnung.AbsprungwinkelDeg, Is.EqualTo(winkel));
            Assert.That(berechnung.Länge,             Is.EqualTo(0.956).Within(0.001));
            Assert.That(berechnung.Radius,            Is.EqualTo(2.936).Within(0.001));
        }

    }

}