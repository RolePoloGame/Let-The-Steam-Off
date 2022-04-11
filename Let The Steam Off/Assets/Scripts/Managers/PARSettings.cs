using System;
using UnityEngine;


[CreateAssetMenu(menuName = "PAR/Preset")]
public class PARSettings : ScriptableObject
{
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
