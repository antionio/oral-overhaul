using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{

    public Animator Animator;

    private void Update()
    {
        if (UI_Ingame.Instance.MenuActive()) return;

        if (Animator.enabled == false)
        {
            Animator.enabled = true;
        }
    }
}
