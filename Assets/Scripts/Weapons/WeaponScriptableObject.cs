using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "Scriptable Objects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }


    [SerializeField]
    private float damage;
    public float Damage { get => damage;  private set => damage = value;}
    [SerializeField] 
    private float speed;
    public float Speed { get => speed; private set => speed = value;}

    [SerializeField]
    private float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

}
