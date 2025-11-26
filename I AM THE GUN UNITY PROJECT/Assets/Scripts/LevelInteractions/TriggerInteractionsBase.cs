using UnityEngine;

public class TriggerInteractionsBase : MonoBehaviour, IInteractable
{
    public GameObject Player { get; set; }
    public bool IsInteractable { get; set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (IsInteractable)
        {
            Debug.Log("Hello world");
        }
    }
    public virtual void Interact()
    {
        
    }
}
