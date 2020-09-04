using System;

namespace Berechnung {

    public static class Winkel {

        /// <summary>
        /// Umrechnung von Radiant zu Grad 
        /// </summary>
        public static double ToDeg(double rad) => rad * 180 / Math.PI;

        /// <summary>
        /// Umrechnung von Grad in Radiant.
        /// </summary>
        public static double ToRad(double deg) => deg * Math.PI / 180;

    }

}