using System.Data.SQLite;

public class DatabaseHelper
{
    private string _connectionString = "Data Source=wifi.db;Version=3;";

    public void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Devices (MacAddress TEXT PRIMARY KEY, DeviceName TEXT, IsAllowed INTEGER)", connection);
            command.ExecuteNonQuery();
        }
    }

    public void AddDevice(UserDevice device)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = new SQLiteCommand("INSERT INTO Devices (MacAddress, DeviceName, IsAllowed) VALUES (@mac, @name, @allowed)", connection);
            command.Parameters.AddWithValue("@mac", device.MacAddress);
            command.Parameters.AddWithValue("@name", device.DeviceName);
            command.Parameters.AddWithValue("@allowed", device.IsAllowed ? 1 : 0);
            command.ExecuteNonQuery();
        }
    }
}