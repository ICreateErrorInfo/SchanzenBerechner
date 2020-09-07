using System;

namespace Berechnung {

    /// <summary>
    /// Siehe: https://de.wikipedia.org/wiki/Wurfparabel
    /// </summary>
    public static class Wurfparabel {

        public static double Höhe(double v0, double y0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var h = 0.5 * C2(v0) * Math.Sin(alpha) * Math.Sin(alpha)+y0;
            return h;

        }

        public static double Weite(double v0, double y0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var t = Flugzeit(v0: v0,y0: y0, alpha: alpha);

            var vx = Math.Cos(alpha) * v0;

            var s = vx * t;

            return s;
        }

        public static double Winkel(double v0, double ys) {

            var ergebnis = Math.Asin(Math.Sqrt(ys * 2 * Naturkonstante.G / (v0 * v0)));
            return ergebnis;
        }

        public static double Flugzeit(double v0, double y0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var t  = v0 / Naturkonstante.G * (Math.Sin(alpha) + Math.Sqrt(Math.Sin(alpha) * Math.Sin(alpha) + C1(y0,v0)) );
            return t;
        }

        public static double Y(double x, double v0, double alpha, double y0) {

            var y = y0 + Math.Tan(alpha) * x -(Naturkonstante.G * x * x) / (2 * v0 * v0 * Math.Cos(alpha) * Math.Cos(alpha));

            return y;

        }

        static double C1(double y0, double v0) {
            return (2 * y0 * Naturkonstante.G) / (v0 * v0);
        }

        static double C2(double v0) {
            return (v0 * v0)/ Naturkonstante.G ;
        }

    }

}