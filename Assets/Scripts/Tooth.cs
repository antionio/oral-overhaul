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
        Broken = 4
    }
    
    [Header("Visuals")]
    public Animator Animator;
    public SpriteRenderer ToothSpriteRenderer;
    public Sprite[] ToothConditionSprites;
    public ParticleSystem ToohSpurtParticle;
    [Header("Settings")]
    public float ToothShatteredTime = 2f;

    private ToothCondition toothCondition = ToothCondition.Healthy;
    private float toothShatteredTimeCounter;

    private void OnEnable()
    {
        SetToothCondition(toothCondition);
        
        var random = UnityEngine.Random.Range(0, 100);
        if (random < 50)
        {
            SetToothCondition(ToothCondition.Decay);    
        }
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
                Screenshake.Instance.ScreenShake(0.05f, 0.02f);
                break;
            case ToolManager.ToolType.Drill:

                if (toothCondition == ToothCondition.Broken) return;
                
                ToohSpurtParticle.Play();
                Screenshake.Instance.ScreenShake(0.01f, 0.1f);

                toothShatteredTimeCounter += Time.deltaTime;
                if (toothShatteredTimeCounter >= ToothShatteredTime)
                {
                    toothShatteredTimeCounter = 0f;
                    OnDrillTooth();
                }
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
