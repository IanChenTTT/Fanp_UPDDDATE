using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_trap : MonoBehaviour
{
    public float xpoint,ypoint;
    public GameObject trap;
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
        Instantiate(trap,new Vector3(xpoint,ypoint,0), transform.rotation);
        this.gameObject.SetActive(false);
        Invoke("open_again",openagain_time);
    }
    public void open_again()
    {
        this.gameObject.SetActive(true);
    }
}
