using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;
    
    [SerializeField] private int kaijusToDefeat = 10;
    private int defeatedKaijus = 0;
    
    [Header("UI References")]
    [SerializeField] private Slider progressBar;
    
    [Header("Scene Management")]
    [SerializeField] private string phase1SceneName = "Phase1";
    [SerializeField] private string phase2SceneName = "Phase2";
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    
    public event System.Action OnPhaseComplete;
    
    // Properties for external access
    public int DefeatedKaijus => defeatedKaijus;
    public int TotalKaijus => kaijusToDefeat;
    public int KaijusToDefeat => kaijusToDefeat;
    public bool IsPhaseComplete => defeatedKaijus >= kaijusToDefeat;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset phase progress when loading a new phase scene
        if (scene.name == phase1SceneName || scene.name == phase2SceneName)
        {
            ResetPhase();
            
            // Find new progress bar in the scene
            progressBar = FindObjectOfType<Slider>();
            if (progressBar != null)
            {
                UpdateProgressBar();
            }
        }
    }
    
    private void Start()
    {
        // Initialize progress bar
        UpdateProgressBar();
    }
    
    public void RegisterKaijuDefeat()
    {
        if (IsPhaseComplete) return;
        
        defeatedKaijus++;
        Debug.Log($"Kaijus defeated: {defeatedKaijus}/{kaijusToDefeat}");
        
        UpdateProgressBar();
        
        if (IsPhaseComplete)
        {
            Debug.Log("Phase Complete!");
            OnPhaseComplete?.Invoke();
            HandlePhaseCompletion();
        }
    }
    
    private void HandlePhaseCompletion() {
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == phase1SceneName) {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.phaseTransition);
            AudioManager.Instance.PlayMusic(AudioManager.Instance.phase2Music);
            LoadPhase2();
        }
        else if (currentScene == phase2SceneName) {
            ReturnToMainMenu();
        }
    }
    
    public float GetProgress()
    {
        return (float)defeatedKaijus / kaijusToDefeat;
    }
    
    private void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.value = GetProgress();
        }
        
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
    
    // Scene management methods
    public void LoadPhase1()
    {
        SceneManager.LoadScene(phase1SceneName);
    }
    
    public void LoadPhase2()
    {
        SceneManager.LoadScene(phase2SceneName);
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}