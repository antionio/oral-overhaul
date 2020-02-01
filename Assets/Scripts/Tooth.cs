using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class Tooth : MonoBehaviour
{

    public enum ToothCondition
    {
        Healthy = 0,
        Cracked = 1,
        Shattered = 2,
        Broken = 3
    }
    
    public Animator Animator;
    public SpriteRenderer ToothSpriteRenderer;
    public Sprite[] ToothConditionSprites;
    public ParticleSystem ToohSpurtParticle;
    public float ToothShatteredTime = 2f;

    private ToothCondition toothCondition = ToothCondition.Healthy;
    private float toothShatteredTimeCounter;
    
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
                    DecayToothCondition();
                }
                break;
        }
        
    }

    private void DecayToothCondition()
    {
        var nextCondition = ((int) toothCondition + 1);
        if (nextCondition > (int) ToothCondition.Broken)
        {
            return;
        }

        toothCondition = (ToothCondition)nextCondition;

        if (toothCondition == ToothCondition.Broken)
        {
            ToothSpriteRenderer.sprite = null;
            return;
        }
        
        ToothSpriteRenderer.sprite = ToothConditionSprites[nextCondition];
    }
}
