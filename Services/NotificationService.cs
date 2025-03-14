using System;
using System.Windows;

public class NotificationService
{
   private void CheckFirstTimeConnection(string ssid)
{
    if (!_wifiService.IsNetworkKnown(ssid))
    {
        MessageBox.Show($"Anda pertama kali terhubung ke {ssid}.", "Notifikasi", MessageBoxButton.OK, MessageBoxImage.Information);
        _wifiService.MarkNetworkAsKnown(ssid);
    }
}
}