using System;

namespace Berechnung {

    public readonly struct Winkel: IEquatable<Winkel> {

        Winkel(double rad) {
            Rad = rad;

        }

        /// <summary>
        /// Liefert den Wert des Winkels im Bogenmaß
        /// </summary>
        public double Rad { get; }

        /// <summary>
        /// Liefert den Wert des Winkel in Grad
        /// </summary>
        public double Deg => ToDeg(Rad);

        public override string ToString() => $"{Deg}°";

        /// <summary>
        /// Erstellt einen Winkel mit dem angegeben Wert im Bogenmaß.
        /// </summary>
        public static Winkel FromRad(double rad) => new Winkel(rad);

        /// <summary>
        /// Erstellt einen Winkel mit dem angegeben Wert in Grad.
        /// </summary>
        public static Winkel FromDeg(double rad) => new Winkel(ToRad(rad));

        /// <summary>
        /// Umrechnung von Radiant zu Grad 
        /// </summary>
        public static double ToDeg(double rad) => rad * 180 / Math.PI;

        /// <summary>
        /// Umrechnung von Grad ins Bogenmaß (Radiant).
        /// </summary>
        public static double ToRad(double deg) => deg * Math.PI / 180;

        #region Equality members

        public bool Equals(Winkel other) {
            return Rad.Equals(other.Rad);
        }

        public override bool Equals(object obj) {
            return obj is Winkel other && Equals(other);
        }

        public override int GetHashCode() {
            return Rad.GetHashCode();
        }

        public static bool operator ==(Winkel left, Winkel right) {
            return left.Equals(right);
        }

        public static bool operator !=(Winkel left, Winkel right) {
            return !left.Equals(right);
        }

        #endregion

    }

}