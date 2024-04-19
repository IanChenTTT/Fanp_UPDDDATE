using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sound_slider : MonoBehaviour
{
    public Slider slider;
    public Toggle toggle;
    public AudioSource BGsound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ControlAudio()
    {
        if(toggle.isOn)
        {
            BGsound.gameObject.SetActive(true);
            Volume();
        }
        else
        {
            BGsound.gameObject.SetActive(false);
        }
    }
    public void Volume()
    {
        BGsound.volume=slider.value;
    }
}
