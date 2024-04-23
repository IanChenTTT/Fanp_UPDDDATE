using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSys<HeatMapGridOBJ> gridSys;
    private void Start(){
        gridSys = new GridSys<HeatMapGridOBJ>(4,2,5f,new Vector3(20,0),(GridSys<HeatMapGridOBJ>g,int x,int y)=> new HeatMapGridOBJ(g, x, y));
    }
    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = GetMouseWorldPosition();
            HeatMapGridOBJ heatMapGridOBJ = gridSys.GetGridOBJ(pos);
            heatMapGridOBJ?.AddValue(45);
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
public class HeatMapGridOBJ{
    private const int MIN = 0;
    private const int MAX = 100;
    private int x;
    private int y;
    public int value;
    private GridSys<HeatMapGridOBJ> grid;
    public HeatMapGridOBJ(GridSys<HeatMapGridOBJ>grid,int x, int y){
        this.grid = grid;
        this.x  = x;
        this.y = y;
    }
    public void AddValue(int addVal){
        value += addVal;
        Math.Clamp(value, MIN, MAX);
        grid.TriggerOnChanged(x,y);
    }
    public float GetValNormalized(){
        return (float)value / MAX;
    }
    public override string ToString()
    {
        return value.ToString();
    }

}
