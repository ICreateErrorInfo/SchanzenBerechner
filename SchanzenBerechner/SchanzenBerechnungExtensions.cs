using System.Windows;

using Berechnung;

namespace SchanzenBerechner {

    static class SchanzenBerechnungExtensions {

        private const double Scale = 100;

        public static Point GetSchanzenStartPunkt(this SchanzenBerechnung berechnung) {
            return new Point(0, 0);
        }

        public static Point GetSchanzenAbsprungPunkt(this SchanzenBerechnung berechnung) {
            return new Point(berechnung.Länge * Scale, berechnung.Höhe * Scale);
        }

        public static Point GetSchanzenEndPunkt(this SchanzenBerechnung berechnung) {
            return new Point(berechnung.Länge * Scale, 0);
        }

        public static Size GetSchanzenRadiusGröße(this SchanzenBerechnung berechnung) {
            return new Size(berechnung.Radius * Scale, berechnung.Radius * Scale);
        }

    }

}