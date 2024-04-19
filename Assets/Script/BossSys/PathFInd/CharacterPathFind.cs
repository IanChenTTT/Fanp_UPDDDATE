using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathFind : MonoBehaviour
{
    int currentPathIndex = 0;
    List<Vector3> pathVectorList;

   private void Start(){
    this.pathVectorList = new List<Vector3>();
   }
   private void HandleMovement(){
    if(pathVectorList != null){
        Vector3 targetPos = pathVectorList[currentPathIndex];
        if(Vector3.Distance(transform.position, targetPos) > 1f){
            Vector3 move = (targetPos - transform.position).normalized;

            float distBefore = Vector3.Distance(transform.position, targetPos);
        }
    }
   }
   private void StopMoving(){
    pathVectorList = null;
   }
   public Vector3 GetPosition(){
    return transform.position;
   }
   public void SetTargetPosition(Vector3 targetPosition){
    currentPathIndex = 0;
    pathVectorList = PathFind.Instance.FindPath(GetPosition(),targetPosition);
    if(pathVectorList != null && pathVectorList.Count > 1){
        pathVectorList.RemoveAt(0);
    }
   }
}
