using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndRoom : BaseClass
{
    private List<Vector3> endRoomFloorPos = new List<Vector3>();
    private List<Vector3> endRoomWallsPos = new List<Vector3>();

    private Hashtable endRoomFloorContainer = new Hashtable();
    private Hashtable endRoomWallsContainer = new Hashtable();

    public GameObject floorBlockObject;
    public GameObject wallBlockObject;
    public GameObject PlayerRoom;

    // Start is called before the first frame update
    void Start()
    {
        y_WallPossition = (int)wallBlockObject.transform.localScale.y / 2;
        GenerateEndRoom(floorBlockObject, wallBlockObject, roomSize);
        TriggerLaunch(PlayerRoom);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateEndRoom(GameObject floor, GameObject wall, int Size)
    {
        for (int x = -Size + 1; x < Size; x++)
        {
            CreateWall(wall, new Vector3(x + randDoor, y_WallPossition, -worldSizeZ - (Size + (Size - 1))));
            CreateWall(wall, new Vector3(randDoor - Size, y_WallPossition, x - ((worldSizeZ - 1) + Size)));
            CreateWall(wall, new Vector3(randDoor + Size, y_WallPossition, x - ((worldSizeZ - 1) + Size)));
            for (int z = -Size + 1; z < Size; z++)
            {
                CreateFloor(floor, new Vector3(x * 1 + randDoor, 1, z * 1 + -worldSizeZ - (Size - 1)));
            }
        }
    }

    private void CreateFloor(GameObject spawnedObject, Vector3 position)
    {
        GameObject floor = Instantiate(spawnedObject, position, Quaternion.identity);
        endRoomFloorContainer.Add(position, floor);
        endRoomFloorPos.Add(floor.transform.position);
        floor.transform.SetParent(transform);
    }

    private void CreateWall(GameObject spawnedObject, Vector3 position)
    {
        GameObject wall = Instantiate(spawnedObject, position, Quaternion.identity);
        endRoomWallsContainer.Add(position, wall);
        endRoomWallsPos.Add(wall.transform.position);
        wall.transform.SetParent(transform);
    }

    private void TriggerLaunch(GameObject PlayerRoom)
    {
        PlayerRoom.transform.position = new Vector3(randDoor, roomSize, -worldSizeZ - 2);
        PlayerRoom.transform.localScale = new Vector3(roomSize + 1, roomSize-1, roomSize-1);

    }

}
