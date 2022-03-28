using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class PAR : MonoBehaviour
{
    public static PAR Instance { get; private set; }
    void Awake()
    {
        Instance = this;   
    }

    public GameObjectReference Player;

    [Serializable]
    public class GameObjectReference
    {
        public GameObject GameObject;
        public Transform Transform;
    }
}
