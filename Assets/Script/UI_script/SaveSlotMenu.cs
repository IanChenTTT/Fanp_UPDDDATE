using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotMenu : MonoBehaviour
{
     [Header("Menu Navigations")]
    [SerializeField] private Menu_event  menu_Event;

    private SaveSlot[] saveSlots;
    private bool isLoadingGame = false;
    private void Awake(){
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }
    public void OnBackClicked(){
        menu_Event.activateMainMenu();
        this.DeactivateMenu();
    }
    //DEBUG 這會讓你每次都new game!!!!!!!測試用
    // void Start(){
    //     this.ActivateMenu(false);
    // }
    public void ActivateMenu(bool isLoadingGame){
        this.gameObject.SetActive(true);
        this.isLoadingGame = isLoadingGame;
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();
        foreach(SaveSlot saveSlot in saveSlots)
        {
           GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.setData(profileData);
            if (profileData == null && isLoadingGame) 
            {
                saveSlot.SetInteractable(false);
            }
            else 
            {
                saveSlot.SetInteractable(true);
            }
        }
    }
    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
       
        // case - loading game
        if (isLoadingGame) 
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
             // save the game anytime before loading a new scene
            DataPersistenceManager.instance.SaveGame();
            if(DataPersistenceManager.instance.HasSceneName())
                menu_Event.GetComponent<Menu_event>().StartScene(DataPersistenceManager.instance.CurrentScene());
        }
        else 
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            DataPersistenceManager.instance.NewGame();
            DataPersistenceManager.instance.SaveGame();
            menu_Event.GetComponent<Menu_event>().StartScene("scene_ver1");
        }
    }
    public void DeactivateMenu(){
        this.gameObject.SetActive(false);
    }
}
