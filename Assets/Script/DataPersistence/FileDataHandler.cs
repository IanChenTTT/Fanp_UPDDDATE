using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName) 
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId) 
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath,profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath)) 
        {
            try 
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                        Debug.Log(dataToLoad);
                    }
                }

                // deserialize the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e) 
            {
                Debug.Log(loadedData);
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data,string profileId) 
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath,profileId, dataFileName);
        try 
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into Json
            string dataToStore = JsonUtility.ToJson(data, true);

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
     public Dictionary<string, GameData> LoadAllProfiles(){
        Dictionary<string, GameData> profileDict = new Dictionary<string, GameData>();
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in directoryInfos)
        {
            string profileId = dirInfo.Name;
            
            //CHECK FILE EXIST IN THE DIRECTORY OR NOT
            //IF NOT THEN SKIP THIS FOLDER
            string fullPath = Path.Combine(dataDirPath,profileId,dataFileName);
            if(!File.Exists(fullPath)){
                Debug.LogWarning
                ("skipping directory when loading all profiles bevause it does not contain data"+profileId);
                continue;
            }
            //LOAD DATA FROM FILE
            GameData profileData = Load(profileId);
            if(profileData != null)
            {
                profileDict.Add(profileId,profileData);
            }
            else{
                Debug.LogError("Tried to load profile but something went wrong"+profileId);
            }
        }
        return profileDict;
    }
}