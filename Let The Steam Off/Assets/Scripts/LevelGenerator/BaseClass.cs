using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    protected static int worldSizeX;
    protected static int worldSizeZ;
    protected int y_WallPossition;
    protected static int randDoor;
    protected static int roomSize = 3;

    /// <summary>
    /// Entering the size of the map and the variable needed to draw the place where the player's rooms are created.
    /// </summary>
    private void Awake()
    {
        worldSizeX = Random.Range(5, 20);
        worldSizeZ = Random.Range(5, 20);
        randDoor = Random.Range(-worldSizeX + roomSize, worldSizeX - (roomSize - 1));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
