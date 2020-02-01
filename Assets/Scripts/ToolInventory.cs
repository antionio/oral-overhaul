using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInventory : SingletonBehaviour<ToolInventory>
{

    public GameObject[] Tools;


    public void HideSelectedTool(ToolManager.ToolType selectedToolType)
    {
        foreach (var t in Tools)
        {
            t.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        
        if (selectedToolType == ToolManager.ToolType.Hand)
        {
            return;
        }

        var tool = Tools[(int) selectedToolType - 1];
        tool.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
}
