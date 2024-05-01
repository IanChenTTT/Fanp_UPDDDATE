using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveingPole : MonoBehaviour
{
    [SerializeField]
    private GameObject canvaHint;
    private bool PlayerEnter;
    private int saveCounter; //TODO
    private Animator saveAnimator;
    private IEnumerator coroutine;

    void Awake()
    {
        this.saveAnimator = this.GetComponent<Animator>();
        this.PlayerEnter = false;
        saveCounter = 0;
    }
    private void Update(){
        if (Input.GetKeyDown(KeyCode.E) && PlayerEnter){
            DataPersistenceManager.instance.SaveGame();    
            this.saveAnimator.Play("savepoint");
            canvaHint.transform.Find("HintTxt").gameObject.SetActive(false);
            canvaHint.transform.Find("SuccessTxt").gameObject.SetActive(true);
            coroutine = successSave();
            StartCoroutine(coroutine);
            Debug.Log("Success save");
        }

    }
    IEnumerator successSave(){
         // suspend execution for 5 seconds
        yield return new WaitForSeconds(1f);
        canvaHint.transform.Find("SuccessTxt").gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        this.PlayerEnter = true;
        canvaHint.transform.Find("HintTxt").gameObject.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        this.PlayerEnter = false;
        canvaHint.transform.Find("HintTxt").gameObject.SetActive(false);
    }
}
