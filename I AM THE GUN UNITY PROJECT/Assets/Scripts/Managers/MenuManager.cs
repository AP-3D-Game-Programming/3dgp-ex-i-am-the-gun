using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas menuScreen;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject resumeButton;



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
        if (startButton != null)
        {
            startButton.SetActive(false);
        }

        if (resumeButton != null)
        {
            resumeButton.SetActive(false);
        }
    }

    public void PauseToggle()
    {
        if (menuScreen != null)
        {
            menuScreen.gameObject.SetActive(true);
        }
        if (!GameManager.Instance.gameIsPaused)
        {
            if (!GameManager.Instance.gameIsActive)
            {
                return;
            }
            GameManager.Instance.TogglePause();
            if (resumeButton != null)
            {
                resumeButton.SetActive(true);
            }
        }
    }
    public void ResumeToggle()
    {
        if (GameManager.Instance.gameIsPaused)
        {
            if (menuScreen != null)
            {
                menuScreen.gameObject.SetActive(false);
            }
            GameManager.Instance.TogglePause();
            if (resumeButton != null) { resumeButton.SetActive(false); }
        }
    }

    public async void Quit()
    {
        if (menuScreen != null)
        {
            menuScreen.gameObject.SetActive(false);
        }

        if (GameManager.Instance != null && GameManager.Instance.gameIsPaused)
        {
            GameManager.Instance.TogglePause();
        }
        await SceneManager.LoadSceneAsync(0);
        ResetUI();
    }

    private void ResetUI()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TogglePause();
        }

        if (startButton != null)
        {
            startButton.SetActive(true);
        }

        if (resumeButton != null)
        {
            resumeButton.SetActive(false);
        }

        if (menuScreen != null)
        {
            menuScreen.gameObject.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
