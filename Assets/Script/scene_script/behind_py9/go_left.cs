using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class go_left : MonoBehaviour
{
    public GameObject rock,Left,trans;
    public float Leftspeed,Left_x,Right_x;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rockleft();
    }
    public void Rockleft(){
        if(rock.transform.position.x<=Right_x&&rock.transform.position.x>Left_x||rock.transform.position.x>Right_x){    
            rock.transform.Translate(Leftspeed*Time.deltaTime,0,0);
            
        }
        if(rock.transform.position.x<=Left_x){
            Left.SetActive(false);
            trans.SetActive(true);
        }
    }
}
