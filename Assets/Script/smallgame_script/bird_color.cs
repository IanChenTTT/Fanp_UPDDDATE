using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_color : MonoBehaviour
{
    
    private SpriteRenderer sp;
    public float rn;
    // Start is called before the first frame update
    void Start()
    {
        sp=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rn = Random.Range(0, 255);
        sp.color = new Color(rn,rn,rn);
        print(rn);
    }

}
