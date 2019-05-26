// <copyright file="FileHelper.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Common
{
    using System.IO;

    /// <summary>
    /// File Helper
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Encrypt File
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <returns>True or False</returns>
        public static bool EncryptFile(string filePath)
        {
            bool result = false;

            if (File.Exists(filePath))
            {
                File.Encrypt(filePath);
            }

            return result;
        }

        /// <summary>
        ///     Decrypt File
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <returns>True or False</returns>
        public static bool DecryptFile(string filePath)
        {
            bool result = false;

            if (File.Exists(filePath))
            {
                File.Decrypt(filePath);
            }

            return result;
        }

        /// <summary>
        ///     Delete File
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <returns>True or False</returns>
        public static bool DeleteFile(string filePath)
        {
            bool result = false;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                result = true;
            }

            return result;
        }

        /// <summary>
        ///     Save File
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <param name="contents">File Contents</param>
        /// <returns>True or False</returns>
        public static bool SaveFile(string filePath, string contents)
        {
            bool result = false;

            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, contents);
                result = true;
            }

            return result;
        }

        /// <summary>
        ///     Read File
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <returns>True or False</returns>
        public static string ReadFile(string filePath)
        {
            string result = string.Empty;

            if (File.Exists(filePath))
            {
                result = File.ReadAllText(filePath);
            }

            return result;
        }

        /// <summary>
        ///     Create File Stream
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <returns>FileStream created</returns>
        public static FileStream CreateFileStream(string filePath)
        {
            return File.Create(filePath);
        }

        /// <summary>
        ///     Move File
        /// </summary>
        /// <param name="oldFilePath">Old File Path</param>
        /// <param name="newFilePath">New File Path</param>
        public static void MoveFile(string oldFilePath, string newFilePath)
        {
            File.Move(oldFilePath, newFilePath);
        }
    }
}