using System;

namespace Berechnung
{
    public static class  Berechne {

        /// <summary>
        /// Umrechnung von Radiant zu Grad 
        /// </summary>
        public static double ToDeg(double rad) => rad * 180 / Math.PI;

        /// <summary>
        /// Umrechnung von Grad in Radiant.
        /// </summary>
        public static double ToRad(double deg) => deg * Math.PI / 180;

        public static double Höhe(double v0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var t = Flugzeit(v0, alpha) / 2;
            var h = 0.5 * Naturkonstante.G * t * t;

            return h;

        }
        public static double Weite(double v0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var t = Flugzeit(v0, alpha);

            var vx = Math.Cos(alpha) * v0;

            var s = vx * t;

            return s;
        }
        public static double Winkel(double v0, double ys)
        {

            var ergebnis = Math.Asin(Math.Sqrt(ys * 2 * Naturkonstante.G / (v0 * v0)));
            ergebnis = ergebnis * 180 / Math.PI;
            return ergebnis;
        }
        public static double Flugzeit(double v0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var vy = Math.Sin(alpha);
            vy = vy * v0;
            var t = vy / Naturkonstante.G;
            return t * 2;
        }

        public static double Y(double x, double v0, double alpha, double y0) {

            var y = -(x * x) / (2 * v0 * v0 * Math.Cos(alpha) * Math.Cos(alpha)) + Math.Tan(alpha) * x + y0;

            return y;

        }

    }
}
