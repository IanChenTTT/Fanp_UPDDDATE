using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
   [Header("profile")]
   [SerializeField]private string profileId = "";

   [Header("Content")]
   [SerializeField]private GameObject noDataOBJ;
   [SerializeField]private GameObject hasDataOBJ;
   [SerializeField]private Text percentComplete;
  public bool hasData { get; private set; } = false;
    private Button saveSlotButton;

    private void Awake() 
    {
        saveSlotButton = this.GetComponent<Button>();
    }
   public void setData(GameData data){
        if(data == null)
        {
            hasData = false;
            noDataOBJ.SetActive(true);
            hasDataOBJ.SetActive(false);
        }
        else{
            hasData = true;
            noDataOBJ.SetActive(false);
            hasDataOBJ.SetActive(true);
        }
   }
    public string GetProfileId(){
        return this.profileId;
    }
     public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
        // clearButton.interactable = interactable; //TODO
    }
}
