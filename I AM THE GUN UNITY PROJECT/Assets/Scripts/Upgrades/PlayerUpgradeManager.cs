using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerUpgradeManager : MonoBehaviour
{
    private static PlayerUpgradeManager instance;

    public PlayerCharacter1 PlayerStats;

    private List<MidGameUpgrade> midGameUpgrades = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find PlayerCharacter1 in the new scene and assign it
        PlayerStats = FindObjectOfType<PlayerCharacter1>();

        // Reapply all upgrades to the new player stats instance
        if (PlayerStats != null)
        {
            foreach (var upgrade in midGameUpgrades)
            {
                upgrade.OnApply(this);
            }
        }
        else
        {
            Debug.LogWarning("PlayerCharacter1 not found on scene load.");
        }
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        upgrade.OnApply(this);

        if (upgrade is MidGameUpgrade mid)
            midGameUpgrades.Add(mid);
    }

    public List<MidGameUpgrade> GetMidGameUpgrades() => midGameUpgrades;

    public void ClearMidGame()
    {
        midGameUpgrades.Clear();
    }
}
