using System;

namespace SchanzenBerechner
{
    class Berechne
    {
        public static double Höhe(double v0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {

            var t = Flugzeit(v0, alpha) / 2;
            var h = 0.5 * Naturkonstante.G * t * t;

            return h;

        }
        public static double Weite(double v0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {
            var t = Flugzeit(v0, alpha);
            alpha = alpha * Math.PI / 180;

            var vx = Math.Cos(alpha) * v0;

            var s = vx * t;

            return s;
        }
        public static double Flugzeit(double v0, double alpha) //V0 Geschwindigeit, alpha schanzenwinkel
        {


            alpha = alpha * Math.PI / 180; //winkelmaß in Bogenmaß

            var vy = Math.Sin(alpha);
            vy = vy * v0;
            var t = vy / Naturkonstante.G;
            return t * 2;
        }

    }
}
