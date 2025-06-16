using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "2D Top-Down Rogue-Like/Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    WeaponData startingWeapon;
    public WeaponData StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [System.Serializable]
    public struct Stats
    {
        //public float maxHealth, recovery, moveSpeed;
        //public float might, speed, souls, magnet;

        public float maxHealth, recovery;
        [Range(-1, 10)] public float moveSpeed, might, area;
        [Range(-1, 5)] public float speed;
        [Range(-1, 1)] public float cooldown;
        public float magnet, souls;

        //public Stats(float maxHealth = 1000, float recovery = 0, float moveSpeed = 1f, float might = 1f, float speed = 1f, float magnet = 30f, float souls =0)
        //{
        //    this.maxHealth = maxHealth;
        //    this.recovery = recovery;
        //    this.moveSpeed = moveSpeed;
        //    this.might = might;
        //    this.speed = speed;
        //    this.magnet = magnet;
        //    this.souls = souls;
        //}

        public static Stats operator +(Stats s1, Stats s2)
        {
            s1.maxHealth += s2.maxHealth;
            s1.recovery += s2.recovery;
            s1.moveSpeed += s2.moveSpeed;
            s1.might += s2.might;
            s1.speed += s2.speed;
            s1.magnet += s2.magnet;
            s1.souls += s2.souls;
            s1.area += s2.area;
            s1.cooldown += s2.cooldown;
            return s1;
        }
    }
    public Stats stats = new Stats
    {
        maxHealth = 100,
        recovery = 0,
        moveSpeed = 1,
        might = 1,
        speed = 1,
        magnet = 1.8f,
        souls = 0,
        cooldown = 1,
    };

}
