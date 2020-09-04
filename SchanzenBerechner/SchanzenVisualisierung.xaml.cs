using Berechnung;

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

        void Refesh() {

            SchanzenPath.Data = null;

            var berechnung = Schanze;
            if (berechnung == null) {
                return;
            }

            var pathGeometry = new PathGeometry();
            var figure       = new PathFigure {IsClosed = true};

            figure.Segments.Add(new LineSegment {Point = berechnung.GetSchanzenStartPunkt()});
            figure.Segments.Add(new LineSegment {Point = berechnung.GetSchanzenEndPunkt()});
            figure.Segments.Add(new LineSegment {Point = berechnung.GetSchanzenAbsprungPunkt()});
            figure.Segments.Add(new ArcSegment {
                Size  = berechnung.GetSchanzenRadiusGröße(),
                Point = berechnung.GetSchanzenStartPunkt(),

            });

            pathGeometry.Figures.Add(figure);
            SchanzenPath.Data = pathGeometry;

            Canvas.Width = berechnung.GetSchanzenEndPunkt().X;
            Canvas.Height = berechnung.GetSchanzenAbsprungPunkt().Y;
        }

        public static readonly DependencyProperty SchanzenBerechnungProperty =
            DependencyProperty.Register(
                name: nameof(Schanze),
                propertyType: typeof(Schanze),
                ownerType: typeof(SchanzenVisualisierung),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    flags: FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnSchanzenBerechnungChanged));

        static void OnSchanzenBerechnungChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            var me = (SchanzenVisualisierung) d;
            me.Refesh();

        }

    }

}