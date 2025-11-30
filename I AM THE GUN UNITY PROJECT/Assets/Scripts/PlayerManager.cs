using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Input
    [SerializeField] float speed = 7f;
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] float xSensitivity = 5f;
    [SerializeField] float ySensitivity = 5f;
    [SerializeField] float deceleration = 10f;
    private float xMouse;
    private float yMouse;
    private float xRotation = 0f;
    private Vector3 currentVelocity;
    

    //Dependencies
    [SerializeField] GameObject gun;
    [SerializeField] GameObject camera;
    private GunMovement movement;
    private Gun usage;

    //Stats
    [SerializeField] int cartridgesCount;
    public int CartridgesCapacity;

    public GameObject Gun => gun;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = gun.GetComponent<GunMovement>();
        usage = gun.GetComponent<Gun>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RunGameLogic();
    }

    void RunGameLogic()
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

        Vector3 targetDirection = transform.TransformDirection(move);

        //SMOOOTHING
        if (targetDirection.sqrMagnitude > 0.001f)
        {
            currentVelocity = targetDirection * speed;
        }
        else
        {
            //SLOW DOWN
            currentVelocity = Vector3.Lerp(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.deltaTime
            );
        }

        //Actual movement.
        transform.position += currentVelocity * Time.deltaTime;
    }
    void MouseLooking()
    {
        transform.rotation *= Quaternion.Euler(0, xMouse, 0);

        xRotation -= yMouse;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        gun.transform.localRotation = Quaternion.identity;
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
