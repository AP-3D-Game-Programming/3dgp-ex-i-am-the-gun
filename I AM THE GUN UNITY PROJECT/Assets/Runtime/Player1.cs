using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private PlayerCharacter playerCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCharacter.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
