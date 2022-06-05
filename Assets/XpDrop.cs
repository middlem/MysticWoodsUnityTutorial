using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpDrop : MonoBehaviour
{
    public int xpValue = 5;
    public AudioClip pickupClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveDrop()
    {
        Destroy(gameObject);
    }
}
