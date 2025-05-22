using UnityEngine;

[CreateAssetMenu(fileName = "BigKaijuScriptableObject", menuName = "Scriptable Objects/BigKaijuScriptableObject")]
public class BigKaijuScriptableObject : ScriptableObject
{
    [SerializeField]
    private int numEnemies;
    public int NumEnemies { get => numEnemies; private set => numEnemies = value; }

    public GameObject enemyPrefab;

    [SerializeField]
    private float health;
    public float Health { get => health; private set => health = value; }
}
