using UnityEngine;
using UnityEngine.UI;

public class LevelBanner : MonoBehaviour
{
    [SerializeField] Text text;
    string prefix = "Lv: ";

    // Start is called before the first frame update
    void Start()
    {
        text.text = prefix + "1";
    }

    public void UpdatePlayerLevelText(int playerLevel)
    {
        text.text = prefix + playerLevel;
    }
}
