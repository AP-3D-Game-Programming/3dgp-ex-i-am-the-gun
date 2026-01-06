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
        //Disconnect AI from gun
        AIFireController aiController = GetComponentInParent<AIFireController>();
        if (aiController != null)
        {
            aiController.gun = null;
            aiController.target = null;
        }

        //Kill AI brain + movement
        if (behaviorAgent != null) behaviorAgent.enabled = false;
        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.enabled = false;
        }

        //Convert gun to player ownership
        Gun gunScript = GetComponent<Gun>();
        if (gunScript != null)
        {
            gunScript.IsPlayerGun = true;
            gunScript.enabled = true;

            //Reset bullet rotation
            if (gunScript.BulletSpawn != null)
                gunScript.BulletSpawn.localRotation = Quaternion.identity;
        }

        //Swap
        player.ChangeWeapon(gameObject);
        if (gunScript != null)
    {
        gunScript.enabled = false; 
        gunScript.IsPlayerGun = false; 
    }

        //Clean up AI shell
        //Hide original gun mesh
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            if (r.gameObject.name.Contains("Muzzle") || r.gameObject.name.Contains("Flash")) continue;
            r.enabled = false;
        }

        //Disable pickup triggers
        foreach (var c in GetComponentsInChildren<Collider>())
            c.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Player entered gun range: " + other.name);
        if (other.CompareTag("Player"))
            isPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Left gun range: " + other.name);
        if (other.CompareTag("Player"))
            isPlayerInside = false;
    }
}
