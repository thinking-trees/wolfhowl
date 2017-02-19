using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;

namespace Workflow.Platform.Common.Helper
{
    public class FileOperationHelper
    {
        /// <summary>
        /// 文件夹的全复制
        /// </summary>
        /// <param name="sourecDirectory">待复制的文件夹路径 如D:/Test</param>
        /// <param name="destPath">目标路径 如E:/ABC</param>
        public static void DirectoryCopy(string sourecDirectory, string destPath)
        {
            //异常检查
            if (!Directory.Exists(sourecDirectory))
            {
                return;
            }
            if (!Directory.Exists(destPath))
            {
                return;
            }

            //待复制的文件夹名称
            DirectoryInfo di = new DirectoryInfo(sourecDirectory);
            string childFolder = di.Name;

            if (sourecDirectory == destPath + childFolder)
            {
                return;
            }

            if (Directory.Exists(destPath + Path.DirectorySeparatorChar.ToString() + childFolder))
            {
                DeleteDir(destPath + Path.DirectorySeparatorChar.ToString() + childFolder);
            }
            Directory.CreateDirectory(destPath + Path.DirectorySeparatorChar.ToString() + childFolder);

            //拷贝文件，如果新目录中已经包含该文件，则直接覆盖
            string[] files = Directory.GetFiles(sourecDirectory);
            for (int i = 0; i < files.Length; i++)
            {
                string destFileName = destPath + Path.DirectorySeparatorChar.ToString() + childFolder + Path.DirectorySeparatorChar.ToString() + Path.GetFileName(files[i]);
                if (File.Exists(destFileName))
                {
                    File.Delete(destFileName);
                }
                File.Copy(files[i], destFileName);
            }

            //拷贝目录
            string[] dires = Directory.GetDirectories(sourecDirectory);
            for (int j = 0; j < dires.Length; j++)
            {
                DirectoryCopy(dires[j], destPath + Path.DirectorySeparatorChar.ToString() + childFolder);
            }
        }

        /// <summary>
        /// 递归删除文件夹，避免只读文件导致删除不了的情况
        /// </summary>
        /// <param name="dir">文件夹全路径</param>
        public static void DeleteDir(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            try
            {
                if (File.Exists(dir))
                {
                    File.Delete(dir);
                }
                else
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(dir);
                    DirectorySecurity dirsecurity = dirinfo.GetAccessControl();
                    dirsecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                    dirinfo.SetAccessControl(dirsecurity);
                    foreach (string childName in Directory.GetFileSystemEntries(dir))//获取子文件和子文件夹
                    {
                        if (File.Exists(childName)) //如果是文件
                        {
                            FileInfo fi = new FileInfo(childName);
                            if (fi.IsReadOnly)
                            {
                                fi.IsReadOnly = false; //更改文件的只读属性
                            }

                            File.Delete(childName); //直接删除其中的文件    
                        }

                        else//不是文件就是文件夹
                            DeleteDir(childName); //递归删除子文件夹   
                    }
                    System.IO.Directory.Delete(dir, true); //删除空文件夹  
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 计算文件夹大小
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static float ComputeDisk(String dic)
        {
            FileInfo fileInfo = null;
            long result = 0;
            var files = System.IO.Directory.GetFiles(dic, "*.*", System.IO.SearchOption.AllDirectories);
            foreach (var file in files)
            {
                fileInfo = new FileInfo(file);
                if (fileInfo.Exists)
                    result += fileInfo.Length;
            }
            return result / (1024 * 1024);
        }

        /// <summary>
        /// 获取文件版本
        /// </summary>
        public static String GetVersion(String fileName)
        {
            var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(fileName);
            return version.FileVersion;
        }
    }
}
