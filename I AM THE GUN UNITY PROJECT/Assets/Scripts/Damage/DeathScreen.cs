using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject screen;
    void Awake()
{
    PlayerDamageManager player = GameObject.Find("Player1").GetComponent<PlayerDamageManager>();
    player.deathScreen = this;
    screen.SetActive(false);

    if (FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
    {
        GameObject es = new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
    }
}


    public void Show()
    {
        screen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        GameObject.Find("Player1").GetComponent<PlayerDamageManager>().isDead = false;
    }

    public void Retry()
{
    Time.timeScale = 1f;

    PlayerDamageManager player = GameObject.Find("Player1").GetComponent<PlayerDamageManager>();
    player.isDead = false;
    player.useWeapon.cartridgesCount = player.useWeapon.CartridgesCapacity;
    player.useWeapon.Weapon.GetComponent<Gun>().BulletCount = player.useWeapon.Weapon.GetComponent<Gun>().BulletCapacity; 
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

}
