using UnityEngine;
using TMPro;

public class KaijuCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private PhaseManager phaseManager;
    
    private void Update()
    {
        counterText.text = $"Kaijus: {phaseManager.DefeatedKaijus}/{phaseManager.KaijusToDefeat}";
    }
}