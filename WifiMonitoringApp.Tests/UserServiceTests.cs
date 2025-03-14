using Xunit;
using WifiMonitoringApp.Services;
using WifiMonitoringApp.Models;
namespace WifiMonitoringApp.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddDevice_ShouldAddDeviceToList()
        {
            // Arrange
            var userService = new UserService();
            var device = new UserDevice { DeviceName = "Test Device", MacAddress = "00-11-22-33-44-55", IsAllowed = true };

            // Act
            userService.AddDevice(device);
            var devices = userService.GetAllowedDevices();

            // Assert
            Assert.Contains(device, devices);
        }

        [Fact]
        public void BlockDevice_ShouldBlockDevice()
        {
            // Arrange
            var userService = new UserService();
            var device = new UserDevice { DeviceName = "Test Device", MacAddress = "00-11-22-33-44-55", IsAllowed = true };
            userService.AddDevice(device);

            // Act
            userService.BlockDevice(device.MacAddress);
            var blockedDevice = userService.GetAllowedDevices().Find(d => d.MacAddress == device.MacAddress);

            // Assert
            Assert.False(blockedDevice.IsAllowed);
        }
    }
}