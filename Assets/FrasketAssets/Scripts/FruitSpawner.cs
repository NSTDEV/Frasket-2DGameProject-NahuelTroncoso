using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public FruitData[] fruitDataArray;
    public float spawnInterval = 0.5f;
    public float minTras;
    public float maxTras;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);

            FruitData selectedFruitData = fruitDataArray[Random.Range(0, fruitDataArray.Length)];

            GameObject fruitPrefab = selectedFruitData.fruitPrefab;
            GameObject spawnedFruit = Instantiate(fruitPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}