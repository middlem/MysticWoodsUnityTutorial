using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePosition;


    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
