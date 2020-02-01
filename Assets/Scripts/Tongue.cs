using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{

    public Animator Animator;

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        //Animator.SetTrigger(toolType + "_Use");
    }
}
