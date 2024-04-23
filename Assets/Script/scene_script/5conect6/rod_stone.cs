using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rod_stone : MonoBehaviour
{
    public GameObject rod,stonestand;
    Animator rod_ani,stone_ani;
    // Start is called before the first frame update
    void Start()
    {
        rod_ani=rod.GetComponent<Animator>();
        stone_ani=stonestand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {

            rod_ani.Play("rod_use");
            stone_ani.Play("stonecanStand");
    }

}
