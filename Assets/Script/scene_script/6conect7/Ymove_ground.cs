using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ymove_ground : MonoBehaviour
{
    public float high;
    private int colicount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        colicount++;
        if(colicount<=1)
        {
this.gameObject.transform.position += new Vector3(0, high, 0);
        }
        
    }
}
