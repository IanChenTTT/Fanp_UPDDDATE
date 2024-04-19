using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGtrans : MonoBehaviour
{
    private Material mat;
    [SerializeField] private Vector2 movingspeed;
    private Vector2 offset;

    private void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = movingspeed * Time.deltaTime;
        mat.mainTextureOffset += offset;
    }

}
