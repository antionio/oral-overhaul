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

    [Header("Visual")]
    public SpriteRenderer ToolSpriteRenderer;
    public GameObject[] ToolSprites;
    [Header("Audio")]
    public AudioSource AudioSource;
    public AudioClip[] ToolUseClips;
    [Header("Settings")]
    public float ToolRadius;
    
    private ToolType SelectedToolType;

    private void OnEnable()
    {
        Cursor.visible = false;
        SetTool(ToolType.Hand);
    }

    private void SetTool(ToolType toolType)
    {
        SelectedToolType = toolType;

        foreach (var s in ToolSprites)
        {
            s.SetActive(false);
        }
        
        ToolSprites[(int)SelectedToolType].SetActive(true);
    }

    private bool HoldUseSelectedTool(bool holding)
    {
        if (SelectedToolType != ToolType.Drill) return false; // only drill is hold

        if (holding == false)
        {
            AudioSource.Stop();
            return true;
        }
        
        if (AudioSource.isPlaying == false)
        {
            AudioSource.clip = ToolUseClips[(int) SelectedToolType];
            AudioSource.loop = true;
            AudioSource.Play();
        }
        
        var hits = Physics2D.CircleCastAll(transform.position, ToolRadius, Vector2.zero);
        if (hits.Length > 0)
        {
            // first check tooth
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider.CompareTag("Tooth"))
                {
                    Debug.Log("Hit a tooth");
                    h.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.RequireReceiver);
                    return true; // do not proceed, tooth is prioritized
                }
            }

            var firstHit = hits[0];
            Debug.Log("Hit a " + firstHit.collider.gameObject.name);
            firstHit.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.DontRequireReceiver);
        }
        
        return true;
    }
    
    private void UseSelectedTool()
    {

        if (SelectedToolType == ToolType.Drill) return;
        
        var hits = Physics2D.CircleCastAll(transform.position, ToolRadius, Vector2.zero);
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

        if (Input.GetMouseButton(0))
        {
            bool constantUse = HoldUseSelectedTool(true);
            if (constantUse == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    UseSelectedTool();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            HoldUseSelectedTool(false);
        }
       
        
        var mousePos = Input.mousePosition;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0f);
    }
    
}
