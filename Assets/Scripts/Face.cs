using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{

    public ParticleSystem BloodSpurtParticle;

    public void OnUseTool(ToolManager.ToolType toolType)
    {
        switch (toolType)
        {
            case ToolManager.ToolType.Drill:
                BloodSpurtParticle.transform.position = ToolManager.Instance.transform.position;
                BloodSpurtParticle.Play();
                break;
        }
    }

}
