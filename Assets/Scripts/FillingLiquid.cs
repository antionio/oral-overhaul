using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingLiquid : MonoBehaviour
{

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        switch (toolType)
        {
            case ToolManager.ToolType.Filler:
                ToolManager.Instance.PrepareTool(true);
                break;
        }
    }
    
}
