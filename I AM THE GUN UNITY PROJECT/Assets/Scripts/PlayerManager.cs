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
    [SerializeField] GameObject camera;
    private GunMovement movement;
    private Gun usage;

    //Stats
    [SerializeField] int cartridgesCount;
    public int CartridgesCapacity;

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
        Inputs();
        Movement();
        MouseLooking();
        LeftMouseFire();
        RightMouseAiming();
    }
    void Inputs()
    {    //get inputs
        //keyboard
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //mouse
        xMouse = Input.GetAxis("Mouse X") * xSensitivity;
        yMouse = Input.GetAxis("Mouse Y") * ySensitivity;
    }

    void Movement()
    {
        Vector3 move = new(horizontalInput, 0f, verticalInput);
        movement.Move(move);
    }
    void MouseLooking()
    {
    Quaternion look = Quaternion.Euler(xMouse, yMouse, 0);
    movement.Look(look);
    }
    void LeftMouseFire()
    {
            //LeftMouse
        if (Input.GetMouseButtonDown(0))
        {
            //Firing mechanic
            usage.FireWeapon();
            if (usage.BulletCount <= 0)
            {
                if (cartridgesCount > 0)
                {
                    cartridgesCount -= 1;
                    usage.BulletCount = usage.BulletCapacity;
                }
                else
                {
                    //death mechanic
                }
            }
        }
    }

    void RightMouseAiming()
    {
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
//When player switches weapon}
public void ChangeWeapon(GameObject newGun)
    {
        gun = newGun;
        movement = gun.GetComponent<GunMovement>();
        cartridgesCount = CartridgesCapacity;
    }
}
