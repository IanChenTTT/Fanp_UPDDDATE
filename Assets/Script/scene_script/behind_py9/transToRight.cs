using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transToRight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rock,Left,tran;
    public float right;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trans();
    }
    public void trans(){
        rock.transform.Translate(right,0,0);
        Left.SetActive(true);
        tran.SetActive(false);
    }
}
