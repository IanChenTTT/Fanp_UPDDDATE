using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scene_event : MonoBehaviour
{
    public GameObject option_canvas;
    public bool IsOpened=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keydownESC();
    }

    public void open_option_canvas()
    {
        option_canvas.SetActive(true);
       Time.timeScale=0;//暫停
    }

    public void close_option_canvas()
    {
        option_canvas.SetActive(false);
        Time.timeScale=1;//恢復
    }

    public void backtoMainmenu()
    {
        Time.timeScale=1;//恢復
        SceneManager.LoadScene("MainMenu");
    }

    public void keydownESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //print("Esc");
            if(IsOpened==true)
            {
                open_option_canvas();
                IsOpened=false;
                //print("open");
            }
            else if(IsOpened==false)
            {
                close_option_canvas();
                IsOpened=true;
                //print("close");
            }
            
        }
    }
}
