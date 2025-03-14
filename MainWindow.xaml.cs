using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WifiMonitoringApp
{
    public partial class MainWindow : Window
    {
        private WifiService _wifiService = new WifiService();
        private UserService _userService = new UserService();

        public MainWindow()
        {
            InitializeComponent();
            RefreshWifiList();
            RefreshUserList();
        }

        private void RefreshWifiList()
        {
            // Ambil daftar WiFi dari WifiService
            var wifiNetworks = _wifiService.GetAvailableWifiNetworks();
            WifiList.ItemsSource = wifiNetworks;
        }

        private void RefreshUserList()
        {
            // Ambil daftar pengguna dari UserService
            var userDevices = _userService.GetAllowedDevices();
            UserList.ItemsSource = userDevices;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Refresh daftar WiFi dan pengguna
            RefreshWifiList();
            RefreshUserList();
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            // Blokir perangkat yang dipilih
            var selectedDevice = UserList.SelectedItem as UserDevice;
            if (selectedDevice != null)
            {
                _userService.BlockDevice(selectedDevice.MacAddress);
                RefreshUserList();
                MessageBox.Show($"Perangkat {selectedDevice.DeviceName} berhasil diblokir.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Pilih perangkat terlebih dahulu.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Tambahkan pengguna baru (contoh sederhana)
            var newDevice = new UserDevice
            {
                DeviceName = "Perangkat Baru",
                MacAddress = "AA-BB-CC-DD-EE-FF",
                IsAllowed = true
            };
            _userService.AddDevice(newDevice);
            RefreshUserList();
            MessageBox.Show($"Perangkat {newDevice.DeviceName} berhasil ditambahkan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}