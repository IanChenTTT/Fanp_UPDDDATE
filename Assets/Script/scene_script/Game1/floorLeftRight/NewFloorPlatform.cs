using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFloorPlatform : MonoBehaviour
{
    [SerializeField]private float FloorSpeed;
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform  _pointB;
    [SerializeField] private Transform  _CurrentTarget;

    void FixedUpdate(){
        var _step = FloorSpeed * Time.deltaTime;
        if(transform.position == _pointA.position)
            _CurrentTarget = _pointB;
        else if(transform.position == _pointB.position)
            _CurrentTarget = _pointA;
        transform.position = Vector3.MoveTowards(transform.position, _CurrentTarget.position, _step);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            other.transform.parent = this.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            other.transform.parent = null;
        }
    }
}
