using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Bdl.Utils
{
    public static class SaveSystem
    {
        // PUBLIC FIELDS
        private static readonly string SaveFolder = Application.dataPath + "/Saves/";
        private static readonly string SaveExtension = "txt";
        private static bool isInit = false;

        // BASE SAVE/LOAD METHODS

        public static void Init()
        {
            // Create save folder if doesnt exist
            if (!isInit)
            {
                if (!Directory.Exists(SaveFolder))
                    Directory.CreateDirectory(SaveFolder);
                isInit = true;
            }
        }

        public static void Save(string filename, string saveStr, bool overwrite = false)
        {
            Init();

            string finalFilename = filename;
            if (!overwrite)
            {
                // Make sure finalFilename is unique
                int saveNumber = 1;
                while (File.Exists(SaveFolder + finalFilename + "." + SaveExtension))
                    finalFilename = filename + saveNumber++;
            }

            File.WriteAllText(SaveFolder + finalFilename + "." + SaveExtension, saveStr);
        }

        public static string Load(string filename)
        {
            Init();

            if (File.Exists(SaveFolder + filename + "." + SaveExtension))
                return File.ReadAllText(SaveFolder + filename + "." + SaveExtension);
            else
                return null;
        }

        public static string LoadMostRecentFile()
        {
            Init();

            var directoryInfo = new DirectoryInfo(SaveFolder);
            var saveFiles = directoryInfo.GetFiles("*." + SaveExtension);
            FileInfo mostRecentFile = null;

            foreach (var saveFile in saveFiles)
            {
                if (mostRecentFile is null)
                    mostRecentFile = saveFile;
                else if (saveFile.LastWriteTime > mostRecentFile.LastWriteTime)
                    mostRecentFile = saveFile;
            }

            if (mostRecentFile != null)
                return File.ReadAllText(mostRecentFile.FullName);
            else
                return null;
        }

        // SAVE/LOAD METHODS FOR OBJECTS

        public static void SaveObject(string filename, object obj, bool overwrite = false)
        {
            Init();

            string json = JsonUtility.ToJson(obj);
            Save(filename, json, overwrite);
        }

        public static TSaveObject LoadObject<TSaveObject>(string filename)
        {
            Init();

            string saveStr = Load(filename);
            if (saveStr != null)
                return JsonUtility.FromJson<TSaveObject>(saveStr);
            else
                return default;
        }

        public static TSaveObject LoadMostRecentObject<TSaveObject>()
        {
            Init();

            string saveStr = LoadMostRecentFile();
            if (saveStr != null)
                return JsonUtility.FromJson<TSaveObject>(saveStr);
            else
                return default;
        }
    }
}
