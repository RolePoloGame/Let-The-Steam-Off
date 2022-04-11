using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSingleRoom : MonoBehaviour
{

    private List<Vector3> floorPositions = new List<Vector3>();
    private List<Vector3> wallsPositions = new List<Vector3>();

    private Hashtable floorContainer = new Hashtable();
    private Hashtable wallsContainer = new Hashtable();

    public GameObject floorBlockObject;
    public GameObject wallBlockObject;
    public GameObject objectToSpawn;
    public GameObject player;

    private Vector3 startPosition;

    private int worldSizeX = 10;
    private int worldSizeZ = 10;
    private int wallsHeight = 5;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid(floorBlockObject);
        GenerateWalls(wallBlockObject);
        SpawnObject();
        CreateNodes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid(GameObject spawnedObject)
    {
        for (int x = -worldSizeX; x < worldSizeX; x++)
        {
            for (int z = -worldSizeZ; z < worldSizeZ; z++)
            {
                Vector3 position = new Vector3(x * 1 + startPosition.x, 0, z * 1 + startPosition.z);
                GameObject floor = Instantiate(spawnedObject, position, Quaternion.identity);
                floorContainer.Add(position, floor);
                floorPositions.Add(floor.transform.position);
                floor.transform.SetParent(transform);
            }
        }
    }

    private void GenerateWalls(GameObject spawnedObject)
    {
        for (int x = 0; x < wallsHeight; x++)
        {
            for (int z = -worldSizeZ; z < worldSizeZ; z++)
            {

                CreateWall(spawnedObject, new Vector3(worldSizeX, x * 1, z + startPosition.z));
                CreateWall(spawnedObject, new Vector3(-worldSizeX - 1, x * 1, z + startPosition.z));
                CreateWall(spawnedObject, new Vector3(z + startPosition.x, x, worldSizeZ));
                CreateWall(spawnedObject, new Vector3(z + startPosition.x, x, -worldSizeZ - 1));

            }
        }
    }

    private void CreateWall(GameObject spawnedObject, Vector3 newposition)
    {
        Vector3 position = newposition;
        GameObject wall = Instantiate(spawnedObject, position, Quaternion.identity);
        wallsContainer.Add(position, wall);
        wallsPositions.Add(wall.transform.position);
        wall.transform.SetParent(transform);
    }

    private void CreateNodes()
    {

    }

    private void SpawnObject()
    {
        for (int c = 0; c < 20; c++)
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
