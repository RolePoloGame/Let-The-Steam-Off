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

    public GameObject floorBlockObject;
    public GameObject wallBlockObject;


    // Start is called before the first frame update
    void Start()
    {
        y_WallPossition = (int)wallBlockObject.transform.localScale.y / 2;
        GenerateStartRoom(floorBlockObject, wallBlockObject, roomSize);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

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

    private void CreateFloor(GameObject spawnedObject, Vector3 position)
    {
        GameObject floor = Instantiate(spawnedObject, position, Quaternion.identity);
        startRoomFloorContainer.Add(position, floor);
        startRoomFloorPos.Add(floor.transform.position);
        floor.transform.SetParent(transform);
    }

    private void CreateWall(GameObject spawnedObject, Vector3 position)
    {
        GameObject wall = Instantiate(spawnedObject, position, Quaternion.identity);
        startRoomWallsContainer.Add(position, wall);
        startRoomWallsPos.Add(wall.transform.position);
        wall.transform.SetParent(transform);
    }
}
