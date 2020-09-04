using System;
using System.Windows;

namespace Berechnung {

    public class Schanze {

        private readonly double _länge;
        private readonly double _höhe;
        private readonly double _radius;

        Schanze(double länge, double höhe, double absprungwinkelRad, double radius, double scale) {
            _länge            = länge;
            _höhe             = höhe;
            AbsprungwinkelRad = absprungwinkelRad;
            _radius           = radius;
            Scale             = scale;

        }

        public double Länge  => _länge  * Scale;
        public double Höhe   => _höhe   * Scale;
        public double Radius => _radius * Scale;

        public double AbsprungwinkelRad { get; }
        public double AbsprungwinkelDeg => Berechne.ToDeg(AbsprungwinkelRad);

        public double Scale { get; }

        public Point SchanzenStartPunkt => new Point(0, 0);

        public Point AbsprungPunkt => new Point(Länge, Höhe);

        public Point EndPunkt => new Point(Länge, 0);

        public Size RadiusGröße => new Size(Radius, Radius);

        public Schanze WithScale(double scale) {
            return new Schanze(
                länge: Länge,
                höhe: Höhe,
                absprungwinkelRad: AbsprungwinkelRad,
                radius: Radius,
                scale: scale);
        }

        public static Schanze Create(double höhe, double absprungwinkel) {

            var radius = höhe   / (1 - Math.Cos(absprungwinkel));
            var länge  = radius * Math.Sin(absprungwinkel);

            return new Schanze(
                länge: länge,
                höhe: höhe,
                absprungwinkelRad: absprungwinkel,
                radius: radius,
                scale: 1
            );
        }

    }

}