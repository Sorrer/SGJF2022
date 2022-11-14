using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Common.ScriptableData
{
    // TODO: Implemented dirty data detection.
    // In theory, it should speed up saving by not having to serialize every block of data, and instead only serialize those that are needed
    // Basically would save a Dictionary of data that is wiped on start
    // If any save data is not found in the dictionary or if the field is marked as dirty, overwrite the dictionary
    // Save the dictioanry


    [CreateAssetMenu(fileName = "Scriptable Data Saver", menuName = ScriptableData<Object>.SCRIPTABLE_OBJECT_DATA_MENU_NAME + "Saver")]
    public class ScriptableDataSaver : UnityEngine.ScriptableObject
    {


        public ScriptableDataBase[] scriptableDataArray = {};

        private Dictionary<string, string> saveData = new(); //Save data consists of guid and the serialzied data
        
        public String saveFileName = "save"; // File to save this object as
        
        /// <summary>
        /// EDITOR ONLY
        /// Refreshes the stored array of scriptable data, use whenever new data needs to be saved, and other data does not.
        /// </summary>
        public void RefreshDatabase()
        {

#if UNITY_EDITOR

            int lastSize = scriptableDataArray != null ? scriptableDataArray.Length : 0;

            Debug.Log("Finding all savable scriptable data and syncing");

            string[] guids = AssetDatabase.FindAssets("t:" + typeof(ScriptableData<>).Name);

            List<ScriptableDataBase> data = new List<ScriptableDataBase>();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                var dataObj = AssetDatabase.LoadAssetAtPath<ScriptableDataBase>(path);
                if (dataObj.IsSavable())
                {
                    data.Add(dataObj);
                }
            }

            scriptableDataArray = data.ToArray();


            // TODO: Add more verbose details. For example, what objects were added, removed, etc.
            if (lastSize == data.Count)
            {
                Debug.Log($"Loaded total of {data.Count} scriptable data Objects. No size change");
            }
            else if (lastSize > data.Count)
            {
                Debug.Log($"Loaded total of {data.Count} scriptable data Objects. Removed {lastSize - data.Count}");
            }
            else
            {
                Debug.Log($"Loaded total of {data.Count} scriptable data Objects. Added {data.Count - lastSize}");
            }


            //Check for conflicts

            Dictionary<string, string> guidsCheck = new Dictionary<string, string>();

            foreach (var scriptableData in data)
            {
                if (guidsCheck.ContainsKey(scriptableData.GetGuid()))
                {
                    Debug.LogError($"Conflicting ID detected {guidsCheck[scriptableData.GetGuid()]} <-> {scriptableData.name}");
                    continue;
                }

                guidsCheck.Add(scriptableData.GetGuid(), scriptableData.name);
            }

#else

            Debug.LogError("Refresh Database should not be called in runtime, editor only");

#endif

        }

        /// <summary>
        /// Reads from a file, and returns the contents of file.
        /// </summary>
        /// <param name="path">File Path</param>
        /// <returns>String, null if error/file not found</returns>
        private string ReadFile(string path)
        {
            string data = null;

            try
            {
                StreamReader reader = new StreamReader(path, Encoding.Default);


                using (reader)
                {
                    data = reader.ReadToEnd();

                    reader.Close();
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.LogException(e);
            }

            return data;
        }

        /// <summary>
        /// Overwrites the file with the given string
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        private bool OverwriteFile(string path, string data)
        { 
            var dir = Path.GetDirectoryName(path);

            if (dir == null)
            {
                Debug.LogError("Failed to overwrite file, invalid dir! " + dir + " " + data);
            }
            
            bool exists = System.IO.Directory.Exists(dir);
            
            if(!exists)
                System.IO.Directory.CreateDirectory(dir);
                
            try
            {
                StreamWriter writer = new StreamWriter(path);

                using (writer)
                {
                    writer.Write(data);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }

            return true;
        }

        public static bool ForceClearSaveLocation(string saveName)
        {

            string savePath = GetSavePath(saveName);
            
            
            bool exists = Directory.Exists(savePath) || File.Exists(savePath);

            // No work needed
            if (!exists)
            {
                return true;
            }
            
            Exception e1 = null, e2 = null;
            try
            {
                Directory.Delete(savePath, true);
            }
            catch (Exception e)
            {
                e1 = e;
            }
            
            try
            {
                File.Delete(savePath);
            }
            catch (Exception e)
            {
                e2 = e;
            }

            if (e1 != null && e2 != null)
            {
                Debug.LogException(e1);
                Debug.LogException(e2);
                return false;
            }
            return true;
        }
        
        public static bool DeleteSave(string saveName)
        {
            string savePath = GetSavePath(saveName);

            if (savePath == null)
            {
                return false;
            }

            bool exists = Directory.Exists(savePath);


            if (!exists)
            {
                Debug.LogError("Tried to delete a save that does not exist");
                return false;
            }

            if (!File.GetAttributes(savePath).HasFlag(FileAttributes.Directory))
            {
                Debug.LogError("Delete save only works on a directory. Not given a directory");
                return false;
            }

            Directory.Delete(savePath, true);
            
            try
            {
                Directory.Delete(savePath, true);
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.LogError("Failed to delete - Unauthorized access " + e);
                return false;
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to delete - file in use " + e);
                return false;
            }
            catch (Exception e)
            {
                // Should not reach here
                Debug.LogException(e);
                throw;
            }
            return true;
        }

        public bool DeleteSaveFile(string saveName)
        {

            string savePath = GetSavePath(saveName, saveFileName);

            if (savePath == null)
            {
                return false;
            }
            
            bool exists = File.Exists(savePath);


            if (!exists)
            {
                Debug.LogError("Tried to delete a save that does not exist");
                return false;
            }

            if (File.GetAttributes(savePath).HasFlag(FileAttributes.Directory))
            {
                Debug.LogError("Tried to delete a folder for saves when needed to delete a file.");
                return false;
            }

            try
            {
                File.Delete(savePath);
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.LogError("Failed to delete - could not find the path " + e);
                return false;
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to delete - file in use " + e);
                return false;
            }
            catch (Exception e)
            {
                // Should not reach here
                Debug.LogException(e);
                throw;
            }
            
            return true;
        }

        public bool Exists(string saveName)
        {
            var savePath = GetSavePath(saveName, saveFileName);
            
            if (savePath == null)
            {
                return false;
            }

            return File.Exists(savePath);
        }


        // TODO: Add async functionality


        /// <summary>
        /// Saves all scriptable data (that is flagged as savable) into a file.
        /// </summary>
        /// <param name="saveName"></param>
        public bool Save(string saveName)
        {

            string path = GetSavePath(saveName, saveFileName);

            if (path == null)
            {
                return false;
            }

            foreach (var scriptableData in scriptableDataArray)
            {
                if (!saveData.ContainsKey(scriptableData.GetGuid()))
                {
                    saveData.Add(scriptableData.GetGuid(), scriptableData.Serialize());
                }
                else if (scriptableData.IsDirty())
                {
                    saveData[scriptableData.GetGuid()] = scriptableData.Serialize();
                    scriptableData.ClearDirtyFlag();
                }
            }


            string serialized = JsonConvert.SerializeObject(saveData);

            var res = OverwriteFile(path, serialized);
            
            Debug.Log("Saved to " + path + " (" + serialized.Length + ")");

            return res;
        }


        /// <summary>
        /// Deserializes all scriptable data (in database).
        /// </summary>
        /// <param name="saveName"></param>
        public bool Load(string saveName)
        {
            string path = GetSavePath(saveName, saveFileName);

            if (path == null || !File.Exists(path))
            {
                return false;
            }
            
            string data = ReadFile(path);

            if (data == null)
            {
                Debug.LogError($"Failed to load from file - {path}");
                return false;
            }

            saveData = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            if (saveData == null)
            {
                Debug.LogError("Error loading the save data.");
                return false;
            }


            foreach (var scriptableData in scriptableDataArray)
            {
                if (saveData.TryGetValue(scriptableData.GetGuid(), out var value))
                {
                    scriptableData.Deserialize(value);
                }
                else
                {
                    // TODO: Handle missing data, and garbage data that has not been retrieved through scriptable data
                    Debug.LogWarning("Save file could not load specific data. Maybe mismatch version?");
                }
            }

            return true;
        }


        private void OnEnable()
        {
            saveData.Clear();
        }

        private void OnDestroy()
        {
            saveData.Clear();
        }

        public static string GetSavePath(string saveName)
        {
            // If save file is empty, it ignores.

            if (saveName == "")
            {
                Debug.LogError("Tried to parse a invalid save name");
                return null;
            }
            
            return Path.Combine(Application.persistentDataPath, "Saves", saveName);
        }
        public static string GetSavePath(string saveName, string saveFile)
        {
            // If save file is empty, it ignores.

            if (saveName == "")
            {
                Debug.LogError("Tried to parse a invalid save name");
                return null;
            }
            
            if (saveFile == "")
            {
                Debug.LogError("Tried to parse a invalid save file name");
                return null;
            }
            
            return Path.Combine(Application.persistentDataPath, "Saves", saveName, saveFile);
        }
    }
}