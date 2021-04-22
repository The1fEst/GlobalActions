using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

namespace GlobalActions.GUI.Components {
    public class NamedField : UserControl {
        public NamedField() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }

        private static readonly DirectProperty<NamedField, string> LabelProperty =
            AvaloniaProperty.RegisterDirect<NamedField, string>(
                nameof(Label),
                o => o.Label,
                (o, v) => o.Label = v);

        private string _label;

        public string Label {
            get => _label;
            set => SetAndRaise(LabelProperty, ref _label, value);
        }

        private static readonly DirectProperty<NamedField, GridLength> LabelWidthProperty =
            AvaloniaProperty.RegisterDirect<NamedField, GridLength>(
                nameof(LabelWidth),
                o => o.LabelWidth,
                (o, v) => o.LabelWidth = v);

        private GridLength _labelWidth;

        public GridLength LabelWidth {
            get => _labelWidth;
            set => SetAndRaise(LabelWidthProperty, ref _labelWidth, value);
        }
    }
}