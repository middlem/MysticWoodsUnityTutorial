using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] public GameObject levelUpPanel;
    [SerializeField] public List<GameObject> weaponUpgrades;
    [SerializeField] public Button upgrade_option1;
    [SerializeField] public Button upgrade_option2;
    [SerializeField] public Button upgrade_option3;

    private Weapon weapon_option1;
    private Weapon weapon_option2;
    private Weapon weapon_option3;

    public void SelectOptionOne()
    {
        weapon_option1.upgradeWeapon();
        UnpauseGame();
    }

    public void SelectOptionTwo()
    {
        weapon_option2.upgradeWeapon();
        UnpauseGame();
    }

    public void SelectOptionThree()
    {
        weapon_option3.upgradeWeapon();
        UnpauseGame();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1; // Unpause Game
        levelUpPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        randomizeUpgradeOptions();
    }

    public void randomizeUpgradeOptions()
    {
        int upgrade_count = weaponUpgrades.Count;
        var random_nums_used = new HashSet<int>();

        //Option 1
        int random_num = Random.Range(0, upgrade_count);
        random_nums_used.Add(random_num);
        weapon_option1 = weaponUpgrades[random_num].GetComponent<Weapon>();
        setWeaponUpgradeButtonInfo(upgrade_option1.GetComponent<UpgradeButton>(), weapon_option1);

        //Option 2
        random_num = getRandomNumberNotInSet(random_nums_used);
        random_nums_used.Add(random_num);
        weapon_option2 = weaponUpgrades[random_num].GetComponent<Weapon>();
        setWeaponUpgradeButtonInfo(upgrade_option2.GetComponent<UpgradeButton>(), weapon_option2);

        //Option 3
        random_num = getRandomNumberNotInSet(random_nums_used);
        weapon_option3 = weaponUpgrades[random_num].GetComponent<Weapon>();
        setWeaponUpgradeButtonInfo(upgrade_option3.GetComponent<UpgradeButton>(), weapon_option3);
    }

    private void setWeaponUpgradeButtonInfo(UpgradeButton upgrade_button, Weapon weapon)
    {
        upgrade_button.upgrade_weapon_name.text = weapon.weaponName;
        // If out of bounds of description, just use current weapon level description
        upgrade_button.upgrade_weapon_description.text = weapon.weaponLevel + 1 > weapon.upgradeDescriptions.Count ?
            weapon.upgradeDescriptions[weapon.weaponLevel] : weapon.upgradeDescriptions[weapon.weaponLevel + 1];
    }

    private int getRandomNumberNotInSet(HashSet<int> numbers_to_exclude)
    {
        int upgrade_count = weaponUpgrades.Count;
        var randomize_int_map = new Dictionary<int, int>();
        int map_index = 0;
        for (int i = 0; i < upgrade_count; i++)
        {
            if (!numbers_to_exclude.Contains(i))
            {
                randomize_int_map[map_index] = i;
                map_index++;
            }
        }

        return randomize_int_map[Random.Range(0, map_index)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
