using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    private PlayerManager player;

    //Offset
    [SerializeField] Vector3 offset = new Vector3(-2f, 1f, -4f);
    [SerializeField] Vector3 aimOffset = new Vector3(0, 0.5f, -1f);

    //Aiming
    public bool isAiming;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.Gun.transform.rotation;
        transform.position = player.Gun.transform.position;

        if (isAiming)
        {
            transform.position += aimOffset;
        }
        else
        {
            transform.position += offset;
        }
    }
}
