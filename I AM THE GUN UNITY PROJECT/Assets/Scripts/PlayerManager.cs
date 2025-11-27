using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Input
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    //Dependencies
    [SerializeField] GameObject gun;
    private GunMovement movement;
    public GameObject Gun 
    {
        get
        {
            return gun;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = gun.GetComponent<GunMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(verticalInput, 0f, horizontalInput).normalized;

        if (move != Vector3.zero)
        {
            movement.Move(move);
        }

        //LeftMouse
        if (Input.GetMouseButtonDown(0))
        {
            //Shoot gun mechanic
        }

        //RightMouse
        if (Input.GetMouseButtonDown(1))
        {
            //Aim gun mechanic
        }
    }

    //When player switches weapon
    public void ChangeWeapon(GameObject newGun)
    {
        gun = newGun;
        movement = gun.GetComponent<GunMovement>();
    }
}
