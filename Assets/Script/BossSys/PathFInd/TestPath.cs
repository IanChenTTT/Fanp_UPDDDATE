using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestPath : MonoBehaviour
{
    PathFind pathFind;
    void Start()
    {
        pathFind = new PathFind(10,10);
    }
    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            pathFind.GetGrid().GetXY(mouseWorldPos,out int x,out int y);
            List<PathNode> path = pathFind.FindPath(0,0,x,y);
            if(path != null){
                for(int i = 0 ; i < path.Count-1 ; i++){
                    Debug.DrawLine(new Vector3(path[i].x,path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x,path[i+1].y) *10f + Vector3.one * 5f);
                    Debug.Log("x:"+x+" "+"y:"+y);
                    Debug.Log("draw:"+path[i].x+" "+"y:"+path[i].y);
                }
            }
        }
    }
    public static Vector3 GetMouseWorldPosition(){
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(){
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera){
        return  GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(
        Vector3 screenPostion,
        Camera worldCamera
    ){
        Vector3 worldPoistion = worldCamera.ScreenToWorldPoint(screenPostion);
        return worldPoistion;
    }
}
