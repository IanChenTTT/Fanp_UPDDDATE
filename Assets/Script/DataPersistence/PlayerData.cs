using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour,IDataPersistence
{
    /* 重要!!
     * 假如儲存scene等於現在scene，Load 玩家位置
     * 不然先Load場景，遞迴執行Load data 讀玩家位置
    */
    public void LoadData(GameData data){
        if(data.playerScene == SceneManager.GetActiveScene().name) 
            this.transform.position = data.playerPosition;
    }
    public void SaveData(GameData data){
        if(this != null) //如果PlayerData腳本有在scene裡才儲存
        {
            data.playerPosition = this.transform.position;
            data.playerScene = SceneManager.GetActiveScene().name;
        }
    }
}
