using UnityEngine;

public class MenuMusicController : MonoBehaviour {
    void Start() {
        // Safely handle missing AudioManager
        if (AudioManager.Instance != null) {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);
        }
        else {
            Debug.LogWarning("AudioManager instance not found!");
        }
    }
}