public class PathNode 
{
    private GridSys<PathNode> grid;
    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost;
    public bool WalkAble;
    public PathNode cameFromNode;
    public PathNode(GridSys<PathNode>grid,int x,int y){
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.WalkAble = true;
    }
    public void CalculateFcost(){
        fCost = gCost + hCost;
    }
    public override string ToString(){
        return x + "," + y;
    }
}
