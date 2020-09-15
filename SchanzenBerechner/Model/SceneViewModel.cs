using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

using Berechnung.Einheiten;

namespace SchanzenBerechner.Model {

    public class SceneViewModel: ViewModel {

        readonly ObservableCollection<SettingViewModel> _settings;

        public SceneViewModel() {
            _settings                   =  new ObservableCollection<SettingViewModel>();
            _settings.CollectionChanged += OnSettingCollectionChanged;

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) {

                var winkel          = Winkel.FromDeg(22);
                var geschwindigkeit = Geschwindigkeit.FromKilometerProStunde(20);
                var schanzenHöhe    = Länge.FromCentimeter(16);

                var schanze  = Berechnung.Schanze.Create(schanzenHöhe, winkel);
                var flugbahn = Berechnung.Flugbahn.Create(schanze, geschwindigkeit);

                var model = new SettingViewModel(schanze, flugbahn) {RenderMetrics = true};
                Settings.Add(model);
            }
        }

        public ObservableCollection<SettingViewModel> Settings => _settings;

        private void OnSettingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            UpdateScale();
        }

        void UpdateScale() {

            if (Settings.Count == 0) {
                return;
            }

            var maxWidth = Settings.Select(s => s.CalculateNaturalSize().Width)
                                   .Max();

            var scale = 1000 / maxWidth;

            foreach (var setting in Settings) {
                setting.Rescale(scale);
            }
        }

    }

}