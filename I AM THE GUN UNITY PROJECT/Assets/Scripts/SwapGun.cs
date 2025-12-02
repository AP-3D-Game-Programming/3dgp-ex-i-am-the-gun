using UnityEngine;

public class SwapGun : MonoBehaviour
{
    //PlayerMovement
    private PlayerManager player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Near a gun: Press R to swamp.");
            player.ChangeWeapon(gameObject);
            Destroy(gameObject);
        }
    }
}
