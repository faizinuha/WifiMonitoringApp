using Xunit;
using WifiMonitoringApp.Services;

namespace WifiMonitoringApp.Tests
{
    public class WifiServiceTests
    {
        [Fact]
        public void GetAvailableWifiNetworks_ShouldReturnList()
        {
            // Arrange
            var wifiService = new WifiService();

            // Act
            var result = wifiService.GetAvailableWifiNetworks();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}