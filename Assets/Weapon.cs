using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int upgradeLevel = 0;
    public int maxUpgradeLevel = 0;
    IDictionary<int, string> upgradeDescriptions;

    public string weaponName = "";
    public Image weaponIcon = null;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip weaponDamageSound = null;
    [SerializeField] public GameObject my_projectile;
    //List<Projectile> weapon_projectiles;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Projectile projectile = my_projectile.GetComponent<Projectile>();
        audioSource.clip = weaponDamageSound;
        projectile.audioSource = audioSource;
    }

    protected virtual void FixedUpdate()
    {

    }
}
