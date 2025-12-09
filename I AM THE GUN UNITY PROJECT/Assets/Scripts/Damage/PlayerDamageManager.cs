using UnityEngine;

public class PlayerDamageManager : DamageManager
{
    public PlayerUpgradeManager upgradeManager;
    public PlayerManager playerManager;
    protected override void Die()
    {
        Debug.Log("PLAYER DEAD!");
        
        // deletes mid-game upgrades (no carry-over)
        upgradeManager.ClearMidGame();

        //drops weapon on floor (little spice, hope it works)
        GameObject weapon = GameObject.FindWithTag("Weapon"); // all gun prefabs normally have this
        Instantiate(weapon, transform.position, Quaternion.identity);

        // go to main menu (maybe move this to death screen script later, if ever get death screen)
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

