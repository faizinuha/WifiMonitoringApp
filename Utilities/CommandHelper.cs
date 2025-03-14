using System;
using System.Diagnostics;

namespace WifiMonitoringApp.Utilities
{
    public static class CommandHelper
    {
        /// <summary>
        /// Menjalankan perintah sistem dan mengembalikan output-nya.
        /// </summary>
        /// <param name="command">Nama perintah atau executable (misalnya, netsh).</param>
        /// <param name="arguments">Argumen yang diberikan ke perintah.</param>
        /// <returns>Output dari perintah yang dijalankan.</returns>
        public static string RunCommand(string command, string arguments)
        {
            try
            {
                // Konfigurasi proses untuk menjalankan perintah
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    RedirectStandardOutput = true, // Mengarahkan output ke aplikasi
                    UseShellExecute = false,      // Tidak menggunakan shell
                    CreateNoWindow = true          // Tidak menampilkan jendela console
                };

                // Jalankan perintah
                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit(); // Tunggu sampai perintah selesai
                    return process.StandardOutput.ReadToEnd(); // Baca output
                }
            }
            catch (Exception ex)
            {
                // Tangani error dan kembalikan pesan error
                return $"Error: {ex.Message}";
            }
        }
    }
}