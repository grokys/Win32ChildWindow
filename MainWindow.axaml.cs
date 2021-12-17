using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Win32ChildWindow
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr value);
        const int GWL_HWNDPARENT = -8;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public void ShowChild(object sender, RoutedEventArgs e)
        {
            var child = new Window { Content = "Child Window" };
            child.Show();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var parentHwnd = this.PlatformImpl.Handle.Handle;
                var hwnd = child.PlatformImpl.Handle.Handle;
                SetWindowLongPtr(hwnd, GWL_HWNDPARENT, parentHwnd);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
