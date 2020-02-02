using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(360, 640, false);
        Application.targetFrameRate = 60;
    }
}
