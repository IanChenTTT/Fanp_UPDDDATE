using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadCheckPoint : MonoBehaviour
{
   // This is bootsrap function check DataPersistence exists;
   private bool CheckDataPersistenceManager(){
    return DataPersistenceManager.instance != null;
   }
   // This function was origin from SaveSlotMenu cs
   // Check if any error
   // Sorry ugly code 
   public void OnLoadOnDeath(){
        if(CheckDataPersistenceManager())
        {
            if(DataPersistenceManager.instance.HasGameData())
            {
                if(DataPersistenceManager.instance.HasSceneName())
                {
                    Debug.Log("It should load+1"+DataPersistenceManager.instance.CurrentScene());
                }
                    StartCoroutine(FadeAndLoadSceneRoutine(DataPersistenceManager.instance.CurrentScene()));
            }
        }
        else{
            // If data persistence manager didn't exist 
            // Disable current BTN
            Debug.Log("it end here");
            this.gameObject.SetActive(false);
        }
   }
    IEnumerator FadeAndLoadSceneRoutine(string sceneName){
        yield return new WaitForSeconds(0.2f);
        Debug.Log("It should load"+ sceneName);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
