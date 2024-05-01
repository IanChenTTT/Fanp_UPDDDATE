using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
public class Menu_event : MonoBehaviour
{
    public GameObject HowtoPlay_canvas;
    public GameObject CG;
    public float x;
    public float alpha=0;
    public bool alphaopen=false;

    [Header("Menu Navigations")]
    [SerializeField] private GameObject saveSlotMenuCanvas;
    [SerializeField] private GameObject mainMenuCanvas;

    [Header("Menu Buttons")]
    [SerializeField] private Button continueBTN;
    [SerializeField] private Button newGameBTN;
    
    void Start(){
        if(!DataPersistenceManager.instance.HasGameData())
        {
            continueBTN.interactable = false;
        }
    }
    void Update()
    {
        cg();
    }

    public void QuitGame()
    {
        Application.Quit();//bulid後才會有用
        //EditorApplication.isPlaying=false;//編輯狀態關閉執行
    }

    public void start_game_click()
    {
        //click遊戲開始跳轉道mainScene
    }
    public void open_HowToPlay_canvas()
    {
        HowtoPlay_canvas.SetActive(true);
    }

    public void close_HowToPlay_canvas()
    {
        HowtoPlay_canvas.SetActive(false);
    }
    public void activateMainMenu(){
        mainMenuCanvas.SetActive(true);
    }
public void deactivateMainMenu(){
        mainMenuCanvas.SetActive(false);
    }
    public void activateSaveslotMenu(bool isLoadingGame)
    {
        saveSlotMenuCanvas.SetActive(true);
        saveSlotMenuCanvas.GetComponent<SaveSlotMenu>().ActivateMenu(isLoadingGame);
    }
    public void startclick(){
        this.activateSaveslotMenu(false);
        this.deactivateMainMenu();
        //update判定與轉場景等待 ORIGIN
        // alphaopen=true;
        // CG.SetActive(true);
        // Invoke("startchange",3.5f);
    }
    public void StartScene(string sceneName){
        alphaopen=true;
        CG.SetActive(true);
        // Invoke("startchange",3.5f);
        Debug.Log(sceneName+"StartScene Before courtine");
        StartCoroutine(FadeAndLoadSceneRoutine(sceneName));
    }
    public void OncontinueClick(){
        this.activateSaveslotMenu(true);
        this.deactivateMainMenu();
    }
    IEnumerator FadeAndLoadSceneRoutine(string sceneName){
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadSceneAsync(sceneName);
    }
    // public void startchange(){ //DEBUG USE
    //     SceneManager.LoadScene("scene_ver1");
    // }
    public void cg(){
        //漸變黑暗
        if(alphaopen==true){
        alpha=alpha+x*Time.deltaTime;
        if(alpha<1){
            CG.GetComponent<Image>().color = new Color(CG.GetComponent<Image>().color.r,CG.GetComponent<Image>().color.g,CG.GetComponent<Image>().color.b,alpha);
        }
        else if(alpha>=1){
            alphaopen=false;
        }
    }     
    }
}
