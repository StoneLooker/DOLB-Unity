using System.Collections.Generic;
using UnityEngine;

//Enum to define different types of obstacles
public enum ObstacleType { Moving, Static}

//Object pool manager for handling obstacle objects
public class ObjectPool : MonoBehaviour
{   
    //Prefabs for moving and static obstacles
    public GameObject movingObstaclePrefab;
    public GameObject staticObstaclePrefab;
    public Transform parentTransform;
    public int poolSize = 10;

    //Dictionary to hold pools of different obstacle types
    private Dictionary<ObstacleType, List<GameObject>> pools;

    void Start()
    {
        //Initialize the pools dictionary with created pools
        pools = new Dictionary<ObstacleType, List<GameObject>>
        {
            { ObstacleType.Moving, CreatePool(movingObstaclePrefab) },
            { ObstacleType.Static, CreatePool(staticObstaclePrefab) },
        };
    }

    //Create a pool of GameObjects from a given prefab
    private List<GameObject> CreatePool(GameObject prefab)
    {
        List<GameObject> pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parentTransform);
            obj.SetActive(false);
            pool.Add(obj);
        }
        return pool;
    }

    //Get an inactive object from the pool, or create a new one if necessary
    public GameObject GetPooledObject(ObstacleType obstacleType)
    {
        List<GameObject> pool = pools[obstacleType];
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        //If no inactive object is available, create a new one
        GameObject newObj = Instantiate(GetPrefabByType(obstacleType), parentTransform);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    //Get the prefab corresponding to the given obstacle type
    private GameObject GetPrefabByType(ObstacleType obstacleType)
    {
        switch (obstacleType)
        {
            case ObstacleType.Moving:
                return movingObstaclePrefab;
            case ObstacleType.Static:
                return staticObstaclePrefab;
            default:
                return null;
        }
    }

    //Return an object to the pool by deactivating it
    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
