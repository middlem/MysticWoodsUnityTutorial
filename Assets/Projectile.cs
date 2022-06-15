using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public int weaponDamage = 1;
    public AudioSource audioSource = null; // Set by weapon
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
       
    }

    private void FixedUpdate()
    {

    }
}
