using UnityEngine;

[CreateAssetMenu(fileName = "PlayerKaijuScriptableObject", menuName = "Scriptable Objects/PlayerKaijuScriptableObject")]
public class PlayerKaijuScriptableObject : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private string kaijuName;
    
    [Header("Movement")]
    [SerializeField] private int speed;
    
    [Header("Combat Stats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private float blockStrength;
    [SerializeField] private float cost;
    
    [Header("Attack Types")]
    [SerializeField] private AttackType attackType;
    [SerializeField] private GameObject attackPrefab; // For projectile attacks
    [SerializeField] private float specialAbilityCooldown;
    
    public GameObject Prefab => prefab;
    public string KaijuName => kaijuName;
    public int Speed => speed;
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public float AttackCooldown => attackCooldown;
    public float AttackRange => attackRange;
    public float BlockStrength => blockStrength;
    public float Cost => cost;
    public AttackType AttackType => attackType;
    public GameObject AttackPrefab => attackPrefab;
    public float SpecialAbilityCooldown => specialAbilityCooldown;
}

public enum AttackType
{
    Melee,
    Ranged,
    AreaOfEffect,
    Special
}