using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabsArr;
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

            GameObject prefabSpawner = Instantiate(prefabsArr[Random.Range(0, prefabsArr.Length)], position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}