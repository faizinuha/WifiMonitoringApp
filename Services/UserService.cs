using System.Collections.Generic;
using WifiMonitoringApp.Utilities;

public class UserService
{
    private List<UserDevice> _allowedDevices = new List<UserDevice>();
    private DatabaseHelper _databaseHelper = new DatabaseHelper();

    public UserService()
    {
        _databaseHelper.InitializeDatabase();
        _allowedDevices = _databaseHelper.GetDevices();
    }

    public void AddDevice(UserDevice device)
    {
        _allowedDevices.Add(device);
        _databaseHelper.AddDevice(device);
    }

    public void RemoveDevice(string macAddress)
    {
        _allowedDevices.RemoveAll(d => d.MacAddress == macAddress);
        _databaseHelper.RemoveDevice(macAddress);
    }

    public void BlockDevice(string macAddress)
    {
        var device = _allowedDevices.Find(d => d.MacAddress == macAddress);
        if (device != null)
        {
            device.IsAllowed = false;
            _databaseHelper.UpdateDevice(device);
        }
    }

    public List<UserDevice> GetAllowedDevices()
    {
        return _allowedDevices;
    }
}