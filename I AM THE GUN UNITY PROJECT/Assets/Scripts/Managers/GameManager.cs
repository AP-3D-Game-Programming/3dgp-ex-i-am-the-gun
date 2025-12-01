using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool gameIsActive {  get;  private set; } = false;
    public bool gameStarted { get; private set; } = false;
    //public bool gameIsPaused { get; private set; } = false;
    [SerializeField] public bool gameIsPaused = false;
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
        gameIsActive = false;

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
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (!gameIsActive)
            return;

        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None; // Unlock cursor to interact with menu
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor for FPS control
            Cursor.visible = false;
        }

    }


}

