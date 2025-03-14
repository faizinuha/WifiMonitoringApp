using System;
using System.Windows;

namespace WifiMonitoringApp
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Inisialisasi dan jalankan aplikasi WPF
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}