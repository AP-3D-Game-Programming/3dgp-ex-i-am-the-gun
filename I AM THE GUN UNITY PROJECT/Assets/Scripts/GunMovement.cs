using UnityEngine;

public class GunMovement : MonoBehaviour
{
    //State
    bool ragdol = false;

    //Dependincies
    private Rigidbody gunRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Make this actually function every from
            //No clue why it does not fire every frame but it only works when Move() is called
        if (!ragdol && gunRb.position.y <= 1.5)
        {
            gunRb.position = new Vector3(gunRb.position.x, 1.5f, gunRb.position.z);
        }
    }

    public void Move(Vector3 input)
    {
        //Empty cus playermanager now handles movement
    }
}
