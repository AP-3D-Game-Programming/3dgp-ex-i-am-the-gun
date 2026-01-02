using UnityEngine;
using UnityEngine.AI;

public class SwapGun : MonoBehaviour
{
    private UseWeapon player;
    private bool isPlayerInside;

    private NavMeshAgent agent;
    private Behaviour behaviorAgent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        behaviorAgent = GetComponent<Behaviour>();
    }

    void Start()
    {
        player = GameObject.Find("Character1").GetComponent<UseWeapon>();
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.R))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Swapping gun (hide world gun (Still needs to be fixed))");

        // Remove AI
        if (behaviorAgent != null)
            behaviorAgent.enabled = false;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.enabled = false;
        }
        // Swap weapon :o
        player.ChangeWeapon(gameObject);
        // Hide gun
        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = false;

        // Disable colliders so it can't be picked again
        foreach (var c in GetComponentsInChildren<Collider>())
            c.enabled = false;



    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered gun range: " + other.name);
        if (other.CompareTag("Player"))
            isPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left gun range: " + other.name);
        if (other.CompareTag("Player"))
            isPlayerInside = false;
    }
}
