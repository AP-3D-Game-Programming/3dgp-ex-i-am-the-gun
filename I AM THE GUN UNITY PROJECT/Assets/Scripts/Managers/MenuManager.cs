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
        //if (startButton != null)
        //{
        //    startButton.SetActive(false);
        //}
        // Below example of
        // shortening the if statement
        startButton?.SetActive(false);

        resumeButton?.SetActive(false);
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

            resumeButton?.SetActive(true);
        }
    }
    public void ResumeToggle()
    {
        if (GameManager.Instance.gameIsPaused)
        {
            menuScreen?.gameObject.SetActive(false);

            GameManager.Instance.TogglePause();

            resumeButton?.SetActive(false);
        }
    }

    public async void Quit()
    {
        menuScreen?.gameObject.SetActive(false);

        if (GameManager.Instance != null && GameManager.Instance.gameIsPaused)
        {
            GameManager.Instance.TogglePause();
        }
        await SceneManager.LoadSceneAsync(0);
        ResetUI();
    }

    private void ResetUI()
    {
        //if (GameManager.Instance != null)
        //{
        //    GameManager.Instance.TogglePause();
        //}
        GameManager.Instance?.TogglePause();

        startButton?.SetActive(true);

        resumeButton?.SetActive(false);

        menuScreen?.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
