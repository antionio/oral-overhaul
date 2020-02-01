using UnityEngine;

public class UI_Ingame : SingletonBehaviour<UI_Ingame>
{

    public GameObject Intro;
    public GameObject StartMenu;
    public GameObject EndMenu;

    private void OnEnable()
    {
        BeginIntroSequence();
    }

    private void BeginIntroSequence()
    {
        Cursor.visible = true;
        Intro.SetActive(true);
        StartMenu.SetActive(false);
        EndMenu.SetActive(false);
    }

    public void BeginStartGameSequence()
    {
        Cursor.visible = true;
        Intro.SetActive(false);
        StartMenu.SetActive(true);
        EndMenu.SetActive(false);
    }
    
    public void BeginIngameSequence()
    {
        Cursor.visible = false;
        Intro.SetActive(false);
        StartMenu.SetActive(false);
        EndMenu.SetActive(false);
    }

    public void BeginEndGameSequence()
    {
        
    }
    
}
