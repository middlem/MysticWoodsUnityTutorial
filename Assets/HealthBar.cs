using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform bar; 

    public void SetState(int currentHp, int maxHp)
    {
        float state = (float)currentHp;
        state/=maxHp;
        if(state < 0f) {  state = 0f; }
        bar.transform.localScale = new Vector3 (state, 1f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
