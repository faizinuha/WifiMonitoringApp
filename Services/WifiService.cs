using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class WifiService
{
    private NotificationService _notificationService = new NotificationService();
    private List<string> _connectedNetworks = new List<string>();

    public List<WifiNetwork> GetAvailableWifiNetworks()
    {
        var wifiNetworks = new List<WifiNetwork>();

        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && ni.OperationalStatus == OperationalStatus.Up)
            {
                var wifiNetwork = new WifiNetwork
                {
                    SSID = ni.Name,
                    IPAddress = GetIpAddress(ni),
                    SignalStrength = "High" // Ini bisa diganti dengan logika untuk mendapatkan kekuatan sinyal
                };
                wifiNetworks.Add(wifiNetwork);
            }
        }

        return wifiNetworks;
    }

    private string GetIpAddress(NetworkInterface ni)
    {
        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
        {
            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.Address.ToString();
            }
        }
        return "No IP Address";
    }

    public void CheckFirstTimeConnection(string ssid)
    {
        if (!_connectedNetworks.Contains(ssid))
        {
            _connectedNetworks.Add(ssid);
            _notificationService.ShowNotification($"Anda pertama kali terhubung ke {ssid}");
        }
    }

    public void BlockUnknownDevices(List<UserDevice> allowedDevices)
    {
        // Dapatkan daftar perangkat yang terhubung
        var connectedDevices = GetConnectedDevices();

        foreach (var device in connectedDevices)
        {
            if (!allowedDevices.Any(d => d.MacAddress == device.MacAddress && d.IsAllowed))
            {
                BlockDevice(device.MacAddress);
            }
        }
    }

    private List<UserDevice> GetConnectedDevices()
    {
        // Implementasi untuk mendapatkan daftar perangkat yang terhubung
        // Ini bisa menggunakan ARP table atau tools lain
        return new List<UserDevice>();
    }

    public void BlockDevice(string macAddress)
    {
        string command = "netsh";
        string arguments = $"interface set interface Wi-Fi deny mac={macAddress}";
        string result = CommandHelper.RunCommand(command, arguments);
        Console.WriteLine(result); // Output hasil perintah
    }
}