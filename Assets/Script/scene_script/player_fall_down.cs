using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player_fall_down : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
            SceneManager.LoadScene("player_dead");
        }
    }
}
