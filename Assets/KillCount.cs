using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCount : MonoBehaviour
{
    public int killCount;
    [SerializeField] Text text;
    string prefix = "Kills: ";

    // Start is called before the first frame update
    void Start()
    {
        text.text = prefix + killCount;
    }

    public void addOneToKillCount()
    {
        killCount++;
        UpdateKillCountText();
    }

    public void UpdateKillCountText()
    {
        text.text = prefix + killCount;
    }

}
