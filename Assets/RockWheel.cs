using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWheel : MonoBehaviour
{

    float distanceFromPlayer = 0.5f;
    float currentRotationOnPlayer = 0f;
    float rotationSpeed = 0.06f;
    //float currentSpriteRotation = 0f;
    //float spriteRotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void FixedUpdate()
    {
        var player = GameObject.FindWithTag("Player").transform.position;

        currentRotationOnPlayer += rotationSpeed;
        currentRotationOnPlayer = currentRotationOnPlayer % 360;

        Vector3 newPos = new Vector3(
            player.x + ((float)Math.Cos(currentRotationOnPlayer)) * distanceFromPlayer,
            player.y + ((float)Math.Sin(currentRotationOnPlayer)) * distanceFromPlayer,
            player.z);

        Quaternion newRotation = new Quaternion();
        this.transform.SetPositionAndRotation(newPos, newRotation);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= 10;
            }
        }
        else if (other.tag == "Boss")
        {
            Boss enemy = other.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.Health -= 10;
                Debug.Log("Morbin health: " + enemy.Health);
                if (enemy.Health <= 0)
                {
                    enemy.RemoveMe();
                }
            }
        }
    }
}
