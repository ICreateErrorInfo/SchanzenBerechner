using System;

namespace Berechnung {

    public readonly struct Geschwindigkeit: IEquatable<Geschwindigkeit> {

        readonly double _geschwindigkeit;

        Geschwindigkeit(double geschwindigkeit) {
            _geschwindigkeit = geschwindigkeit;

        }

        /// <summary>
        /// Die Geschwindigkeit in km/h
        /// </summary>
        public double KmProH => ToKmProH(_geschwindigkeit);

        /// <summary>
        /// Die Geschwindigkeit in m/s
        /// </summary>
        public double MProS => _geschwindigkeit;

        /// <summary>
        /// Erstellt eine Geschwindigkeit mit dem angegebenen Wert in m/s.
        /// </summary>
        /// <param name="value">Die Geschwindigkeit in m/s</param>
        public static Geschwindigkeit FromMProS(double value) => new Geschwindigkeit(value);

        /// <summary>
        /// Erstellt eine Geschwindigkeit mit dem angegebenen Wert in km/h.
        /// </summary>
        /// <param name="value">Die Geschwindigkeit in km/h</param>
        public static Geschwindigkeit FromKmProH(double value) => new Geschwindigkeit(ToMProS(value));

        public static double ToKmProH(double value) => value * 3.6;
        public static double ToMProS(double value) => value  / 3.6;

        #region Equality members

        public bool Equals(Geschwindigkeit other) {
            return _geschwindigkeit.Equals(other._geschwindigkeit);
        }

        public override bool Equals(object obj) {
            return obj is Geschwindigkeit other && Equals(other);
        }

        public override int GetHashCode() {
            return _geschwindigkeit.GetHashCode();
        }

        public static bool operator ==(Geschwindigkeit left, Geschwindigkeit right) {
            return left.Equals(right);
        }

        public static bool operator !=(Geschwindigkeit left, Geschwindigkeit right) {
            return !left.Equals(right);
        }

        #endregion

    }

}