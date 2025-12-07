using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    //Dependencies
    public GameObject Weapon;
    [SerializeField] GameObject camera;
    private Gun weaponUtility;

    //Stats
    [SerializeField] int cartridgesCount = 3;
    public int CartridgesCapacity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponUtility = Weapon.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        LeftMouseFire();
    }

    void LeftMouseFire()
    {
        //LeftMouse
        if (Input.GetMouseButtonDown(0))
        {
            //Firing mechanic
            weaponUtility.FireWeapon();
            if (weaponUtility.BulletCount <= 0)
            {
                if (cartridgesCount > 0)
                {
                    cartridgesCount -= 1;
                    weaponUtility.BulletCount = weaponUtility.BulletCapacity;
                }
                else
                {
                    //death mechanic
                }
            }
        }
    }

    public void ChangeWeapon(GameObject newGun)
    {
        Destroy(Weapon);
        Weapon = Instantiate(newGun, camera.transform.position, camera.transform.rotation, camera.transform);
        weaponUtility = Weapon.GetComponent<Gun>();
        weaponUtility.BulletCount = weaponUtility.BulletCapacity;
        cartridgesCount = CartridgesCapacity;
    }
}
