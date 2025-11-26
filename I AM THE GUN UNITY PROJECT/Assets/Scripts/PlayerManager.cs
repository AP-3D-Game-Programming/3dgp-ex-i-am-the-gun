using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Input
    private float horizontalInput;
    private float verticalInput;

    //Dependencies
    [SerializeField] GameObject gun;
    private GunMovement movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = gun.GetComponent<GunMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(verticalInput, 0f, horizontalInput).normalized;

        if (move != Vector3.zero)
        {
            movement.Move(move);
        }
    }

    //When player switches weapon
    public void ChangeWeapon(GameObject newGun)
    {
        gun = newGun;
        movement = gun.GetComponent<GunMovement>();
    }
}
