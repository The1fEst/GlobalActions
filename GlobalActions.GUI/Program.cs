using Avalonia;
using Avalonia.ReactiveUI;
using GlobalActions.GUI.NodeSystem.Nodes;

namespace GlobalActions.GUI {
    internal class Program {
        public static void Main(string[] args) {
            try {
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