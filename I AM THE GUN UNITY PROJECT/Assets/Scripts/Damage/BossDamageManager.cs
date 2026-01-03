using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDamageManager : DamageManager
{
    protected override void Die()
    {
        Destroy(gameObject);
        Debug.Log("Boss defeated!");
        // first let choose between upgrades
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
