using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for spawning shapes
public class ShapeSpawner : MonoBehaviour
{
    // Shapes
    [SerializeField] private List<GameObject> shapePrefabs;
    [SerializeField] private GameObject ObstacleShapePrefab;
    
    [SerializeField] private int minSpawnTime = 3, maxSpawnTime = 5;
    public Vector2 size;

    // Spawns a shape at a random position on the plane
    public void SpawnShapeOnGrid()
    {
        // Pick either a sphere or capsule to spawn
        GameObject gameObject = shapePrefabs[Random.Range(0, shapePrefabs.Count)];

        // Find a random point on the plane
        Vector3 pos = GetRandomPoint();

        while (Physics.Raycast(pos + new Vector3(0,10,0), Vector3.down, 20))
        {
            pos = GetRandomPoint();
        }

        // Spawn the shape at that position.
        Instantiate(gameObject, pos, Quaternion.identity);
    }

    // Spawns the obstacle shape on the plane at a random position every min - max amount of seconds
    public IEnumerator SpawnObstacleOnGrid()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            
            // Find a random point on the plane
            Vector3 pos = GetRandomPoint();
            
            // Check nither a shape or an obstacle is on that position
            if (Physics.Raycast(pos + new Vector3(0,10,0), Vector3.down, 20))
            {
                Debug.Log("Return");
            }
            else {
                Instantiate(ObstacleShapePrefab, pos, Quaternion.identity);
            }           
        }   
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
}
