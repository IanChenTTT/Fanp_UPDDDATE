using System.Collections.Generic;
using UnityEngine;
public class PathFind 
{
    private const int MOVE_STAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

   public static PathFind Instance{get;private set;}
   private GridSys<PathNode> grid;
   private List<PathNode> openList;
   private List<PathNode> closeList;
   public PathFind(int width, int height){
     if (Instance != null) //SingleTon 唯一物件
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            return;
        }
        Instance = this;
    grid = new GridSys<PathNode>(
        width,
        height,
        10f,
        Vector3.zero,
        (GridSys<PathNode> g, int x, int y) =>
        new PathNode(g,x,y)
    );
   }
   public GridSys<PathNode> GetGrid(){
    return grid;
   }

   public List<Vector3> FindPath(Vector3 startWorldPos, Vector3 endWorldPos){
    grid.GetXY(startWorldPos,out int startX,out int startY);
    grid.GetXY(endWorldPos,out int endX,out int endY);
    List<PathNode> path = FindPath(startX,startY,endX,endY);
    if(path == null){
        return null;
    }else{
        List<Vector3> vectorPath = new List<Vector3>();
        foreach(PathNode pathNode in path){
            vectorPath.Add(new Vector3(pathNode.x,pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
        }
        return vectorPath;
    }
   }
   public List<PathNode>FindPath(int startX, int startY,int endX, int endY){
    PathNode startNode = grid.GetGridOBJ(startX,startY);
    PathNode endNode = grid.GetGridOBJ(endX,endY);
    openList = new List<PathNode>{startNode};
    closeList = new List<PathNode>();
    for(int x = 0 ; x < grid.GetWidth() ; x++){
        for(int y = 0 ; y < grid.GetHeight() ; y++){
            PathNode pathNode = grid.GetGridOBJ(x,y);
            pathNode.gCost = int.MaxValue;
            pathNode.CalculateFcost();
            pathNode.cameFromNode = null;
        }
    }
    startNode.gCost = 0;
    startNode.hCost = CalculateDistance(startNode, endNode);
    startNode.CalculateFcost();
    while(openList.Count > 0){
        PathNode currentNode = GetLowestCostNode(openList);
        if(currentNode == endNode) //Reach final Node
            return CalculatePath(endNode);

        openList.Remove(currentNode);
        closeList.Add(currentNode);

        foreach(PathNode neighBourNode in GetNeighbourList(currentNode)){
            if(closeList.Contains(neighBourNode)) continue;
            if(!neighBourNode.WalkAble){
                closeList.Add(neighBourNode);
                continue;
            }
            int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighBourNode);
            if(tentativeGCost < neighBourNode.gCost){
                neighBourNode.cameFromNode = currentNode;
                neighBourNode.gCost = tentativeGCost;
                neighBourNode.hCost = CalculateDistance(neighBourNode,endNode);

                if(!openList.Contains(neighBourNode)){
                    openList.Add(neighBourNode);
                }
            }
        }
    }
    // Out of node on the openList
    return null;
   }
   private List<PathNode> GetNeighbourList(PathNode currentNode){
    List<PathNode> neighbourList = new List<PathNode>();
    if(currentNode.x - 1 >= 0){
        //Left
        neighbourList.Add(GetNode(currentNode.x-1,currentNode.y));
        //Left Down
        if(currentNode.y - 1 >= 0){
            neighbourList.Add(GetNode(currentNode.x-1,currentNode.y-1));
        }
        //Left Up
        if(currentNode.y + 1  < grid.GetHeight()){
            neighbourList.Add(GetNode(currentNode.x-1,currentNode.y+1));
        }
    }
    if(currentNode.x + 1 < grid.GetWidth()){
        // Right
        neighbourList.Add(GetNode(currentNode.x+1,currentNode.y));
        //Right Down
        if(currentNode.y - 1 >= 0){
            neighbourList.Add(GetNode(currentNode.x+1,currentNode.y-1));
        }
        //Right Up
        if(currentNode.y + 1  < grid.GetHeight()){
            neighbourList.Add(GetNode(currentNode.x+1,currentNode.y+1));
        }
    }
    //Down
    if(currentNode.y - 1  >= 0){
        neighbourList.Add(GetNode(currentNode.x,currentNode.y-1));
    }
    //UP
    if(currentNode.y + 1  < grid.GetHeight()){
        neighbourList.Add(GetNode(currentNode.x,currentNode.y+1));
    }
    return neighbourList;
   }
   private PathNode GetNode(int x,int y){
    return grid.GetGridOBJ(x,y);
   }
   //Traverse from end to begin
   //Return begin  to end List of Node
   private List<PathNode> CalculatePath(PathNode endNode){
    List<PathNode> path = new List<PathNode>{endNode};
    PathNode currentNode = endNode;
    while(currentNode.cameFromNode != null){
        path.Add(currentNode.cameFromNode);
        currentNode = currentNode.cameFromNode; //Next node Traverse
    }
    path.Reverse(); 
    return path;
   }
   //Return
   // Amount of it can move diagonal 
   // Add
   // Amount of it can move straight;
   private int CalculateDistance(PathNode a, PathNode b){
    int xDistance = Mathf.Abs(a.x - b.x);
    int yDistance = Mathf.Abs(a.y - b.y);
    int remaining = Mathf.Abs(xDistance - yDistance);
    return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) +
           MOVE_STAIGHT_COST * remaining;
   }
   private PathNode GetLowestCostNode(List<PathNode> pathNodeList){
    PathNode lowFnode = pathNodeList[0];
    foreach(PathNode node in pathNodeList){
        if(node.fCost < lowFnode.fCost)lowFnode = node;
    }
    return lowFnode;
   }
}
