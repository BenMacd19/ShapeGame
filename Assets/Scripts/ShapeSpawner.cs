using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for spawning shapes
public class ShapeSpawner : MonoBehaviour
{
    // Shapes
    [SerializeField] private List<GameObject> shapePrefabs;
    [SerializeField] private List<GameObject> ObstacleShapePrefabs; 
    [SerializeField] private int minSpawnTime = 3, maxSpawnTime = 5;
    [SerializeField] private float rayLength = 10;

    private int[] rotations = new int[] { 0, 90 };
    public Vector2 size;

    // Spawns a shape at a random position on the plane
    public void SpawnShapeOnGrid()
    {
        // Pick either a sphere or capsule to spawn
        GameObject shape = shapePrefabs[Random.Range(0, shapePrefabs.Count)];

        // Find a random point on the plane
        Vector3 pos = GetRandomPoint();

        Debug.DrawRay(pos, Vector3.down * 10, Color.red, 5);

        if (Physics.Raycast(pos, Vector3.down, 10))
        {
            //Debug.DrawRay(pos, Vector3.down * rayLength, Color.blue, 5);
            SpawnShapeOnGrid();
            return;
        }
        else
        {
            // Spawn the shape at that position.
            Instantiate(shape, pos + shape.transform.position , shape.transform.rotation);
        } 
    }

    // Spawns the obstacle shape on the plane at a random position every min - max amount of seconds
    public IEnumerator SpawnObstacleOnGrid()
    {
        while (true)
        {
            // Time inbetween spawning obstacles
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            
            // Pick random obstacle to spawn
            GameObject newObstacle = ObstacleShapePrefabs[Random.Range(0, ObstacleShapePrefabs.Count)];
            RandomRotation(newObstacle);

            // Find a random point on the plane using the centre point
            Vector3 pos = GetRandomPoint() + newObstacle.GetComponent<ObstacleShape>().centrePoint.position;

            // Check if the spawn position is free
            if (!CanPlaceObstacle(pos, newObstacle.GetComponent<ObstacleShape>()))
            {
                // Debug.Log("Cant Place Obstacle");
            }
            else 
            {
                // Spawn obstacle
                Instantiate(newObstacle, pos, newObstacle.transform.rotation);
            }           
        }   
    }

    public void RandomRotation(GameObject shape)
    {
        int rotation = rotations[Random.Range(0, rotations.Length)];
        shape.transform.rotation = Quaternion.Euler(new Vector3(0, (float)rotation, 0));
    }

    // Finds a random point in a designated area
    private Vector3 GetRandomPoint()
    {
        Vector3 pos = Vector3.zero + new Vector3(
                                            Mathf.Round(Random.Range((-size.x + 1) / 2, (size.x - 1) / 2)),
                                            10, 
                                            Mathf.Round(Random.Range((-size.y + 1) / 2, (size.y - 1) / 2)));
        return pos;
    }

    private bool CanPlaceObstacle(Vector3 pos, ObstacleShape obstacleShape)
    {
        foreach (Transform castPoint in obstacleShape.castPoints)
        {
            if (Physics.Raycast(pos + castPoint.position, Vector3.down, rayLength))
            {
                //Debug.DrawRay(pos + castPoint.position, Vector3.down * rayLength, Color.red, 5);
                return false;
            }
            //Debug.DrawRay(pos + castPoint.position, Vector3.down * rayLength, Color.green, 5);
        }    
        return true; 
    }
}
