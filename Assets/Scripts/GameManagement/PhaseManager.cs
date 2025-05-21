using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private int kaijusToDefeat = 10;
    private int defeatedKaijus = 0;
    
    [Header("UI References")]
    [SerializeField] private Slider progressBar;    
    
    public event System.Action OnPhaseComplete;
    
    // Properties for external access
    public int DefeatedKaijus => defeatedKaijus;
    public int TotalKaijus => kaijusToDefeat;
    public int KaijusToDefeat => kaijusToDefeat; // Added this property for UI access
    public bool IsPhaseComplete => defeatedKaijus >= kaijusToDefeat;
    
    private void Start()
    {
        // Initialize progress bar
        UpdateProgressBar();
    }
    
    public void RegisterKaijuDefeat()
    {
        if (IsPhaseComplete) return; // Prevent over-counting
        
        defeatedKaijus++;
        Debug.Log($"Kaijus defeated: {defeatedKaijus}/{kaijusToDefeat}");
        
        UpdateProgressBar();
        
        if (IsPhaseComplete)
        {
            Debug.Log("Phase Complete!");
            OnPhaseComplete?.Invoke();
        }
    }
    
    public float GetProgress()
    {
        return (float)defeatedKaijus / kaijusToDefeat;
    }
    
    private void UpdateProgressBar()
    {
        // Temporary debug - remove after testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RegisterKaijuDefeat();
        }
    }
    
    // Method to reset the phase if needed
    public void ResetPhase()
    {
        defeatedKaijus = 0;
        UpdateProgressBar();
        Debug.Log("Phase reset");
    }
}