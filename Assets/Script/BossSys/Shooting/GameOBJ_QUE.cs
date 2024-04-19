using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameOBJ_QUE
{
    //READONLY
    //USE COMPOSITION AND REF 
   private Queue<GameObject> QueOBJ;
   public int CAPACITY {get;private set;}

   public GameOBJ_QUE(int capacity){
    this.CAPACITY = capacity;
    QueOBJ = new Queue<GameObject>(capacity);   
   }
   public  GameObject DequeOBJ(){
    //If empty do nothing
    if(QueOBJ.Count <= 0 ) return null;
    return QueOBJ.Dequeue(); 
   }
    public bool EnqueOBJ(GameObject obj){
        //If it's full then
        if(QueOBJ.Count >= CAPACITY)return false;
        QueOBJ.Enqueue(obj);
        return true;
    }
}
