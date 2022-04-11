using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class PAR : MonoBehaviour
{
    public static PARSettings Get { get; private set; }
    void Awake()
    {
        Get = Instance;
    }
    [Expandable]
    public PARSettings Instance;
}
