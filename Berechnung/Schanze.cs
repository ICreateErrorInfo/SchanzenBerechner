using System;

namespace Berechnung {

    public class Schanze {

        private readonly double _länge;
        private readonly double _höhe;
        private readonly double _radius;

        Schanze(double länge,
                double höhe,
                Winkel absprungwinkel,
                double radius, double scale) {

            _länge         = länge;
            _höhe          = höhe;
            Absprungwinkel = absprungwinkel;
            _radius        = radius;
            Scale          = scale;

        }

        public double Länge          => _länge  * Scale;
        public double Höhe           => _höhe   * Scale;
        public double Radius         => _radius * Scale;
        public Winkel Absprungwinkel { get; }
        public double Scale          { get; }

        public Schanze WithScale(double scale) {
            return new Schanze(
                länge: _länge,
                höhe: _höhe,
                absprungwinkel: Absprungwinkel,
                radius: _radius,
                scale: scale);
        }

        public static Schanze Create(double höhe, Winkel absprungwinkel) {

            var radius = höhe   / (1 - Math.Cos(absprungwinkel.Rad));
            var länge  = radius * Math.Sin(absprungwinkel.Rad);

            return new Schanze(
                länge: länge,
                höhe: höhe,
                absprungwinkel: absprungwinkel,
                radius: radius,
                scale: 1
            );
        }

    }

}