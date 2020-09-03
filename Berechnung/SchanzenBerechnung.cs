using System;

namespace Berechnung {

    public class SchanzenBerechnung {

        SchanzenBerechnung(double länge, double höhe, double absprungwinkelRad, double radius) {
            Länge             = länge;
            Höhe              = höhe;
            AbsprungwinkelRad = absprungwinkelRad;
            Radius            = radius;

        }

        public double Länge             { get; }
        public double Höhe              { get; }
        public double AbsprungwinkelRad { get; }
        public double AbsprungwinkelDeg => AbsprungwinkelRad * 180 / Math.PI;
        public double Radius            { get; }

        public static SchanzenBerechnung Berechne(double höhe, double absprungwinkel) {

            var radius = höhe   / (1 - Math.Cos(absprungwinkel));
            var länge  = radius * Math.Sin(absprungwinkel);

            return new SchanzenBerechnung(
                länge: länge,
                höhe: höhe,
                absprungwinkelRad: absprungwinkel,
                radius: radius
            );
        }

    }

}