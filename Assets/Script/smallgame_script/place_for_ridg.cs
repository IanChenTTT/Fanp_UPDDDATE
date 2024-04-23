using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class place_for_ridg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 playerPosition = playerTransform.position;
        this.gameObject.transform.position=playerTransform.position;
        transform.rotation=playerTransform.rotation;
    }
}
