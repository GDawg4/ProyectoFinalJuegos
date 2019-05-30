using UnityEngine.UI;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text speedText;

    private PlayerStats stats;

    [SerializeField]
    private float healthMultiplier = 1.2f;

    [SerializeField]
    private float speedMultiplier = 1.2f;

    [SerializeField]
    private int healthUpgradeCost = 25;

    [SerializeField]
    private int speedUpgradeCost = 30;

    private void Start()
    {
        stats = PlayerStats.instance;
    }

    private void OnEnable()
    {
        stats = PlayerStats.instance;
        UpdateValues();
    }

    private void UpdateValues()
    {
        healthText.text = "HEALTH: " + stats.maxHP.ToString();
        speedText.text ="SPEED: " +  stats.movementSpeed.ToString();
    }

    public void UpgradeHealth()
    {
        if (GameMaster.Money < healthUpgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        stats.maxHP = (int)(stats.curHP * healthMultiplier);
        GameMaster.Money -= healthUpgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }

    public void UpgradeSpeed()
    {
        if (GameMaster.Money < speedUpgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        stats.movementSpeed = (int)(stats.movementSpeed * speedMultiplier);
        GameMaster.Money -= speedUpgradeCost;
        if (GameMaster.Money < healthUpgradeCost)
        {
            AudioManager.instance.PlaySound("Money");
            return;
        }
        UpdateValues();
    }

}

