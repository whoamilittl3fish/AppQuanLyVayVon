using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuanLyVayVon.CSDL
{
    public static class CSDL_BackupFunc
    {
         public static string DbPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "data.db");

        public static bool BackupDatabase()
        {
            try
            {
                string backupDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup");
                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
                string backupFile = Path.Combine(backupDir, $"data_{timestamp}.db");

                File.Copy(DbPath, backupFile, true);

                // Open the backup file location in File Explorer
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = backupDir,
                    UseShellExecute = true,
                    Verb = "open"
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RestoreDatabase(string selectedFile)
        {
            try
            {
                File.Copy(selectedFile, DbPath, overwrite: true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
