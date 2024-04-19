using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour
{
    public GameObject player,Cavas_playway,canvas_countdown,progress_bar;
    int time_int =3;
    public Text time_UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void clickstart()
    {
        InvokeRepeating("timer", 1, 1);
    }

    void timer()
    {
        time_int -= 1;
        time_UI.text = time_int + "";
        if(time_int==0)
        {
            time_UI.text="GO!";
            Invoke("close_open",1.0f);
            CancelInvoke("timer");
        }
    }

    void close_open()
    {
        player.SetActive(true);
        Cavas_playway.SetActive(false);
        canvas_countdown.SetActive(false);
        progress_bar.SetActive(true);
    }
}
