using System;

namespace Berechnung.Einheiten {

    public readonly struct Geschwindigkeit: IEquatable<Geschwindigkeit> {

        Geschwindigkeit(double meterProSekunde) {
            MeterProSekunde = meterProSekunde;
        }

        /// <summary>
        /// Die Geschwindigkeit in m/s
        /// </summary>
        public double MeterProSekunde { get; }

        /// <summary>
        /// Die Geschwindigkeit in km/h
        /// </summary>
        public double KilometerProStunde => ToKilometerProStunde(MeterProSekunde);

        /// <summary>
        /// Erstellt eine Geschwindigkeit mit dem angegebenen Wert in m/s.
        /// </summary>
        /// <param name="value">Die Geschwindigkeit in m/s</param>
        public static Geschwindigkeit FromMeterProSekunde(double value) => new Geschwindigkeit(value);

        /// <summary>
        /// Erstellt eine Geschwindigkeit mit dem angegebenen Wert in km/h.
        /// </summary>
        /// <param name="value">Die Geschwindigkeit in km/h</param>
        public static Geschwindigkeit FromKilometerProStunde(double value) => new Geschwindigkeit(ToFromMeterProSekunde(value));

        static double ToKilometerProStunde(double value) => value  * 3.6;
        static double ToFromMeterProSekunde(double value) => value / 3.6;

        #region Equality members

        public bool Equals(Geschwindigkeit other) {
            return MeterProSekunde.Equals(other.MeterProSekunde);
        }

        public override bool Equals(object obj) {
            return obj is Geschwindigkeit other && Equals(other);
        }

        public override int GetHashCode() {
            return MeterProSekunde.GetHashCode();
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