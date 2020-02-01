using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Intro : MonoBehaviour
{

    public void OnIntroEnd()
    {
        UI_Ingame.Instance.BeginStartGameSequence();
    }
    
}
