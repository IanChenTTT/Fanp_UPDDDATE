using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIControll : MonoBehaviour
{
    public GameObject uiElement;

    void Update()
    {
        // 监测 Tab 键按下
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 显示 UI
            ShowUI();
        }

        // 监测 Tab 键抬起
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            // 关闭 UI
            HideUI();
        }
    }

    void ShowUI()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);
            Time.timeScale=0.3f;
        }
    }

    void HideUI()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(false);
            Time.timeScale=1.0f;
        }
    }
    
}
