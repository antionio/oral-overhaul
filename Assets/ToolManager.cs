using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

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

    private void UseSelectedTool()
    {

        switch (SelectedToolType)
        {
            case ToolType.Hand:
                break;
            case ToolType.Drill:
                break;
        }

        var hits = Physics2D.CircleCastAll(transform.position, .5f, Vector2.zero);
        if (hits.Length > 0)
        {
            // first check tooth
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider.CompareTag("Tooth"))
                {
                    Debug.Log("Hit a tooth");
                    h.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.RequireReceiver);
                    return; // do not proceed, tooth is prioritized
                }
            }

            var firstHit = hits[0];
            Debug.Log("Hit a " + firstHit.collider.gameObject.name);
            firstHit.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.DontRequireReceiver);
        }
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

        if (Input.GetMouseButtonDown(0))
        {
            UseSelectedTool();
        }
        
        var mousePos = Input.mousePosition;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0f);
    }
    
}
