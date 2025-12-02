using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    [SerializeField] private PlayerManager player;

    //Offset
    [SerializeField] Vector3 offset = new Vector3(0.5f, -0.5f, 1f);
    [SerializeField] Vector3 aimOffset = new Vector3(0, -0.2f, 0.5f);
    Vector3 targetPosition;

    //Aiming
    public bool isAiming;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("PlayerCamera mist de PlayerManager. Sleep in de inspector AUB.");
        }
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) 
            return;

        transform.localPosition = Vector3.zero;
        

        if (isAiming)
        {
            targetPosition = aimOffset;
        }
        else
        {
            targetPosition = offset;
        }

        player.Gun.transform.localPosition = Vector3.Lerp(
            player.Gun.transform.localPosition,
            targetPosition,
            Time.deltaTime * 10f //smooooth
        );
    }

    internal void Initialize(Transform transform)
    {
        throw new NotImplementedException();
    }
}
