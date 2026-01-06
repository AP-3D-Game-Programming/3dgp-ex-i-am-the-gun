using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject shooter;
    public int damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.gameObject == shooter)
        return;


        DamageManager dmg = collision.transform.root.GetComponent<DamageManager>();
        Debug.Log(dmg);
        Debug.Log("Bullet hit " + collision.gameObject.name);
        if (dmg != null)
        {
            dmg.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
