using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxNew : MonoBehaviour
{
    private float length;
    private float startPos;
    private bool IsParallax;
    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject StartParallax;

    [SerializeField]
    private GameObject EndParallax;

    [SerializeField]
    private float parallaxEffect;

    void Start()
    {
        this.IsParallax = true;
        startPos = transform.position.x;

        //Return width of the image in world space
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = cam.transform.position.x * parallaxEffect;
        if (EndParallax != null && StartParallax != null)
        {
            if (Player.transform.position.x >= EndParallax.transform.position.x || Player.transform.position.x <= StartParallax.transform.position.x)
                IsParallax = false;
            else
                IsParallax = true;
        }
        if (IsParallax)
            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }


}