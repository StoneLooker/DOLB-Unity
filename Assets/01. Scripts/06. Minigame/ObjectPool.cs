using System.Collections.Generic;
using UnityEngine;


public enum ObstacleType { Moving, Static}
public class ObjectPool : MonoBehaviour
{   
    public GameObject movingObstaclePrefab;
    public GameObject staticObstaclePrefab;
    public Transform parentTransform;
    public int poolSize = 10;

    private Dictionary<ObstacleType, List<GameObject>> pools;

    void Start()
    {
        pools = new Dictionary<ObstacleType, List<GameObject>>
        {
            { ObstacleType.Moving, CreatePool(movingObstaclePrefab) },
            { ObstacleType.Static, CreatePool(staticObstaclePrefab) },
        };
    }

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

    public GameObject GetPooledObject(ObstacleType obstacleType)
    {
        List<GameObject> pool = pools[obstacleType];
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        GameObject newObj = Instantiate(GetPrefabByType(obstacleType), parentTransform);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

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

    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
