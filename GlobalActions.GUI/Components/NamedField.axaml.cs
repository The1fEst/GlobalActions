using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GlobalActions.GUI.Components {
	public class NamedField : UserControl {
		private static readonly DirectProperty<NamedField, string> LabelProperty =
			AvaloniaProperty.RegisterDirect<NamedField, string>(
				nameof(Label),
				o => o.Label,
				(o, v) => o.Label = v);

		private static readonly DirectProperty<NamedField, GridLength> LabelWidthProperty =
			AvaloniaProperty.RegisterDirect<NamedField, GridLength>(
				nameof(LabelWidth),
				o => o.LabelWidth,
				(o, v) => o.LabelWidth = v);

		private string _label;

		private GridLength _labelWidth;

		public NamedField() {
			InitializeComponent();
		}

		public string Label {
			get => _label;
			set => SetAndRaise(LabelProperty, ref _label, value);
		}

		public GridLength LabelWidth {
			get => _labelWidth;
			set => SetAndRaise(LabelWidthProperty, ref _labelWidth, value);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}