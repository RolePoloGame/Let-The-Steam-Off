using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSingleRoom : BaseClass
{

    private List<Vector3> floorPositions = new List<Vector3>();
    private List<Vector3> wallsPositions = new List<Vector3>();

    private Hashtable floorContainer = new Hashtable();
    private Hashtable wallsContainer = new Hashtable();

    public GameObject floorBlockObject;
    public GameObject wallBlockObject;
    public GameObject objectToSpawn;
    public GameObject enemyObject;


    private Vector3 startPosition;



    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid(floorBlockObject, worldSizeX, worldSizeZ);
        GenerateWalls(wallBlockObject, worldSizeX, worldSizeZ,roomSize);
        Debug.Log("Przed" + floorPositions.Count);
        SpawnObject((worldSizeX *worldSizeZ)/5);
        SpawnEnemy((worldSizeX * worldSizeZ)/5);
        Debug.Log("Po" + floorPositions.Count);
    }

    private void SpawnEnemy(int No_of_opponents)
    {
        do
        {
            int ranIndx = Random.Range(0,floorPositions.Count);
            Vector3 position = new Vector3(
                floorPositions[ranIndx].x,
                floorPositions[ranIndx].y + 1.5f,
                floorPositions[ranIndx].z
            );

            Instantiate(enemyObject, position, Quaternion.identity);
            floorPositions.RemoveAt(ranIndx);
            No_of_opponents--;
        } while (No_of_opponents >= 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid(GameObject spawnedObject, int SizeX, int SizeZ)
    {
        for (int x = -SizeX+1; x < SizeX; x++)
        {
            for (int z = -SizeZ+1; z < SizeZ; z++)
            {
                CreateFloor(spawnedObject, new Vector3(x * 1 + startPosition.x, 1, z * 1 + startPosition.z));
            }
        }
    }
    private void CreateFloor(GameObject spawnedObject, Vector3 position)
    {
        GameObject floor = Instantiate(spawnedObject, position, Quaternion.identity);
        floorContainer.Add(position, floor);
        floorPositions.Add(floor.transform.position);
        floor.transform.SetParent(transform);
    }

    private void GenerateWalls(GameObject spawnedObject, int SizeX, int SizeZ, int roomSize)
    {

        y_WallPossition = (int)wallBlockObject.transform.localScale.y/2;

        for (int z = -SizeZ+1; z < SizeZ; z++)
            {
                CreateWall(spawnedObject, new Vector3(worldSizeX, y_WallPossition, z + startPosition.z));
                CreateWall(spawnedObject, new Vector3(-worldSizeX, y_WallPossition, z + startPosition.z));
            }
        for (int x = -SizeX + 1; x < SizeX; x++)
        {
            if (x >= (randDoor - roomSize) && x <= (randDoor + roomSize))
            {
                continue;
            }
            CreateWall(spawnedObject, new Vector3(x + startPosition.x, y_WallPossition, worldSizeZ));
            CreateWall(spawnedObject, new Vector3(x + startPosition.x, y_WallPossition, -worldSizeZ));
        }

    }
    
    private void CreateWall(GameObject spawnedObject, Vector3 position)
    {
        GameObject wall = Instantiate(spawnedObject, position, Quaternion.identity);
        wallsContainer.Add(position, wall);
        wallsPositions.Add(wall.transform.position);
        wall.transform.SetParent(transform);
    }

    private void SpawnObject(int No_of_objects)
    {
        for (int c = 0; c < No_of_objects; c++)
        {
            GameObject toPlaceObject = Instantiate(objectToSpawn, ObjectSpawnLocation(), Quaternion.identity);
        }
    }
    private Vector3 ObjectSpawnLocation()
    {
        int rndIndex = Random.Range(0, floorPositions.Count);

        Vector3 newPosition = new Vector3(
                floorPositions[rndIndex].x,
                floorPositions[rndIndex].y + 1.5f,
                floorPositions[rndIndex].z
            );
        floorPositions.RemoveAt(rndIndex);
        return newPosition;
    }
}