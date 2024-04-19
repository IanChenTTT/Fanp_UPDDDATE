using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progress_bar : MonoBehaviour
{
    public float fillAmount = 0;
    public float maxdis,mindis,player_x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        fillAmount = player_x/(maxdis-mindis);
        GetComponent<Image>().fillAmount = fillAmount;
    }
}
