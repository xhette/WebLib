using Ionic.Zip;
using Ionic.Zlib;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.BusinessLayer.GeneralMethods.AdminPages
{
	public static class BackupMethods
	{
		public static byte[] BackupDb(string path)
		{
            try
            {
                Server server = new Server("localhost\\SQLEXPRESS");
                Backup backup = new Backup();
                backup.Action = BackupActionType.Database;
                backup.Database = "LibDB";
                backup.Incremental = false;
                backup.Initialize = true;
                backup.LogTruncation = BackupTruncateLogType.Truncate;

                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                {
                    string filePath = String.Format("{0}\\{1}", path, Guid.NewGuid());
                    DirectoryInfo rootDirInfo = new DirectoryInfo(filePath);

                    if (!rootDirInfo.Exists)
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = String.Format("{0}\\{1}.bak", filePath, "LibDB");

                    BackupDeviceItem backupItemDevice = new BackupDeviceItem(fileName, DeviceType.File);
                    backup.Devices.Add(backupItemDevice);
                    backup.PercentCompleteNotification = 10;
                    backup.SqlBackup(server);


                    using (ZipFile zipArchiver = new ZipFile(Encoding.GetEncoding("cp866")))
                    {
                        zipArchiver.AddDirectory(filePath);
                        zipArchiver.CompressionLevel = CompressionLevel.BestCompression;

                        using (MemoryStream output = new MemoryStream())
                        {
                            zipArchiver.Save(output);
                            return output.ToArray();
                        }
                    }
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RestoreDb(string path)
        {
            Server server = new Server("localhost\\SQLEXPRESS");
            Restore restore = new Restore();
            DirectoryInfo directory = new DirectoryInfo(path);
            if (directory.Exists)
            {

                string fileName = string.Format("{0}\\{1}.bak", path, "LibDB");
                restore.Devices.Add(new BackupDeviceItem(fileName, DeviceType.File));

                string destinationDatabaseName =
                    string.Format("LibDB");

                Database currentDatabase = server.Databases["LibDB"];
                string currentLogicalData =
                    currentDatabase.FileGroups[0].Files[0].Name;
                string currentLogicalLog = currentDatabase.LogFiles[0].Name;

                // Now relocate the data and log files
                RelocateFile reloData =
                    new RelocateFile(currentLogicalData,
                    string.Format(@"{0}\{1}.mdf", path,
                destinationDatabaseName));
                RelocateFile reloLog =
                    new RelocateFile(currentLogicalLog,
                    string.Format(@"{0}\{1} _Log.ldf", path,
                destinationDatabaseName));
                restore.RelocateFiles.Add(reloData);
                restore.RelocateFiles.Add(reloLog);

                restore.Database = destinationDatabaseName;
                restore.ReplaceDatabase = true;

                restore.PercentCompleteNotification = 10;
                server.KillAllProcesses(destinationDatabaseName);
                restore.SqlRestore(server);
            }
        }
    }
}
