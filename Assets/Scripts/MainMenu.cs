using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        PhaseManager.Instance.LoadPhase1();
    }

    public void QuitGame() {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        Application.Quit();
    }
}