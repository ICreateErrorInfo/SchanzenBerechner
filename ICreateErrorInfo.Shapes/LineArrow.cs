using System.Windows;
using System.Windows.Media;

namespace ICreateErrorInfo.Shapes {


    public class LineArrow: Arrow {

        public static readonly DependencyProperty X1Property =
            DependencyProperty.Register(name: nameof(X1),
                                        propertyType: typeof(double),
                                        ownerType: typeof(LineArrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: 0.0,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double X1 {
            set => SetValue(X1Property, value);
            get => (double) GetValue(X1Property);
        }

        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register(name: nameof(Y1),
                                        propertyType: typeof(double),
                                        ownerType: typeof(LineArrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: 0.0,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double Y1 {
            set => SetValue(Y1Property, value);
            get => (double) GetValue(Y1Property);
        }

        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register(name: nameof(X2),
                                        propertyType: typeof(double),
                                        ownerType: typeof(LineArrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: 0.0,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double X2 {
            set => SetValue(X2Property, value);
            get => (double) GetValue(X2Property);
        }

        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register(name: nameof(Y2),
                                        propertyType: typeof(double),
                                        ownerType: typeof(LineArrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: 0.0,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public double Y2 {
            set => SetValue(Y2Property, value);
            get => (double) GetValue(Y2Property);
        }

        protected override PathFigure DefinePathFigure(out PolyLineSegment lineSegment) {
            var pathFigure = new PathFigure {
                StartPoint = new Point(X1, Y1)
            };

            lineSegment = new PolyLineSegment();
            lineSegment.Points.Add(new Point(X2, Y2));
            pathFigure.Segments.Add(lineSegment);

            return pathFigure;
        }

    }

}