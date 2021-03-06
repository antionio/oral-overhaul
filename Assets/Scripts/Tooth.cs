﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using Random = System.Random;

public class Tooth : MonoBehaviour
{

    public enum ToothCondition
    {
        Healthy = 0,
        Cracked = 1,
        Shattered = 2,
        Decay = 3,
        Filled = 4,
        Broken = 5
    }
    
    [Header("Visuals")]
    public Animator Animator;
    public SpriteRenderer ToothSpriteRenderer;
    public Sprite[] ToothConditionSprites;
    public ParticleSystem ToohSpurtParticle;
    [Header("Settings")]
    public float ToothShatteredTime = 2f;

    private ToothCondition toothCondition = ToothCondition.Healthy;
    private bool numb = false;

    public ToothCondition GetToothCondition()
    {
        return toothCondition;
    }

    private float toothShatteredTimeCounter;

    private void OnEnable()
    {
        SetToothCondition(toothCondition);
        
        // about 50% chance that there is a cavity
        var random = UnityEngine.Random.Range(0, 100);
        if (random <= 50)
        {
            SetToothCondition(ToothCondition.Decay);
        }
        
        // about 10% chance that there is scrap on the tooth
        random = UnityEngine.Random.Range(0, 100);
        if (random <= 10)
        {
            AddToothScrap();
        }
        
        // about 5% chance that tooth is missing
        random = UnityEngine.Random.Range(0, 100);
        if (random < 5)
        {
            SetToothCondition(ToothCondition.Broken);
        }
    }

    private void AddToothScrap()
    {
        var scrap = GetComponentInChildren<Scrap>(true);
        scrap.gameObject.SetActive(true);
    }

    private void SetToothCondition(ToothCondition toothCondition)
    {
        this.toothCondition = toothCondition;
        
        if (toothCondition == ToothCondition.Broken)
        {
            ToothSpriteRenderer.sprite = null;
            return;
        }
        
        ToothSpriteRenderer.sprite = ToothConditionSprites[(int)toothCondition];
    }

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        switch (toolType)
        {
            case ToolManager.ToolType.Hand:
                if (toothCondition != ToothCondition.Healthy) {
                    Face.Instance.DoOuchie();
                    Screenshake.Instance.ScreenShake(0.1f, 0.025f);
                    Animator.SetTrigger("Hit");
                }
                break;
            case ToolManager.ToolType.Drill:

                if (toothCondition == ToothCondition.Broken) return;
                
                if (!numb) {
                    Face.Instance.DoOuchie(true);
                    Screenshake.Instance.ScreenShake(0.01f, 0.1f);
                    Animator.SetTrigger("Hit");
                }
                else
                {
                    Screenshake.Instance.ScreenShake(0.01f, 0.02f);
                }
                
                if (ToohSpurtParticle.isPlaying == false)
                    ToohSpurtParticle.Play();

                toothShatteredTimeCounter += Time.deltaTime;
                if (toothShatteredTimeCounter >= ToothShatteredTime)
                {
                    toothShatteredTimeCounter = 0f;
                    OnDrillTooth();
                }
                break;
            case ToolManager.ToolType.Filler:

                if (toothCondition != ToothCondition.Cracked) return;

                if (ToolManager.Instance.IsToolPrepared() == false) return;

                SetToothCondition(ToothCondition.Filled);
                ToolManager.Instance.PrepareTool(false);
                
                break;
            case ToolManager.ToolType.Mirror:
                if (numb) return;
                Face.Instance.DoOuchie();
                Animator.SetTrigger("Hit");
                break;
            case ToolManager.ToolType.Scraper:
                if (numb) return;
                Face.Instance.DoOuchie();
                Screenshake.Instance.ScreenShake(0.1f, 0.025f);
                Animator.SetTrigger("Hit");
                break;
            case ToolManager.ToolType.Syringe:
                if (numb) return;
                Face.Instance.DoOuchie();
                Screenshake.Instance.ScreenShake(0.1f, 0.025f);
                Animator.SetTrigger("Hit");
                numb = true;
                break;
        }
        
    }

    private void OnDrillTooth()
    {
        switch (toothCondition)
        {
            case ToothCondition.Healthy:
                SetToothCondition(ToothCondition.Cracked);
                break;
            case ToothCondition.Decay:
                SetToothCondition(ToothCondition.Cracked);
                break;
            case ToothCondition.Filled:
                SetToothCondition(ToothCondition.Cracked);
                break;
            case ToothCondition.Cracked:
                SetToothCondition(ToothCondition.Shattered);
                break;
            case ToothCondition.Shattered:
                SetToothCondition(ToothCondition.Broken);
                break;
            case ToothCondition.Broken:
                // do nothing
                break;
        }
    }
}
