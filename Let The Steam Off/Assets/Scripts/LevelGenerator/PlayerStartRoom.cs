using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartRoom : BaseClass
{
    private List<Vector3> startRoomFloorPos = new List<Vector3>();
    private List<Vector3> startRoomWallsPos = new List<Vector3>();

    private Hashtable startRoomFloorContainer = new Hashtable();
    private Hashtable startRoomWallsContainer = new Hashtable();

    [SerializeField] private GameObject floorBlockObject;
    [SerializeField] private GameObject wallBlockObject;
    [SerializeField] private GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        y_WallPossition = (int)wallBlockObject.transform.localScale.y / 2;
        GenerateStartRoom(floorBlockObject, wallBlockObject, roomSize);
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Generating a starting room for a player of the given size.
    /// </summary>
    /// <param name="floor">Object used to spawn a floor.</param>
    /// <param name="wall">Object used to spawn a wall.</param>
    /// <param name="Size">Player room size.</param>
    private void GenerateStartRoom(GameObject floor, GameObject wall, int Size)
    {
        for (int x = -Size + 1; x < Size; x++)
        {
            CreateWall(wall, new Vector3(x + randDoor, y_WallPossition, worldSizeZ + (Size + (Size - 1))));
            CreateWall(wall, new Vector3(randDoor - Size, y_WallPossition, x + ((worldSizeZ - 1) + Size)));
            CreateWall(wall, new Vector3(randDoor + Size, y_WallPossition, x + ((worldSizeZ - 1) + Size)));
            for (int z = -Size + 1; z < Size; z++)
            {
                CreateFloor(floor, new Vector3(x * 1 + randDoor, 1, z * 1 + worldSizeZ + (Size - 1)));
            }
        }
    }

    /// <summary>
    /// Create one floor object and add it to the list.
    /// </summary>
    /// <param name="spawnedObject">The object we want to spawn.</param>
    /// <param name="position">The position at which the object spawns.</param>
    private void CreateFloor(GameObject spawnedObject, Vector3 position)
    {
        GameObject floor = Instantiate(spawnedObject, position, Quaternion.identity);
        startRoomFloorContainer.Add(position, floor);
        startRoomFloorPos.Add(floor.transform.position);
        floor.transform.SetParent(transform);
    }

    /// <summary>
    /// Create one wall object and add it to the list.
    /// </summary>
    /// <param name="spawnedObject">The object we want to spawn.</param>
    /// <param name="position">The position at which the object spawns.</param>
    private void CreateWall(GameObject spawnedObject, Vector3 position)
    {
        GameObject wall = Instantiate(spawnedObject, position, Quaternion.identity);
        startRoomWallsContainer.Add(position, wall);
        startRoomWallsPos.Add(wall.transform.position);
        wall.transform.SetParent(transform);
    }

    private void SpawnPlayer()
    {
        GameObject playerPos = Instantiate(playerObject, new Vector3(randDoor, floorBlockObject.transform.localScale.y + 1.5f, worldSizeZ + roomSize - 1), Quaternion.identity);
    }
}
