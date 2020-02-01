using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{

    public Animator Animator;
    
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
}
