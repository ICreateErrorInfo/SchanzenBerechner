using System.Collections.Generic;
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

        SettingViewModel _selectedSetting;

        public SettingViewModel SelectedSetting {
            get => _selectedSetting;
            set {
                _selectedSetting = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SettingViewModel> Settings => _settings;

        void OnSettingViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {

            if (HasPropertyChanged(nameof(SettingViewModel.RenderScene))) {
                UpdateScale();
            }

            bool HasPropertyChanged(string propertyName) {
                return string.IsNullOrEmpty(propertyName) || e.PropertyName == propertyName;
            }
        }

        private void OnSettingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            WireChildEvents();
            UpdateScale();

            if (SelectedSetting == null && Settings.Any()) {
                SelectedSetting = Settings.First();
            }

            if (SelectedSetting != null && !Settings.Contains(SelectedSetting)) {
                SelectedSetting = null;
            }
        }

        void UpdateScale() {

            if (!Settings.Any(s => s.RenderScene)) {
                return;
            }

            var maxWidth = Settings.Where(s => s.RenderScene)
                                   .Select(s => s.CalculateNaturalSize().Width)
                                   .Max();

            var scale = 1000 / maxWidth;

            foreach (var setting in Settings) {
                setting.Rescale(scale);
            }
        }

        readonly List<SettingViewModel> _connectedChilds = new List<SettingViewModel>();

        void WireChildEvents() {

            foreach (var s in _connectedChilds) {
                s.PropertyChanged -= OnSettingViewModelPropertyChanged;
            }

            _connectedChilds.Clear();

            foreach (var s in Settings) {
                s.PropertyChanged += OnSettingViewModelPropertyChanged;
                _connectedChilds.Add(s);
            }
        }

    }

}