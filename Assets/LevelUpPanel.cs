using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] public GameObject levelUpPanel;

    public void UnpauseGame()
    {
        Time.timeScale = 1; // Unpause Game
        levelUpPanel.SetActive(false);
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
