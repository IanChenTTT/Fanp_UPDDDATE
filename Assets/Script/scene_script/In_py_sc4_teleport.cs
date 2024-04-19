using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class In_py_sc4_teleport : MonoBehaviour
{
    public GameObject TeleportItem,tele,trapmove;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {

        TeleportItem.SetActive(false);
        tele.SetActive(true);
        trapmove.SetActive(true);
    }
}
