using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class in_py_sc4_trapmove : MonoBehaviour
{
    public GameObject trapL,trapR;
    public float trapl,trapr;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trap_fall();
    }
    public void trap_fall(){

            trapR.transform.Translate(-trapr,0,0);
            trapL.transform.Translate(trapl,0,0);


    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        trapL.SetActive(false);
        trapR.SetActive(false);
    }
}
