using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{

    public Animator Animator;
    public ParticleSystem ParticleSystem;
    
    public void OnUseTool(ToolManager.ToolType toolType)
    {
        Hurt();
    }

    public void Hurt(bool fromOuchie = false)
    {
        Animator.SetTrigger("Hurt");
        
        if (fromOuchie == false)
            Face.Instance.DoOuchie();
    }

    public void Tear()
    {
        if (ParticleSystem.isPlaying) return;
        ParticleSystem.Play();
    }
}
