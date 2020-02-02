using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    
    public void OnUseTool(ToolManager.ToolType toolType)
    {
        if (toolType == ToolManager.ToolType.Scraper)
        {
            gameObject.SetActive(false);            
        }
    }
    
}
