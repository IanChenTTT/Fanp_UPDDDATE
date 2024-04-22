using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxNew : MonoBehaviour
{
    private float length;
    private float startPos;
    [SerializeField]
    private GameObject cam;
    private float parallaxEffect;

    void Start(){
        startPos = transform.position.x;

        //Return width of the image in world space
        length =  GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update(){
        float temp = (cam.transform.position.x * (1 -parallaxEffect));
        float dist =  cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        if(temp > startPos + length) startPos += length; 
        else if(temp < startPos - length) startPos -= length;
    }


}
