﻿using Berechnung;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchanzenBerechner {

    public partial class SchanzenVisualisierung: UserControl {

        public SchanzenVisualisierung() {
            InitializeComponent();
        }

        public Schanze Schanze {
            get => (Schanze) GetValue(SchanzenBerechnungProperty);
            set => SetValue(SchanzenBerechnungProperty, value);
        }

        public static readonly DependencyProperty SchanzenBerechnungProperty =
            DependencyProperty.Register(
                name: nameof(Schanze),
                propertyType: typeof(Schanze),
                ownerType: typeof(SchanzenVisualisierung),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnSchanzeChanged));

        static void OnSchanzeChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            var me = (SchanzenVisualisierung) d;
            me.Refesh();

        }

        public Flugbahn Flugbahn {
            get => (Flugbahn) GetValue(FlugbahnProperty);
            set => SetValue(FlugbahnProperty, value);
        }

        public static readonly DependencyProperty FlugbahnProperty =
            DependencyProperty.Register(
                name: nameof(Flugbahn),
                propertyType: typeof(Flugbahn),
                ownerType: typeof(SchanzenVisualisierung),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnFlugbahnChanged));

        static void OnFlugbahnChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            var me = (SchanzenVisualisierung) d;
            me.Refesh();

        }

        void Refesh() {

            var orgSize = CalculateDesiredCanvasSize(Schanze, Flugbahn);
            // Wir sagen es soll alles auf 1000 Pixel Platz haben...
            var scale = 1000 / orgSize.Width;
         //   var scale = 1000.0;

            var schanze  = Schanze?.WithScale(scale);
            var flugbahn = Flugbahn?.WithScale(scale);

            SchanzenPath.Data = CreateSchanzenGeometry(schanze);
            FlugbahnPath.Data = CreateFlugbahnGeometry(flugbahn);

            var size = CalculateDesiredCanvasSize(schanze, flugbahn);
            Canvas.Width  = size.Width;
            Canvas.Height = size.Height;
        }

        static Size CalculateDesiredCanvasSize(Schanze schanze, Flugbahn flugbahn) {
            var width  = 10.0;
            var height = 10.0;
            if (schanze != null) {
                width  = schanze.EndPunkt.X;
                height = schanze.AbsprungPunkt.Y;
            }

            if (flugbahn != null) {
                width += flugbahn.SprungWeite;
                //height=Math TODO..
            }

            return new Size(width, height);
        }

        private static PathGeometry CreateSchanzenGeometry(Schanze schanze) {

            if (schanze == null) {
                return null;
            }

            var pathGeometry = new PathGeometry();
            var figure       = new PathFigure {IsClosed = true};

            figure.Segments.Add(new LineSegment {Point = schanze.SchanzenStartPunkt});
            figure.Segments.Add(new LineSegment {Point = schanze.EndPunkt});
            figure.Segments.Add(new LineSegment {Point = schanze.AbsprungPunkt});
            figure.Segments.Add(new ArcSegment {
                Size  = schanze.RadiusGröße,
                Point = schanze.SchanzenStartPunkt,

            });

            pathGeometry.Figures.Add(figure);
            return pathGeometry;
        }

        private static PathGeometry CreateFlugbahnGeometry(Flugbahn flugbahn) {

            if (flugbahn == null) {
                return null;
            }

            return null;
        }

    }

}