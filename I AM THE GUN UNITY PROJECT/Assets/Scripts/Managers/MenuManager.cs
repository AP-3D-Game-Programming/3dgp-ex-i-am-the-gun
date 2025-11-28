using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public void PlayGame()
    //    {
    //        SceneManager.LoadSceneAsync(1);
    //    }
    [SerializeField] Canvas menuScreen;
    public void PlayGame()
    {
        menuScreen.gameObject.SetActive(false);
        GameManager.Instance.LoadLevel(0);
    }
}
