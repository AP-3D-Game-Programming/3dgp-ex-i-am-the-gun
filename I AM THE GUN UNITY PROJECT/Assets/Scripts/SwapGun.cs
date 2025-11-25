using UnityEngine;

public class SwapGun : MonoBehaviour
{
    //PlayerMovement
    private PlayerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (true)
        {
            GameObject newGun = other.gameObject;
            player.ChangeWeapon(newGun);
        }
    }
}
