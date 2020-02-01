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

    private float audioStateTime;

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        switch (toolType)
        {
            case ToolManager.ToolType.Drill:
                BloodSpurtParticle.transform.position = ToolManager.Instance.transform.position;
                BloodSpurtParticle.Play();

                PlayOuchSound();
                break;
        }
    }

    private void Update()
    {
        audioStateTime += Time.deltaTime;
    }

    public void PlayOuchSound()
    {
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
