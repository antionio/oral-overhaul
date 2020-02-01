using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInventory : MonoBehaviour
{

    public GameObject[] Tools;


    public void HideSelectedTool(ToolManager.ToolType selectedToolType)
    {
        foreach (var tool in Tools)
        {
            tool.SetActive(true);
        }
        
        if (selectedToolType == ToolManager.ToolType.Hand)
        {
            return;
        }
        
        Tools[(int)selectedToolType - 1].SetActive(false);
    }
}
