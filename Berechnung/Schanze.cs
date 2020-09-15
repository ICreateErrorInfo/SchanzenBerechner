using System;

using Berechnung.Einheiten;

namespace Berechnung {

    public class Schanze {

        private readonly Länge _länge;
        private readonly Länge _höhe;
        private readonly Länge _radius;

        Schanze(Länge länge,
                Länge höhe,
                Winkel absprungwinkel,
                Länge radius, double scale) {

            _länge         = länge;
            _höhe          = höhe;
            Absprungwinkel = absprungwinkel;
            _radius        = radius;
            Scale          = scale;

        }

        public Länge  Länge          => _länge  * Scale;
        public Länge  Höhe           => _höhe   * Scale;
        public Länge  Radius         => _radius * Scale;
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

        public static Schanze Create(Länge höhe, Winkel absprungwinkel) {

            var radius = höhe.Meter / (1 - Math.Cos(absprungwinkel.Rad));
            var länge  = radius     * Math.Sin(absprungwinkel.Rad);

            return new Schanze(
                länge: Länge.FromMeter(länge),
                höhe: höhe,
                absprungwinkel: absprungwinkel,
                radius: Länge.FromMeter(radius),
                scale: 1
            );
        }

    }

}