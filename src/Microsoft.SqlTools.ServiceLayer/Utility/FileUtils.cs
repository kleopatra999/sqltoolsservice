//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
using System;
using System.IO;
namespace Microsoft.SqlTools.ServiceLayer.QueryExecution
{
    internal static class FileUtils
    {
        internal static string PeekDefinitionTempFolder = Path.GetTempPath() + "mssql_definition"; 
        internal static bool PeekDefinitionTempFolderCreated = false;

        internal static string GetPeekDefinitionTempFolder()
        {
            string tempPath;
            if (!PeekDefinitionTempFolderCreated)
            {               
                try
                {
                    // create new temp folder
                    string tempFolder = string.Format("{0}_{1}", FileUtils.PeekDefinitionTempFolder, DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                    DirectoryInfo tempScriptDirectory = Directory.CreateDirectory(tempFolder);
                    FileUtils.PeekDefinitionTempFolder = tempScriptDirectory.FullName;
                    tempPath = tempScriptDirectory.FullName;
                    PeekDefinitionTempFolderCreated = true;
                }
                catch (Exception)
                {
                    // swallow exception and use temp folder to store scripts
                    tempPath = Path.GetTempPath();
                }
            }
            else
            {
                try
                {
                    // use tempDirectory name created previously
                    DirectoryInfo tempScriptDirectory = Directory.CreateDirectory(FileUtils.PeekDefinitionTempFolder);
                    tempPath = tempScriptDirectory.FullName;
                }
                catch (Exception)
                {
                    // swallow exception and use temp folder to store scripts
                    tempPath = Path.GetTempPath();
                }
            }
            return tempPath;
        }

        /// <summary>
        /// Checks if file exists and swallows exceptions, if any
        /// </summary>
        /// <param name="path"> path of the file</param>
        /// <returns></returns>
        internal static bool SafeFileExists(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch (Exception)
            {
                // Swallow exception
                return false;
            }
        }

        /// <summary>
        /// Deletes a file and swallows exceptions, if any
        /// </summary>
        /// <param name="path"></param>
        internal static void SafeFileDelete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                // Swallow exception, do nothing
            }
        }

        internal static int WriteWithLength(Stream stream, byte[] buffer, int length)
        {
            stream.Write(buffer, 0, length);
            return length;
        }

        /// <summary>
        /// Checks if file exists and swallows exceptions, if any
        /// </summary>
        /// <param name="path"> path of the file</param>
        /// <returns></returns>
        internal static bool SafeDirectoryExists(string path)
        {
            try
            {
                return Directory.Exists(path);
            }
            catch (Exception)
            {
                // Swallow exception
                return false;
            }
        }


        /// <summary>
        /// Deletes a directory and swallows exceptions, if any
        /// </summary>
        /// <param name="path"></param>
        internal static void SafeDirectoryDelete(string path, bool recursive)
        {
            try
            {
                Directory.Delete(path, recursive);
            }
            catch (Exception)
            {
                // Swallow exception, do nothing
            }
        }

    }
}