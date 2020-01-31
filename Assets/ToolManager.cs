using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{

    public enum ToolType
    {
        Hand = 0,
        Drill = 1
    }

    [Header("Tool Sprite")]
    public SpriteRenderer ToolSpriteRenderer;

    public Sprite[] ToolSprites;
    
    private ToolType SelectedToolType;

    private void OnEnable()
    {
        Cursor.visible = false;
        SetTool(ToolType.Hand);
    }

    private void SetTool(ToolType toolType)
    {
        SelectedToolType = toolType;
        ToolSpriteRenderer.sprite = ToolSprites[(int)toolType];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTool(ToolType.Hand);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTool(ToolType.Drill);
        }
        
        var mousePos = Input.mousePosition;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0f);
    }
    
}
