using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Saliva : MonoBehaviour
{

    public enum SalivaLevel
    {
        None = 0,
        Low = 1,
        Mid = 2,
        Full = 3
    }
    
    public Animator Animator;
    public Collider2D Collider;
    public float SalivaIncreaseInterval = 10f;
    public float SuctionEffectInterval = 3f;

    private SalivaLevel level;
    private float salivaTimer;
    private float salivaSuctionTimer;

    private void OnEnable()
    {
        SetLevel(SalivaLevel.None);
    }

    private void Update()
    {
        
        salivaTimer += Time.deltaTime;
        salivaTimer = Mathf.Clamp(salivaTimer, 0f, SalivaIncreaseInterval + 1f);
        

        if (salivaTimer >= SalivaIncreaseInterval)
        {
            if (level == SalivaLevel.Full) return;
            SetLevel(level + 1);
            salivaTimer = 0f;
        }
    }

    private void SetLevel(SalivaLevel salivaLevel)
    {
        level = salivaLevel;
        Collider.enabled = level != SalivaLevel.None;
        Animator.SetTrigger(level.ToString());
    }

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        if (level == SalivaLevel.None) return;
        
        switch (toolType)
        {
            case ToolManager.ToolType.Vacuum:
                salivaSuctionTimer += Time.deltaTime * 5f;
                salivaTimer = 0f;
                if (salivaSuctionTimer >= SuctionEffectInterval)
                {
                    SetLevel(level - 1);
                    salivaSuctionTimer = 0f;
                }
                break;
        }
    }
    
    
}
