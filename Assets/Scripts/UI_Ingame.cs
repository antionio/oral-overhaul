using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Ingame : SingletonBehaviour<UI_Ingame>
{

    [Header("Menus")]
    public GameObject Intro;
    public GameObject StartMenu;
    public GameObject InGame;
    public GameObject EndMenu;

    [Header("Music")]
    public AudioClip MusicIntro;
    public AudioClip MusicIngame;
    public AudioClip OralOverhaulIntro;
    public AudioSource OralOverhaulAudioSource;
    public AudioSource MusicAudioSource;

    private bool allowSkip = false;

    private void OnEnable()
    {
        BeginIntroSequence();
    }

    private void BeginIntroSequence()
    {
        MusicAudioSource.clip = MusicIntro;
        MusicAudioSource.Play();
        
        allowSkip = false;
        Cursor.visible = true;
        InGame.SetActive(false);
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
        
        OralOverhaulAudioSource.PlayOneShot(OralOverhaulIntro);
        
        allowSkip = false;
        Cursor.visible = true;
        InGame.SetActive(false);
        Intro.SetActive(false);
        StartMenu.SetActive(true);
        EndMenu.SetActive(false);
        
        StartCoroutine(SetAllowSkip());
    }
    
    public void BeginIngameSequence()
    {
        if (allowSkip == false) return;
        
        MusicAudioSource.clip = MusicIngame;
        MusicAudioSource.Play();
        
        Cursor.visible = false;
        InGame.SetActive(true);
        Intro.SetActive(false);
        StartMenu.SetActive(false);
        EndMenu.SetActive(false);
    }

    public void BeginEndGameSequence()
    {
        Cursor.visible = true;
        InGame.SetActive(false);
        Intro.SetActive(false);
        StartMenu.SetActive(false);
        EndMenu.SetActive(true);
    }

    public bool MenuActive()
    {
        return Intro.activeSelf || StartMenu.activeSelf || EndMenu.activeSelf;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
