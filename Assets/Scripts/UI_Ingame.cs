using System;
using System.Collections;
using UnityEngine;

public class UI_Ingame : SingletonBehaviour<UI_Ingame>
{

    public GameObject Intro;
    public GameObject StartMenu;
    public GameObject EndMenu;

    private bool allowSkip = false;

    private void OnEnable()
    {
        BeginIntroSequence();
    }

    private void BeginIntroSequence()
    {
        allowSkip = false;
        Cursor.visible = true;
        Intro.SetActive(true);
        StartMenu.SetActive(false);
        EndMenu.SetActive(false);

        StartCoroutine(SetAllowSkip());
    }

    private IEnumerator SetAllowSkip()
    {
        yield return new WaitForSeconds(3f);
        allowSkip = true;
    }

    public void BeginStartGameSequence()
    {
        if (allowSkip == false) return;
        
        allowSkip = false;
        Cursor.visible = true;
        Intro.SetActive(false);
        StartMenu.SetActive(true);
        EndMenu.SetActive(false);
        
        StartCoroutine(SetAllowSkip());
    }
    
    public void BeginIngameSequence()
    {
        if (allowSkip == false) return;
        
        Cursor.visible = false;
        Intro.SetActive(false);
        StartMenu.SetActive(false);
        EndMenu.SetActive(false);
    }

    public void BeginEndGameSequence()
    {
        Cursor.visible = true;
        Intro.SetActive(false);
        StartMenu.SetActive(false);
        EndMenu.SetActive(true);
    }
    
}
