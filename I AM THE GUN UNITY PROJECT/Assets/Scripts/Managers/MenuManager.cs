using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas menuScreen;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject pauseButton;

    private bool paused => GameManager.Instance.gameIsPaused;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
            Debug.Log($"Scene is paused: {GameManager.Instance.gameIsPaused}");
        }
    }
    public void StartGame()
    {
        menuScreen.gameObject.SetActive(false);

        GameManager.Instance.LoadLevel(0);
        startButton.SetActive(false);
    }

    public void PauseToggle()
    {
        menuScreen.gameObject.SetActive(true);
        if (!GameManager.Instance.gameIsPaused)
        {
            if (!GameManager.Instance.gameIsActive)
            {
                return;
            }
            GameManager.Instance.TogglePause();
            pauseButton.SetActive(true);
        }
    }
    public void ResumeToggle()
    {
        if (GameManager.Instance.gameIsPaused)
        {
            menuScreen.gameObject.SetActive(false);
            GameManager.Instance.TogglePause();
        }
    }

    public async void Quit()
    {
        await SceneManager.LoadSceneAsync(0);
        ResetUI();
    }

    private void ResetUI()
    {
        GameManager.Instance.TogglePause();
        startButton.SetActive(true);
        pauseButton.SetActive(false);
        menuScreen.gameObject.SetActive(true);
    }

}
