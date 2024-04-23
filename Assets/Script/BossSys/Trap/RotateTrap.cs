using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrap : MonoBehaviour
{
    private Coroutine rotatingCoro;
    public bool isRotating = false;
    public bool isEnter = false;
    // speed & amount must be positive, change axis to control direction
    // speed & amount must be positive, change axis to control direction
    IEnumerator DoRotation(float speed, float amount, Vector3 axis)
    {
        isRotating = true;
        float rot = 0f;
        while (rot < amount)
        {
            yield return null;
            float delta = Mathf.Min(speed * Time.deltaTime, amount - rot);
            transform.RotateAround(this.transform.position, axis, delta);
            rot += delta;
        }
        isRotating = false;
    }
    void Start()
    {

    }
    void Update()
    {
        if (!isRotating && isEnter)
        {
            StartCoroutine(DoRotation(50f, 180f, Vector3.back));
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("enter");
        if (col.gameObject.CompareTag("Player"))
        {
            isRotating = true;
        }
    }
}
