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
    public Button shrinkResistanceButton;
    public Button maxHealthButton;
    public Button movementSpeedButton;
    public Button bodyDamageButton;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI outOfCoinsText;

    void Start()
    {
        // Assign button listeners
        bulletSpeedButton.onClick.AddListener(() => UpgradeGun("Speed"));
        bulletRangeButton.onClick.AddListener(() => UpgradeGun("Range"));
        bulletDamageButton.onClick.AddListener(() => UpgradeGun("Damage"));
        bulletPenetrationButton.onClick.AddListener(() => UpgradeGun("Penetration"));
        reloadSpeedButton.onClick.AddListener(() => UpgradeGun("Reload"));
        shrinkResistanceButton.onClick.AddListener(() => UpgradeStat("ShrinkResistance"));
        maxHealthButton.onClick.AddListener(() => UpgradeStat("MaxHealth"));
        movementSpeedButton.onClick.AddListener(() => UpgradeStat("MovementSpeed"));
        bodyDamageButton.onClick.AddListener(() => UpgradeStat("BodyDamage"));

        UpdateButtonStates();
    }

    void UpgradeGun(string upgradeType)
    {
        // Define the cost for each gun upgrade
        // Keeping all costs the same for now
        int cost = 10; // Set to 10 as per your original code

        if (GameManager.instance.SpendCoins(cost))
        {
            GameManager.instance.UpgradeGun(upgradeType);
            coinText.text = GameManager.instance.playerCoins.ToString();
            Debug.Log(upgradeType + " upgraded!");

            // Update button states after upgrade
            UpdateButtonStates();
        }
        else
        {
            Debug.Log("Not enough coins to upgrade " + upgradeType);
            ShowOutOfCoinsMessage();
        }
    }

    void UpgradeStat(string statType)
    {
        // Define the cost for each stat upgrade
        // Keeping all costs the same for now
        int cost = 10; // Set to 10 as per your original code

        if (GameManager.instance.SpendCoins(cost))
        {
            GameManager.instance.UpgradeStat(statType);
            coinText.text = GameManager.instance.playerCoins.ToString();
            Debug.Log(statType + " upgraded!");

            // Update button states after upgrade
            UpdateButtonStates();
        }
        else
        {
            Debug.Log("Not enough coins to upgrade " + statType);
            ShowOutOfCoinsMessage();
        }
    }

    private void ShowOutOfCoinsMessage()
    {
        if (outOfCoinsText != null)
        {
            outOfCoinsText.gameObject.SetActive(true);
            StartCoroutine(HideOutOfCoinsMessageAfterDelay(2f)); // Hide after 2 seconds
        }
        else
        {
            Debug.LogWarning("OutOfCoinsText is not assigned in the UpgradeShop script.");
        }
    }

    private System.Collections.IEnumerator HideOutOfCoinsMessageAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (outOfCoinsText != null)
        {
            outOfCoinsText.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// Updates the interactable state and color of upgrade buttons based on current levels.
    /// </summary>
    public void UpdateButtonStates()
    {
        // Bullet Speed
        if (GameManager.instance.bulletSpeedLevel >= GameManager.instance.MaxUpgradeLevel)
        {
            bulletSpeedButton.interactable = false;
            SetButtonColor(bulletSpeedButton, Color.white);
        }
        else
        {
            bulletSpeedButton.interactable = true;
            SetButtonColor(bulletSpeedButton, Color.green); // Assuming green is the default
        }

        // Bullet Range
        if (GameManager.instance.bulletRangeLevel >= GameManager.instance.MaxUpgradeLevel)
        {
            bulletRangeButton.interactable = false;
            SetButtonColor(bulletRangeButton, Color.white);
        }
        else
        {
            bulletRangeButton.interactable = true;
            SetButtonColor(bulletRangeButton, Color.green);
        }

        // Shrink Resistance
        if (GameManager.instance.shrinkResistanceLevel >= GameManager.instance.MaxUpgradeLevel)
        {
            shrinkResistanceButton.interactable = false;
            SetButtonColor(shrinkResistanceButton, Color.white);
        }
        else
        {
            shrinkResistanceButton.interactable = true;
            SetButtonColor(shrinkResistanceButton, Color.green);
        }

        // Movement Speed
        if (GameManager.instance.movementSpeedLevel >= GameManager.instance.MaxUpgradeLevel)
        {
            movementSpeedButton.interactable = false;
            SetButtonColor(movementSpeedButton, Color.white);
        }
        else
        {
            movementSpeedButton.interactable = true;
            SetButtonColor(movementSpeedButton, Color.green);
        }

        if (GameManager.instance.bulletDamageLevel >= GameManager.instance.MaxUpgradeLevel)
        {
            bulletDamageButton.interactable = false;
            SetButtonColor(bulletDamageButton, Color.white);
        }
        else
        {
            bulletDamageButton.interactable = true;
            SetButtonColor(bulletDamageButton, Color.green);
        }
    }

    /// <summary>
    /// Sets the color of a button.
    /// </summary>
    /// <param name="button">Button to change color.</param>
    /// <param name="color">Desired color.</param>
    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        //cb.normalColor = color;
        //cb.highlightedColor = color;
        //cb.pressedColor = color;
        //cb.selectedColor = color;
        cb.disabledColor = Color.gray; // Optionally set a different color for disabled state
        button.colors = cb;
    }
    //void UpgradeGun(string upgradeType)
    //{
    //    // Define the cost for each gun upgrade
    //    // keeping all costs the same for now
    //    int cost = 0;
    //    switch (upgradeType)
    //    {
    //        case "Speed":
    //            cost = 10;
    //            break;
    //        case "Range":
    //            cost = 10;
    //            break;
    //        case "Damage":
    //            cost = 10;
    //            break;
    //        case "Penetration":
    //            cost = 10;
    //            break;
    //        case "Reload":
    //            cost = 10;
    //            break;
    //    }

    //    if (GameManager.instance.SpendCoins(cost))
    //    {
    //        coinText.text = "Coins: " + GameManager.instance.playerCoins.ToString();
    //        GameManager.instance.UpgradeGun(upgradeType);
    //        Debug.Log(upgradeType + " upgraded!");
    //    }
    //    else
    //    {
    //        Debug.Log("Not enough coins to upgrade " + upgradeType);
    //    }
    //}

    //void UpgradeStat(string statType)
    //{
    //    // Define the cost for each stat upgrade
    //    // keeping all costs the same for now
    //    int cost = 0;
    //    switch (statType)
    //    {
    //        case "ShrinkResistance":
    //            cost = 10;
    //            break;
    //        case "MaxHealth":
    //            cost = 10;
    //            break;
    //        case "MovementSpeed":
    //            cost = 10;
    //            break;
    //        case "BodyDamage":
    //            cost = 10;
    //            break;
    //    }

    //    if (GameManager.instance.SpendCoins(cost))
    //    {
    //        coinText.text = "Coins: " + GameManager.instance.playerCoins.ToString();
    //        GameManager.instance.UpgradeStat(statType);
    //        Debug.Log(statType + " upgraded!");
    //    }
    //    else
    //    {
    //        Debug.Log("Not enough coins to upgrade " + statType);
    //    }
    //}
}
