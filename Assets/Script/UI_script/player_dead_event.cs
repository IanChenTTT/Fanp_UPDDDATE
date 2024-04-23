using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_dead_event : MonoBehaviour
{
    public GameObject back_Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("btn_show",3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void backtoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void btn_show()
    {
        back_Canvas.SetActive(true);
    }
}
