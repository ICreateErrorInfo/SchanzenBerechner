using System.Windows;
using System.Windows.Media;

namespace ICreateErrorInfo.Shapes {

    public class PolylineArrow: Arrow {

        public PolylineArrow() {
            Points = new PointCollection();
        }

        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register(name: nameof(Points),
                                        propertyType: typeof(PointCollection),
                                        ownerType: typeof(PolylineArrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: new PointCollection(),
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public PointCollection Points {
            set => SetValue(PointsProperty, value);
            get => (PointCollection) GetValue(PointsProperty);
        }

        protected override PathFigure DefinePathFigure(out PolyLineSegment lineSegment) {
            lineSegment = null;
            var pathFigure = new PathFigure();

            if (Points.Count > 0) {

                pathFigure.StartPoint = Points[0];
                lineSegment           = new PolyLineSegment();
                pathFigure.Segments.Add(lineSegment);

                for (int i = 1; i < Points.Count; i++) {
                    lineSegment.Points.Add(Points[i]);
                }

            }

            return pathFigure;
        }

    }

}