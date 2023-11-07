using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] BoxData[] boxArray;
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;

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