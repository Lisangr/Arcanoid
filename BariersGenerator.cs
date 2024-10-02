using UnityEngine;

public class BariersGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private int _numberOfObjects = 6;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Bounds groundBounds = _ground.GetComponent<Collider>().bounds;

        for (int i = 0; i < _numberOfObjects; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(groundBounds.min.x + 1, groundBounds.max.x - 1),
                groundBounds.min.y+0.15f,
                Random.Range(groundBounds.min.z + 1, groundBounds.max.z - 1)
            );

            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }

}
