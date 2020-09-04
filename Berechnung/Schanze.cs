using System;
using System.Windows;

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

        public double Länge  => _länge  * Scale;
        public double Höhe   => _höhe   * Scale;
        public double Radius => _radius * Scale;

        public Winkel Absprungwinkel { get; }

        public double Scale { get; }

        public Point SchanzenStartPunkt => new Point(0, 0);

        public Point AbsprungPunkt => new Point(Länge, Höhe);

        public Point EndPunkt => new Point(Länge, 0);

        public Size RadiusGröße => new Size(Radius, Radius);

        public Schanze WithScale(double scale) {
            return new Schanze(
                länge: Länge,
                höhe: Höhe,
                absprungwinkel: Absprungwinkel,
                radius: Radius,
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