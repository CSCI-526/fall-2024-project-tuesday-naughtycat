using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeShop : MonoBehaviour
{
    public Button bulletSpeedButton;
    public Button bulletRangeButton;
    public Button bulletDamageButton;
    public Button bulletPenetrationButton;
    public Button reloadSpeedButton;
    public Button healthRegenerationButton;
    public Button maxHealthButton;
    public Button movementSpeedButton;
    public Button bodyDamageButton;
    public TextMeshProUGUI coinText;

    void Start()
    {
        // Assign button listeners
        bulletSpeedButton.onClick.AddListener(() => UpgradeGun("Speed"));
        bulletRangeButton.onClick.AddListener(() => UpgradeGun("Range"));
        bulletDamageButton.onClick.AddListener(() => UpgradeGun("Damage"));
        bulletPenetrationButton.onClick.AddListener(() => UpgradeGun("Penetration"));
        reloadSpeedButton.onClick.AddListener(() => UpgradeGun("Reload"));
        healthRegenerationButton.onClick.AddListener(() => UpgradeStat("Regenerate"));
        maxHealthButton.onClick.AddListener(() => UpgradeStat("MaxHealth"));
        movementSpeedButton.onClick.AddListener(() => UpgradeStat("MovementSpeed"));
        bodyDamageButton.onClick.AddListener(() => UpgradeStat("BodyDamage"));

    }

    void Update()
    {
    }

    void UpgradeGun(string upgradeType)
    {
        // Define the cost for each gun upgrade
        // keeping all costs the same for now
        int cost = 0;
        switch (upgradeType)
        {
            case "Speed":
                cost = 10;
                break;
            case "Range":
                cost = 10;
                break;
            case "Damage":
                cost = 10;
                break;
            case "Penetration":
                cost = 10;
                break;
            case "Reload":
                cost = 10;
                break;
        }

        if (GameManager.instance.SpendCoins(cost))
        {
            coinText.text = "Coins: " + GameManager.instance.playerCoins.ToString();
            GameManager.instance.UpgradeGun(upgradeType);
            Debug.Log(upgradeType + " upgraded!");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade " + upgradeType);
        }
    }

    void UpgradeStat(string statType)
    {
        // Define the cost for each stat upgrade
        // keeping all costs the same for now
        int cost = 0;
        switch (statType)
        {
            case "Regenerate":
                cost = 10;
                break;
            case "MaxHealth":
                cost = 10;
                break;
            case "MovementSpeed":
                cost = 10;
                break;
            case "BodyDamage":
                cost = 10;
                break;
        }

        if (GameManager.instance.SpendCoins(cost))
        {
            coinText.text = "Coins: " + GameManager.instance.playerCoins.ToString();
            GameManager.instance.UpgradeStat(statType);
            Debug.Log(statType + " upgraded!");
        }
        else
        {
            Debug.Log("Not enough coins to upgrade " + statType);
        }
    }
}
