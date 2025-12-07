using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
        public MidGameUpgradeSelector midGameSelector;
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
        // okay so, shows the midgame upgrade selection UI first, after choosing, goes to next, normally
        midGameSelector.GenerateChoices(playerManager);
    }

}
