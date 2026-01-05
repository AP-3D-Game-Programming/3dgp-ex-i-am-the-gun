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

    public GameObject player;

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

    // Load level additively
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

public async void CompleteLevel()
{
    if (currentLevel != -1 && currentLevel < SceneManager.sceneCountInBuildSettings - 1)
    {
        int sceneIndexToUnload = currentLevel + 1;
        Scene unloadScene = SceneManager.GetSceneByBuildIndex(sceneIndexToUnload);

        if (unloadScene.isLoaded)
        {
            await SceneManager.UnloadSceneAsync(sceneIndexToUnload);
        }
    }

    currentLevel++;

    if (currentLevel >= SceneManager.sceneCountInBuildSettings - 1)
    {
        Debug.Log("All levels completed!");
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
        gameIsActive = false;
        gameStarted = false;
        return;
    }

    await SceneManager.LoadSceneAsync(currentLevel + 1, LoadSceneMode.Additive);

    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevel + 1));

    gameIsActive = true;
    gameStarted = true;
    Time.timeScale = 1f;
}


    public void ResetGame()
    {
        gameIsPaused = false;
        gameIsActive = false;
        currentLevel = -1;

        if (player != null)
        {
            player.transform.position = Vector3.zero;
            player.transform.rotation = Quaternion.identity;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }


private void MovePlayerToSpawn(Scene scene)
{
    // Ensure player exists
    if (player == null)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("No player found!");
            return;
        }
    }

    SceneManager.MoveGameObjectToScene(player, scene);

    GameObject spawn = null;
    foreach (var rootObj in scene.GetRootGameObjects())
    {
        spawn = rootObj.transform.Find("PlayerSpawn")?.gameObject;
        if (spawn != null) break;
    }

    if (spawn == null)
    {
        Debug.LogWarning("No PlayerSpawn found in scene " + scene.name);
        return;
    }

    player.transform.position = spawn.transform.position;
    player.transform.rotation = spawn.transform.rotation;

    Rigidbody rb = player.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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
    // Ignore the main menu scene
    if (scene.name == "Main Menu") return;

    MovePlayerToSpawn(scene);
}


}

