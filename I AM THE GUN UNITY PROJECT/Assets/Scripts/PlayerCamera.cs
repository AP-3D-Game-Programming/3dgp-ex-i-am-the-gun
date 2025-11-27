using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    private PlayerManager player;

    //Offset
    [SerializeField] float xOffset = -2;
    [SerializeField] float yOffset = 1;
    [SerializeField] float zOffset = -2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.Gun.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.rotation = player.Gun.transform.rotation;
    }
}
