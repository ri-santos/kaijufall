using UnityEngine;

public class PassiveItem : MonoBehaviour
{

    protected PlayerManager player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        //Apply the boost in the child classes
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerManager>();
        ApplyModifier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
