using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class air_tf : MonoBehaviour
{
        private Animator ani ;
    // Start is called before the first frame update
    void Start()
    {
            ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
private void OnTriggerExit2D(Collider2D other) {
             ani.SetBool("onair",true);
}
    private void OnTriggerEnter2D(Collider2D other) {

         ani.SetBool("onair",false);
    }
}
