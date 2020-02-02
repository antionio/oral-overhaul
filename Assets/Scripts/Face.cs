using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : SingletonBehaviour<Face>
{

    public Animator Animator;
    public ParticleSystem BloodSpurtParticle;
    public AudioSource AudioSource;
    public AudioClip HurtClip;
    public AudioClip ChokingClip;
    public float AudioIntervalSeconds;

    public Eye[] Eyes;
    
    private float audioStateTime;
    private bool choking;

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        switch (toolType)
        {
            case ToolManager.ToolType.Drill:
                BloodSpurtParticle.transform.position = ToolManager.Instance.transform.position;
                BloodSpurtParticle.Play();

                DoOuchie();
                break;
        }
    }

    private void Update()
    {
        audioStateTime += Time.deltaTime;

        if (choking)
        {
            DoOuchie();
            Screenshake.Instance.ScreenShake(0.01f, 0.2f);
            
            if (AudioSource.isPlaying == false) {
                AudioSource.clip = ChokingClip;
                AudioSource.Play();
            }
        }
    }

    public void DoOuchie(bool badOuchie = false)
    {
        foreach (var e in Eyes)
        {
            e.Hurt(true);
            if (badOuchie)
            {
                e.Tear();
            }
        }
        
        if (audioStateTime < AudioIntervalSeconds)
        {
            return;
        }
                
        audioStateTime = 0f;
        if (AudioSource.isPlaying == false) {
            AudioSource.PlayOneShot(HurtClip);
        }
    }

    public void StartChoking()
    {
        if (choking) return;
        choking = true;
        Animator.SetTrigger("Choke");
    }

    public void StopChoking()
    {
        if (choking == false) return;
        choking = false;
        Animator.SetTrigger("Idle");
    }
}
