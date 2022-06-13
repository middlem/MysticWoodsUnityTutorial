using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int weaponDamage = 1;
    public int upgradeLevel = 0;
    public int maxUpgradeLevel = 0;
    IDictionary<int, string> upgradeDescriptions;

    public string weaponName = "";
    public Image weaponIcon = null;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip weaponDamageSound = null;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioSource.clip = this.weaponDamageSound;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }
}
