using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public static CharacterSelector instance;
    public CharacterData characterData;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }
    }
    
    public static CharacterData GetData()
    {
        if(instance && instance.characterData)
        {
            return instance.characterData;
        }
        else
        {
            CharacterData[] characters = Resources.FindObjectsOfTypeAll<CharacterData>();
            if(characters.Length > 0)
            {
                return characters[Random.Range(0, characters.Length)]; // Return the first character found if no selection has been made
            }
        }
        return null;
    }

    public void SelectCharacter(CharacterData character)
    {
        characterData = character;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
