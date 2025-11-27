using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
    public Upgrade upgrade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mgr = other.GetComponent<PlayerUpgradeManager>();
            if (mgr != null)
            {
                mgr.ApplyUpgrade(upgrade);
                Destroy(gameObject);
            }
        }
    }
}
