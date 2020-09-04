using System;

namespace Berechnung {

    public class Schanze {

        Schanze(double länge, double höhe, double absprungwinkelRad, double radius) {
            Länge             = länge;
            Höhe              = höhe;
            AbsprungwinkelRad = absprungwinkelRad;
            Radius            = radius;

        }

        public double Länge             { get; }
        public double Höhe              { get; }
        public double AbsprungwinkelRad { get; }
        public double AbsprungwinkelDeg => Berechne.ToDeg(AbsprungwinkelRad);
        public double Radius            { get; }

        public static Schanze Create(double höhe, double absprungwinkel) {

            var radius = höhe   / (1 - Math.Cos(absprungwinkel));
            var länge  = radius * Math.Sin(absprungwinkel);

            return new Schanze(
                länge: länge,
                höhe: höhe,
                absprungwinkelRad: absprungwinkel,
                radius: radius
            );
        }

    }

}