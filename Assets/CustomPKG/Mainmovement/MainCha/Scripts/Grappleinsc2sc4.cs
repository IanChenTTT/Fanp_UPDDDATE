using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappleinsc2sc4 : MonoBehaviour
{
    public GameObject modGrapple;
    private void Start() {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;

        if (otherGameObject.name == "in_py_sc1TOin_py_sc2")
        {
           modGrapple.SetActive(true);
        }
        if (otherGameObject.name == "in_py_sc2TOin_py_sc3")
        {
           modGrapple.SetActive(false);
        }
        if (otherGameObject.name == "in_py_sc3TOin_py_sc4")
        {
           modGrapple.SetActive(true);
        }
        if(otherGameObject.name == "in_py_sc4_teleport"){
            modGrapple.SetActive(false);
        }
    }

}
