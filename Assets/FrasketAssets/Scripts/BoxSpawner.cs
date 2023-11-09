using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public BoxData[] boxArray;
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

            BoxData selectedBoxData = boxArray[Random.Range(0, boxArray.Length)];

            GameObject boxPrefab = selectedBoxData.boxPrefab;
            GameObject spawnedBox = Instantiate(boxPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}