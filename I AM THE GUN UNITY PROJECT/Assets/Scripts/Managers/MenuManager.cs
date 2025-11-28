using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas menuScreen;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject pauseButton;

    bool paused = GameManager.Instance.gameIsPaused;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
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
        if (!paused)
        {
            if (!GameManager.Instance.gameIsActive)
            {
                return;
            }
            GameManager.Instance.TogglePause();
            pauseButton.SetActive(true);
            paused = true;
        }
    }
    public void ResumeToggle()
    {
        if (paused)
        {
            menuScreen.gameObject.SetActive(false);
        }
    }

}
