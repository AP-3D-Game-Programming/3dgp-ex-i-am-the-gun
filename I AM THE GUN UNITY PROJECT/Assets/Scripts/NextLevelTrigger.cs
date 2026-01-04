using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public MidGameUIManager midGameUiManager;
    private bool triggered;

    private void OnDestroy()
    {
        if (triggered || !Application.isPlaying)
            return;

        triggered = true;

        if (midGameUiManager != null)
            midGameUiManager.ShowChoices();

        GameManager.Instance.CompleteLevel();
    }
}
