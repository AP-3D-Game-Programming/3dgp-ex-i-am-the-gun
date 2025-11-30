using UnityEngine;

public class GunMovement : MonoBehaviour
{
    //Speed
    [SerializeField] float speed = 7f;
    [SerializeField] float deceleration = 10f;
    Vector3 currentVelocity;
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
        //Fix velocity
        if (input.sqrMagnitude > 0.0001f)
        {
            Vector3 targetVelocity = input.normalized * speed;
            currentVelocity = targetVelocity;
        }
        else
        {
            //Smoothinggg
            currentVelocity = Vector3.Lerp(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.deltaTime
            );
        }

        //Fix Physics conflicten
        gunRb.MovePosition(
            gunRb.position + currentVelocity * Time.deltaTime
        );
    }

    public void Look(Quaternion direction)
    {
        gunRb.transform.rotation *= direction;
    }
}
