using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSys<TGridObject> : MonoBehaviour
{
    public event EventHandler<OnGridObjectChangedEventsArgs> OnGridOBJChanged;
    public class OnGridObjectChangedEventsArgs : EventArgs{
        public int x;
        public int y;
    }
    private int width;
    private int height;
    private TGridObject[,] gridArr;
    private float cellSize;
    private Vector3 textOffset;
    private Vector3 originPosition;

    //DEBUG
    private TextMesh[,] txtMeshArr;
   
    public GridSys(
        int width,
        int height,
        float cellSize,
        Vector3 originPosition,
        Func<GridSys<TGridObject>,int,int,TGridObject> createGridOBJ
        ){
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.textOffset = new Vector3(cellSize , cellSize) *0.5f;
        gridArr = new TGridObject [width,height];
        txtMeshArr = new TextMesh [width,height];
        // using lamda expression to inintialize gridArr
        for(int x = 0 ; x < gridArr.GetLength(0) ; x++){
            for(int y = 0 ; y < gridArr.GetLength(1) ; y++){
                gridArr[x,y] = createGridOBJ(this,x,y);
            }
        }
        //DEBUG
        Debug.Log(width + " " + height);

        //TRAVERSE Y FIRST TOP TO DOWN 
        //THEN MOVE NEXT X
        for(int x= 0 ; x < gridArr.GetLength(0) ; x++){
            for(int y= 0 ; y < gridArr.GetLength(1) ; y++){
                  txtMeshArr[x,y] = CreateWorldText(
                  gridArr[x,y].ToString(),
                  null,
                  GetWorldPosition(x,y) + textOffset * 0.5f,
                  8,
                  Color.white,
                  TextAnchor.MiddleCenter
                );
                //STRAIT LINE
                Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x,y+1),Color.white,100);
                Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x+1,y),Color.white,100);
            }
        }
        Debug.DrawLine(GetWorldPosition(0,height),GetWorldPosition(width,height),Color.white,100);
        Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height),Color.white,100);

        OnGridOBJChanged += (object sender, OnGridObjectChangedEventsArgs eventsArgs) =>{
            txtMeshArr[eventsArgs.x,eventsArgs.y].text = gridArr[eventsArgs.x,eventsArgs.y].ToString();
        };
    }
    public int GetWidth(){
        return this.width;
    }
    public int GetHeight(){
        return this.height;
    }
    private Vector3 GetWorldPosition(int x, int y){
        return (new Vector3(x, y) * cellSize) + originPosition;
    }
    // Reverse GetWorldPosition Vector mutiplication
    // x * num = new x
    // new x / num = x
    public void GetXY(Vector3 wordPosition,out int x, out int y){
        x = Mathf.FloorToInt( (wordPosition.x - originPosition.x) / cellSize); 
        y = Mathf.FloorToInt( (wordPosition.y - originPosition.y) / cellSize);
    }
    public float GetCellSize(){
        return this.cellSize;
    }
    public TGridObject GetGridOBJ(int x, int y){
        if(x >=  0 && y >=0 && x < width  && y < height){
            return gridArr[x,y];
        }
        return default(TGridObject);
    }
    public TGridObject GetGridOBJ(Vector3 worldPosition){
        GetXY(worldPosition,out int x,out int y);
        return GetGridOBJ(x,y);
    }
    public void TriggerOnChanged(int x,int y){
        Debug.Log("Trace1");
        OnGridOBJChanged?.Invoke(this, new OnGridObjectChangedEventsArgs { x = x, y = y });
    }
    //SET
    //NOTE: -1 MEAN ERROR DO NOT SET -1
    public void SetGridOBJ(int x,int y, TGridObject val){
        if(x >=  0 && y >=0 && x < width  && y < height)
        {
            gridArr[x,y] = val;
            txtMeshArr[x,y].text = gridArr[x,y].ToString();
            return;
        }
        Debug.LogError("Set Grid Val went Wrong pls Check");
        return;
    }
    public void SetGridOBJ(Vector3 worldPos, TGridObject val){
        int x;
        int y;
        GetXY(worldPos, out x, out y);
        Debug.Log(x+" "+y);
        SetGridOBJ(x, y, val);
    }

    //UTILITY function

    public static TextMesh CreateWorldText(
      string text,
      Transform parent = null,
      Vector3 localPosition = default(Vector3),
      int fontSize = 40,
      Color color = default,
      TextAnchor textAnchor = default,
      TextAlignment textAlignment = default,
      int sortingOrder = 0
    ){
        if(color == null) color = Color.white;
        return CreateWorldText(
            parent,
            text,
            localPosition,
            fontSize,
            (Color)color,
            textAnchor,
            textAlignment,
            sortingOrder
        );
    }
    public static TextMesh CreateWorldText(
      Transform parent,
      string text,
      Vector3 localPosition,
      int fontSize,
      Color color,
      TextAnchor textAnchor, 
      TextAlignment textAlignment,
      int sortingOrder
    ){
        GameObject gameObject = new GameObject("World_Text",typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
