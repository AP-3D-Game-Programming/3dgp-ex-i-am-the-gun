using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public MidGameUIManager midGameUiManager;
    private bool triggered;

    private void Awake()
    {
        if (midGameUiManager == null)
            midGameUiManager = FindAnyObjectByType<MidGameUIManager>();
    }
    private void OnDestroy()
    {
        if (triggered || !Application.isPlaying)
            return;

        triggered = true;

        if (midGameUiManager != null)
            midGameUiManager.ShowChoices();
        else
            GameManager.Instance.CompleteLevel();
    }
}
