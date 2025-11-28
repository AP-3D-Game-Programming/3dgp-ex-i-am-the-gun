using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Input
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    private float xMouse;
    private float yMouse;
    [SerializeField] float xSensitivity = 15f;
    [SerializeField] float ySensitivity = 15f;

    //Dependencies
    [SerializeField] GameObject gun;
    private GunMovement movement;
    private Gun usage;
    [SerializeField] GameObject camera;
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
        usage = gun.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        //get inputs
            //keyboard
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //mouse
        xMouse = Input.GetAxis("Mouse X") * xSensitivity;
        yMouse = Input.GetAxis("Mouse Y") * ySensitivity;

        Vector3 move = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Quaternion look = new Quaternion(xMouse, yMouse, 0, 0);

        if (move != Vector3.zero)
        {
            movement.Move(move);
        }

        movement.Look(look);

        //LeftMouse
        if (Input.GetMouseButtonDown(0))
        {
            //Firing mechanic
            usage.FireWeapon();
        }

        //RightMouse
        if (Input.GetMouseButton(1))
        {
            //Aim gun mechanic
            camera.GetComponent<PlayerCamera>().isAiming = true;
        }
        else
        {
            camera.GetComponent<PlayerCamera>().isAiming = false;
        }
    }

    //When player switches weapon
    public void ChangeWeapon(GameObject newGun)
    {
        gun = newGun;
        movement = gun.GetComponent<GunMovement>();
    }
}
