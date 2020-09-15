using System;

namespace Berechnung.Einheiten {

    public readonly struct Länge: IEquatable<Länge> {

        Länge(double meter) {
            Meter = meter;

        }

        public double Meter      { get; }
        public double Centimeter => Meter * 100;
        public double Millimeter => Meter * 1000;

        public static Länge Empty => new Länge();

        public static Länge FromMeter(double value) => new Länge(value);
        public static Länge FromCentimeter(double value) => new Länge(value / 100);
        public static Länge FromMillimeter(double value) => new Länge(value / 1000);

        public static Länge operator +(Länge l1, Länge l2) {
            return Länge.FromMeter(l1.Meter + l2.Meter);
        }

        public static Länge operator -(Länge l1, Länge l2) {
            return Länge.FromMeter(l1.Meter - l2.Meter);
        }

        public static Länge operator *(Länge länge, double value) {
            return Länge.FromMeter(länge.Meter * value);
        }

        public static Länge operator /(Länge länge, double value) {
            return FromMeter(länge.Meter / value);
        }

        public bool Equals(Länge other) {
            return Meter.Equals(other.Meter);
        }

        public override bool Equals(object obj) {
            return obj is Länge other && Equals(other);
        }

        public override int GetHashCode() {
            return Meter.GetHashCode();
        }

        public static bool operator ==(Länge left, Länge right) {
            return left.Equals(right);
        }

        public static bool operator !=(Länge left, Länge right) {
            return !left.Equals(right);
        }

    }

}