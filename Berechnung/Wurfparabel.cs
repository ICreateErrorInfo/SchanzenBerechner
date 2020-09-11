using System;

namespace Berechnung {

    /// <summary>
    /// Siehe: https://de.wikipedia.org/wiki/Wurfparabel
    /// </summary>
    public static class Wurfparabel {

        public static double ScheitelpunktX(double v0, double y0, double alpha) 
        {
            var x = C2(v0) / 2 * Math.Sin(2 * alpha);
            return x;
        }

        public static double ScheitelpunktY(double v0, double y0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var y = 0.5 * C2(v0) * Math.Sin(alpha) * Math.Sin(alpha)+y0;
            return y;
        }

        public static double Weite(double v0, double y0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var t = Flugzeit(v0: v0,y0: y0, alpha: alpha);

            var vx = Math.Cos(alpha) * v0;

            var s = vx * t;

            return s;
        }

        /// <summary>
        /// Berechnet den Abwurfwinkel im Bogemaß (RAD) an Hand der Geschwindigkeit, Anfangshöhe und Scheitelpunkt.
        /// </summary>
        /// <param name="v0">Die Anfangsgeschwindigkeit in m/s</param>
        /// <param name="y0">Die Anfangshöhe in m</param>
        /// <param name="ys">Der zu erreichende Scheitelpunkt in m</param>
        /// <returns></returns>
        public static double Abwurfwinkel(double v0, double y0, double ys) 
        {
            var alpha = Math.Asin(Math.Sqrt(2 * (ys - y0) / C2(v0)));

            return alpha;
        }

        /// <summary>
        /// Berechnet den Aufprallwinkel im Bogemaß (RAD) an Hand der Geschwindigkeit, Anfangshöhe und Abwurfwinkel.
        /// </summary>
        /// <param name="v0">Die Anfangsgeschwindigkeit in m/s</param>
        /// <param name="y0">Die Anfangshöhe in m</param>
        /// <param name="alpha">Der Abwurfwinkel im Bogenmaß (RAD)</param>
        public static double AufprallWinkel(double v0, double y0, double alpha) 
        {

            var spy = ScheitelpunktY(v0, y0, alpha);
            var tsp = Math.Sqrt(2 * spy / Naturkonstante.G);
            var vy  = tsp * Naturkonstante.G;

            var ap = Math.Atan(vy / (v0 * Math.Cos(alpha)));
            return ap;
        }

        /// <summary>
        /// Berechnet die Aufprallgeschwindigkeit in m/s an Hand der Geschwindigkeit, Anfangshöhe und Abwurfwinkel.
        /// </summary>
        /// <param name="v0">Die Anfangsgeschwindigkeit in m/s</param>
        /// <param name="y0">Die Anfangshöhe in m</param>
        /// <param name="alpha">Der Abwurfwinkel im Bogenmaß (RAD)</param>
        public static double AufprallGeschwindigkeit(double v0, double y0, double alpha) 
        {
            var vx = v0 * Math.Cos(alpha);
            var ap = AufprallWinkel(v0, y0, alpha);
            return vx / Math.Cos(ap);
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