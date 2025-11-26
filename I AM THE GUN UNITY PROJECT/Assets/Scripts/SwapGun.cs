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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeWeapon(this.gameObject);
        }
    }
}
