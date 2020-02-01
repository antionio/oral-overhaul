using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : SingletonBehaviour<Face>
{
    
    public ParticleSystem BloodSpurtParticle;
    public AudioSource AudioSource;
    public AudioClip HurtClip;
    public float AudioIntervalSeconds;

    public Eye[] Eyes;
    
    private float audioStateTime;

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
    }

    public void DoOuchie()
    {
        foreach (var e in Eyes)
        {
            e.Hurt(true);
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
}
