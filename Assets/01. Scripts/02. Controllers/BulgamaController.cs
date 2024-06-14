using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulgamaController : MonoBehaviour
{
    [SerializeField] GameObject EmptyStone;
    [SerializeField] int numberOfStonesToSpawn = 10;
    [SerializeField] Vector2 spawnAreaMin;
    [SerializeField] Vector2 spawnAreaMax;

    void PutInfoToEmptyStone(GameObject obj, STONE_TYPE type)
    {
        switch (type)
        {
            case STONE_TYPE.LimeStone:
                obj.AddComponent<LimeStoneController>();
                break;
            case STONE_TYPE.Granite:
                obj.AddComponent<GraniteController>();
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomStones();
    }

    void SpawnRandomStones()
    {
        for (int i = 0; i < numberOfStonesToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject stone = Instantiate(EmptyStone, randomPosition, Quaternion.identity);
            STONE_TYPE randomType = GetRandomStoneType();
            PutInfoToEmptyStone(stone, randomType);
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector3(randomX, randomY, 0); // Assuming 2D spawning, set z to 0
    }

    STONE_TYPE GetRandomStoneType()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(STONE_TYPE)).Length);
        return (STONE_TYPE)randomIndex;
    }
}   
