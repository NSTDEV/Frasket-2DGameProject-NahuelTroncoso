using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxData", menuName = "Box Data")]
public class BoxData : ScriptableObject
{
    public string boxName;
    public GameObject boxPrefab;
    public int boxLifes;
    public List<FruitInfo> availableFruits;
}

[System.Serializable]
public class FruitInfo
{
    public FruitData fruitData;
}