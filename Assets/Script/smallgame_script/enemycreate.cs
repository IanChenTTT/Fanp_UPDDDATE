using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemycreate : MonoBehaviour
{
    public int create_count;
    public float xpoint;
    public GameObject bird1,bird2;
    public bool chose_bird1=false,chose_bird2=false;
    public int openagain_time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {

            if(chose_bird1==true)
        {
            for(int i=0;i<=create_count;i++)
            {
                Instantiate(bird1,new Vector3(xpoint,Random.Range(-37f,38f),0), transform.rotation);
                xpoint=xpoint+12;
            }
        }

        if(chose_bird2==true)
        {
            for(int i=0;i<=create_count;i++)
            {
                Instantiate(bird2,new Vector3(xpoint,Random.Range(-37f,38f),0), transform.rotation);
                xpoint=xpoint+12;
            }
        }
        this.gameObject.SetActive(false);
        Invoke("open_again",openagain_time);

        
    }
    public void open_again()
    {
        this.gameObject.SetActive(true);
    }
}
