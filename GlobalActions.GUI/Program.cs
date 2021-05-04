using Avalonia;
using Avalonia.ReactiveUI;

namespace GlobalActions.GUI {
	internal class Program {

		public static void Main(string[] args) {
			try {
				InterceptKeys.Run();
				BuildAvaloniaApp()
					.StartWithClassicDesktopLifetime(args);
			}
			finally {
				InterceptKeys.KeyDown = null;
				InterceptKeys.Stop();
			}
		}

		private static AppBuilder BuildAvaloniaApp() {
			return AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.LogToTrace()
				.UseReactiveUI();
		}
	}
}