using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTool : MonoBehaviour
{

    public ToolManager.ToolType ToolType;

    private static bool allowChangeTool = true;

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        if (allowChangeTool == false) return;
        
        if (toolType == ToolType) // drop tool
        {
            ToolManager.Instance.SetTool(ToolManager.ToolType.Hand);
            return;
        }
        
        allowChangeTool = false;
        ToolManager.Instance.SetTool(ToolType);
        StartCoroutine(SetAllowChangeTool());
    }

    private IEnumerator SetAllowChangeTool()
    {
        yield return new WaitForSeconds(1f);
        allowChangeTool = true;
    }
}
