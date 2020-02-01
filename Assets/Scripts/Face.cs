using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
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

                if (audioStateTime < AudioIntervalSeconds)
                {
                    break;
                }
                
                audioStateTime = 0f;
                if (AudioSource.isPlaying == false) {
                    AudioSource.PlayOneShot(HurtClip);
                }
                break;
        }
    }

    private void Update()
    {
        audioStateTime += Time.deltaTime;
    }
}
