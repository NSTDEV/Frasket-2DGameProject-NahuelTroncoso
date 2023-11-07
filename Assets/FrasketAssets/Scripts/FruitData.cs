using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Fruit Data")]
public class FruitData : ScriptableObject
{
    public string fruitName;
    public GameObject fruitPrefab;
    public int fruitScore;
}