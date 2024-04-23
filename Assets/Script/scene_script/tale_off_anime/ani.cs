using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
public class ani : MonoBehaviour
{
    public float invo;
    // Start is called before the first frame update
    void Start()
    {

        Invo();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Invo(){
        Invoke("ChangeSc",invo);
    }
    public void ChangeSc(){
        SceneManager.LoadScene("smallgame_2");
    }
}
