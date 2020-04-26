using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLib.DataLayer
{
	public class BackupMethods
	{

        public static byte[] BackupDb(string path)
        {
            //Server server = new Server("localhost");
            //Backup backup = new Backup();
            //backup.Action = BackupActionType.Database;
            //backup.Database = "LibDB";
            //backup.Incremental = false;
            //backup.Initialize = true;
            //backup.LogTruncation = BackupTruncateLogType.Truncate;

            //DirectoryInfo directory = new DirectoryInfo(path);
            //if (directory.Exists)
            //{
            //    string name = "LibraryDbBackup";
            //    string date = DateTime.Today.ToString("dd-MM-yyyy");

            //    string folderName = name + "-" + date;

            //    string filePath = String.Format("{0}\\{1}", path, folderName);
            //    DirectoryInfo rootDirInfo = new DirectoryInfo(filePath);

            //    if (!rootDirInfo.Exists)
            //    {
            //        Directory.CreateDirectory(filePath);
            //    }

            //    string fileName = String.Format("{0}\\{1}.bak", filePath, "BackupFile");

            //    BackupDeviceItem backupItemDevice = new BackupDeviceItem(fileName, DeviceType.File);
            //    backup.Devices.Add(backupItemDevice);
            //    backup.PercentCompleteNotification = 10;
            //    backup.SqlBackup(server);

            //    string archiveName = String.Format("{0}.zip", folderName);

            //    return null;

            //}
           // else 
                return null;
        }
     
        public static void RestoreDb(string path)
        {
            //Server server = new Server("localhost");
            //Restore restore = new Restore();
            //DirectoryInfo directory = new DirectoryInfo(path);
            //if (directory.Exists)
            //{

            //    string fileName = string.Format("{0}\\{1}.bak", path, "BackupFile");
            //    restore.Devices.Add(new BackupDeviceItem(fileName, DeviceType.File));

            //    string destinationDatabaseName =
            //        string.Format("LibDB");

            //    Database currentDatabase = server.Databases["LibDB"];
            //    string currentLogicalData =
            //       currentDatabase.FileGroups[0].Files[0].Name;
            //    string currentLogicalLog = currentDatabase.LogFiles[0].Name;

            //    // Now relocate the data and log files
            //    RelocateFile reloData =
            //        new RelocateFile(currentLogicalData,
            //        string.Format(@"{0}\{1}.mdf", path,
            //    destinationDatabaseName));
            //    RelocateFile reloLog =
            //        new RelocateFile(currentLogicalLog,
            //        string.Format(@"{0}\{1} _Log.ldf", path,
            //    destinationDatabaseName));
            //    restore.RelocateFiles.Add(reloData);
            //    restore.RelocateFiles.Add(reloLog);

            //    restore.Database = destinationDatabaseName;
            //    restore.ReplaceDatabase = true;

            //    restore.PercentCompleteNotification = 10;
            //    server.KillAllProcesses(destinationDatabaseName);
            //    restore.SqlRestore(server);
            //}
        }
    }
}
