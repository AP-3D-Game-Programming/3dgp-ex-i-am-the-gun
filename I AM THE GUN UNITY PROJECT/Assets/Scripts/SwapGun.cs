using UnityEngine;

public class SwapGun : MonoBehaviour
{
    //PlayerMovement
    private PlayerManager player;
    private bool playerInHitbox = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInHitbox && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.E)))
            {
            if (player != null)
            {
                player.ChangeWeapon(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInHitbox = true;
            Debug.Log("Near a gun: Press R or E to swamp.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInHitbox = false;
        }
    }
}
