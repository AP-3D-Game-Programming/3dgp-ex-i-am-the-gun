using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject shooter;
    public int damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == shooter)
            return;

        DamageManager dmg = collision.gameObject.GetComponent<DamageManager>();
        if (dmg != null)
        {
            dmg.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
