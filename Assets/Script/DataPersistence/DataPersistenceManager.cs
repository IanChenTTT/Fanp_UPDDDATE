using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("Debugging")] //要不要不用儲存
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")] //請使用 XXX.json
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";

    public static DataPersistenceManager instance { get; private set; }

    private void Awake() 
    {
        if (instance != null) //SingleTon 唯一物件
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject); //轉換場僅不摧毀

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        Debug.Log(Application.persistentDataPath);
    }

    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // SceneManager.sceneUnloaded += OnSceneUnloaded; //呼叫函式 QUIT ON SAVE
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }
    public void NewGame() 
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //DEBUG!
        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (this.gameData == null && initializeDataIfNull) 
        {
            NewGame();
        }
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(selectedProfileId);
        
          // if no data can be loaded, don't continue
        if (this.gameData == null) 
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void ChangeSelectedProfileId(string newProfileId) 
    {
        // update the profile to use for saving and loading
        this.selectedProfileId = newProfileId;
        // load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }

    public void SaveGame()
    {

        // if we don't have any data to save, log a warning here
        if (this.gameData == null) 
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }
        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId);
    }

    private void OnApplicationQuit() 
    {
        SaveGame();
    }
   
    private List<IDataPersistence> FindAllDataPersistenceObjects() 
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public Dictionary<string, GameData> GetAllProfilesGameData(){
        return dataHandler.LoadAllProfiles();
    }
    public bool HasGameData() 
    {
        return gameData != null;
    }
    public bool HasSceneName(){
        return (gameData.playerScene == "scene_ver1") || (gameData.playerScene == "small_game1") || (gameData.playerScene == "smallgame_2");
    }
    public string CurrentScene(){
        return gameData.playerScene;
    }
}
