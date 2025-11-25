using UnityEngine;

public class GunMovement : MonoBehaviour
{
    //Speed
    float speed = 10f;

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
        
    }

    public void Move(Vector3 move)
    {
        gunRb.linearVelocity = move * speed * Time.deltaTime;
    }
}
