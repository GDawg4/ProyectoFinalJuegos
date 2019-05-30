using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour {
    [SerializeField]
    private StatusIndicator statusIndicator;

	public float fallBoundary = -20f;

    private void Start()
    {
        PlayerStats.instance.curHP = PlayerStats.instance.maxHP;

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(PlayerStats.instance.curHP, PlayerStats.instance.maxHP);
        } 

        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        InvokeRepeating("RegenHealth",1f/PlayerStats.instance.RegenRate, 1f/PlayerStats.instance.RegenRate);
    }

    void Update()
	{
		if(transform.position.y <= fallBoundary)
			DamagePlayer(10000);

        statusIndicator.SetHealth(PlayerStats.instance.curHP, PlayerStats.instance.maxHP);
    }

	public void DamagePlayer(int dmg)
	{
		PlayerStats.instance.curHP -= dmg;
		if(PlayerStats.instance.curHP <= 0)
		{
			GameMaster.KillPlayer(this);
		}
	}

    void OnUpgradeMenuToggle (bool active)
    {
        GetComponent<Platformer2DUserControl>().enabled = !active;
        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.enabled = !active;
        }
    }

    private void OnDestroy()
    {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }

    void RegenHealth()
    {
        PlayerStats.instance.curHP += 1;
        statusIndicator.SetHealth(PlayerStats.instance.curHP, PlayerStats.instance.maxHP);
    }
}
