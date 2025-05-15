using UnityEngine;

public class BeamController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

   // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBeam = Instantiate(weaponData.Prefab); 
        spawnedBeam.transform.position = transform.position;
        spawnedBeam.GetComponent<BeamBehaviour>().DirectionChecker();
    }
}
