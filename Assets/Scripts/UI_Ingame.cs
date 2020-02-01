using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ingame : MonoBehaviour
{

    public GameObject StartMenu;
    public GameObject EndMenu;

    private void OnEnable()
    {
        BeginStartGameSequence();
    }

    public void BeginStartGameSequence()
    {
        Cursor.visible = true;
        StartMenu.SetActive(true);
    }
    
    public void BeginIngameSequence()
    {
        Cursor.visible = false;
        StartMenu.SetActive(false);
    }

    public void BeginEndGameSequence()
    {
        
    }
    
}
