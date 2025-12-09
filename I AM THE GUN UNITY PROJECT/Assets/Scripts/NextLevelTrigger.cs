using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
        public MidGameUIManager midGameUiManager;
public PlayerUpgradeManager playerManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnLevelEnd();
        }
    }

    public void OnLevelEnd()
    {
        midGameUiManager.ShowChoices();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
