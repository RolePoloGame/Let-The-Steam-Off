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
    private InputManager inputManager;
    public InputManager GetInputManager()
    {
        if (inputManager == null)
        {
            inputManager = gameObject.AddComponent<InputManager>();
        }

        return inputManager;
    }

    public GameObjectReference Player;

    [Serializable]
    public class GameObjectReference
    {
        public GameObject GameObject;
        public Transform Transform;
    }

}
