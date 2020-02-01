using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class ToolManager : SingletonBehaviour<ToolManager>
{

    public enum ToolType
    {
        Hand = 0,
        Drill = 1,
        Filler = 2,
        Mirror = 3,
        Scraper = 4,
    }

    [Header("Visual")]
    public Animator Animator;
    public SpriteRenderer ToolSpriteRenderer;
    public GameObject[] ToolSprites;
    public GameObject[] ToolPreparedSprites;
    [Header("Audio")]
    public AudioSource AudioSource;
    public AudioClip[] ToolUseClips;
    public AudioClip[] ToolSecondaryUseClips;
    [Header("Settings")]
    public float ToolRadius;
    
    private ToolType SelectedToolType;
    private bool prepred = false;

    private List<ToolType> GetToolsThatRequirePreparation()
    {
        var list = new List<ToolType>();
        list.Add(ToolType.Filler);
        return list;
    }

    public bool IsToolPrepared()
    {
        if (GetToolsThatRequirePreparation().Contains(SelectedToolType))
        {
            return prepred;
        }
        return true;
    }

    public void PrepareTool(bool prepared)
    {
        try
        {
            ToolPreparedSprites[(int) SelectedToolType].SetActive(prepared);
            prepred = prepared;
        }
        catch
        {
            Debug.Log("No prepared sprite");
        }
    }

    private void OnEnable()
    {
        SetTool(ToolType.Hand);
    }

    private void SetTool(ToolType toolType)
    {
        PrepareTool(false);
        SelectedToolType = toolType;

        foreach (var s in ToolSprites)
        {
            s.SetActive(false);
        }
        
        ToolSprites[(int)SelectedToolType].SetActive(true);

        FindObjectOfType<ToolInventory>().HideSelectedTool(SelectedToolType);
    }

    private bool HoldUseSelectedTool(bool holding)
    {
        if (SelectedToolType != ToolType.Drill) return false; // only drill is hold
        
        if (holding == false)
        {
            Animator.ResetTrigger(SelectedToolType + "_Use");
            AudioSource.Stop();
            return true;
        }

        Animator.SetTrigger(SelectedToolType + "_Use");
        
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
                    h.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.RequireReceiver);

                    var secondaryClip = ToolSecondaryUseClips[(int) SelectedToolType];
                    if (AudioSource.isPlaying == false || AudioSource.clip != secondaryClip)
                    {
                        AudioSource.clip = secondaryClip;
                        AudioSource.loop = true;
                        AudioSource.Play();
                    }
                    
                    return true; // do not proceed, tooth is prioritized
                }
            }
            // first check tooth
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider.CompareTag("Tongue"))
                {
                    h.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.RequireReceiver);

                    var secondaryClip = ToolSecondaryUseClips[(int) SelectedToolType];
                    if (AudioSource.isPlaying == false || AudioSource.clip != secondaryClip)
                    {
                        AudioSource.clip = secondaryClip;
                        AudioSource.loop = true;
                        AudioSource.Play();
                    }
                    
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
        
        Animator.SetTrigger(SelectedToolType + "_Use");

        var hits = Physics2D.CircleCastAll(transform.position, ToolRadius, Vector2.zero);
        if (hits.Length > 0)
        {
            // first check tooth
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider.CompareTag("Tooth"))
                {
                    h.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.RequireReceiver);

                    if (AudioSource.isPlaying == false)
                    {
                        AudioSource.clip = ToolUseClips[(int) SelectedToolType];
                        AudioSource.loop = false;
                        AudioSource.Play();
                    }

                    return; // do not proceed, tooth is prioritized
                }
            }
            
            // second check tongue
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider.CompareTag("Tongue"))
                {
                    h.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.RequireReceiver);
                    
                    if (AudioSource.isPlaying == false)
                    {
                        AudioSource.clip = ToolSecondaryUseClips[(int) SelectedToolType];
                        AudioSource.loop = false;
                        AudioSource.Play();
                    }

                    return;
                }
            }
            
            // check face
            foreach (RaycastHit2D h in hits)
            {
                if (h.collider.CompareTag("Face"))
                {
                    h.collider.gameObject.SendMessage("OnUseTool", SelectedToolType, SendMessageOptions.RequireReceiver);
                    
                    if (AudioSource.isPlaying == false)
                    {
                        AudioSource.clip = ToolSecondaryUseClips[(int) SelectedToolType];
                        AudioSource.loop = false;
                        AudioSource.Play();
                    }

                    return;
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
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetTool(ToolType.Filler);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetTool(ToolType.Mirror);
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetTool(ToolType.Scraper);
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
