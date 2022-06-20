using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int weaponLevel = 1;
    public int maxWeaponLevel = 1;
    public IDictionary<int, string> upgradeDescriptions;

    public string weaponName = "";
    public Image weaponIcon = null;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip weaponDamageSound = null;
    [SerializeField] public GameObject my_projectile;
    //List<Projectile> weapon_projectiles;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        upgradeDescriptions = new Dictionary<int, string>();
        Projectile projectile = my_projectile.GetComponent<Projectile>();
        audioSource.clip = weaponDamageSound;
        projectile.audioSource = audioSource;
    }

    public virtual void upgradeWeapon()
    {
        if(weaponLevel < maxWeaponLevel)
        {
            weaponLevel++;
        }
        else
        {
            Debug.Log(weaponName + " exceeding max weapon level of " + maxWeaponLevel);
        }

    }

    protected virtual void FixedUpdate()
    {

    }
}
