using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class PAR : MonoBehaviour
{
    public static PAR Get { get; private set; }
    void Awake()
    {
        if (Get == null)
            Get = this;
        else
            Destroy(gameObject);
    }
    public InputManager InputManager
    {
        get
        {
            return InputManager.Instance;
        }
    }

    public GameObjectReference Player;

    [Serializable]
    public class GameObjectReference
    {
        public GameObject GameObject;
        public Transform Transform;
    }

}
