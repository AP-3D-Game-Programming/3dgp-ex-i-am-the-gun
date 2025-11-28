using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool gameIsActive {  get; private set; }
    public bool gameStarted { get; private set; }
    [SerializeField] int currentLevel;
    void Awake()
    {
        currentLevel = -1;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public async void LoadLevel(int level)
    {
        if (currentLevel != -1 && currentLevel != SceneManager.sceneCountInBuildSettings - 1)
            await SceneManager.UnloadSceneAsync(currentLevel + 1);
        currentLevel = level;
        //If main needs to stay loaded: (Main needs to stay loaded for the camera lol)
        await SceneManager.LoadSceneAsync(currentLevel + 1, LoadSceneMode.Additive);
        gameIsActive = true;
        gameStarted = true;
    }
}

