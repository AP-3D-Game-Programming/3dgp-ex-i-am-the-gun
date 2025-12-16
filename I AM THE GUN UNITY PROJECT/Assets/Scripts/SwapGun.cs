using UnityEngine;

public class SwapGun : MonoBehaviour
{
    private UseWeapon player;
    private bool isPlayerInside = false; //track if player is near

    void Start()
    {
        // Finding Player1 as a child of Player1 or in the scene
        player = GameObject.Find("Character1").GetComponent<UseWeapon>();
    }

    void Update()
    {
        // 1. Check input in Update (Every Frame)
        if (isPlayerInside && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Input Detected! Swapping...");
            player.ChangeWeapon(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 2. Set the flag when player enters
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered gun range: " + other.name);
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 3. Unset the flag when player leaves
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}