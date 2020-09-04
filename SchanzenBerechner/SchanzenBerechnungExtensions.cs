using System.Windows;

using Berechnung;

namespace SchanzenBerechner {

    static class SchanzenBerechnungExtensions {

        private const double Scale = 100;

        public static Point GetSchanzenStartPunkt(this Schanze parameter) {
            return new Point(0, 0);
        }

        public static Point GetSchanzenAbsprungPunkt(this Schanze parameter) {
            return new Point(parameter.Länge * Scale, parameter.Höhe * Scale);
        }

        public static Point GetSchanzenEndPunkt(this Schanze parameter) {
            return new Point(parameter.Länge * Scale, 0);
        }

        public static Size GetSchanzenRadiusGröße(this Schanze parameter) {
            return new Size(parameter.Radius * Scale, parameter.Radius * Scale);
        }

    }

}