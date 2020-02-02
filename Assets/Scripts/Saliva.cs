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

    public AudioSource AudioSource;
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
        if (UI_Ingame.Instance.MenuActive()) return;
        
        salivaTimer += Time.deltaTime;
        salivaTimer = Mathf.Clamp(salivaTimer, 0f, SalivaIncreaseInterval + 1f);

        if (salivaTimer >= SalivaIncreaseInterval)
        {
            if (level == SalivaLevel.Full) return;
            SetLevel(level + 1);
            salivaTimer = 0f;
        }

        if (level == SalivaLevel.Mid || level == SalivaLevel.Full)
        {
            if (AudioSource.isPlaying == false)
            {
                AudioSource.Play();
            }

            if (level == SalivaLevel.Mid)
            {
                AudioSource.volume = 0.6f;
            }
            else
            {
                AudioSource.volume = 1f;
            }
        } else if (level == SalivaLevel.Low || level == SalivaLevel.None)
        {
            if (AudioSource.isPlaying)
            {
                AudioSource.Stop();
            }
        }
    }

    private void SetLevel(SalivaLevel salivaLevel)
    {
        level = salivaLevel;
        Animator.SetTrigger(level.ToString());
    }

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        
        switch (toolType)
        {
            case ToolManager.ToolType.Vacuum:
                if (level == SalivaLevel.None) break;
                salivaSuctionTimer += Time.deltaTime * 5f;
                salivaTimer = 0f;
                if (salivaSuctionTimer >= SuctionEffectInterval)
                {
                    SetLevel(level - 1);
                    salivaSuctionTimer = 0f;
                }
                break;
            case ToolManager.ToolType.Waterer:
                salivaTimer += Time.deltaTime * 5f;
                break;
        }
    }
    
    
}
