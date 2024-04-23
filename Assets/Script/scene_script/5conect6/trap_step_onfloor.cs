using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_step_onfloor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject floor_trap,pike_fall;
    Animator floor_trap_ani,pike_ani;
    void Start()
    {
        floor_trap_ani=floor_trap.GetComponent<Animator>();
        pike_ani=pike_fall.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        floor_trap_ani.Play("floor_step_trap");
        pike_ani.Play("pike_down");
    }
}
