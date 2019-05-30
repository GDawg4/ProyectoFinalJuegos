using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
    [SerializeField]
    private int maxLives = 3;

    private static int playerLives = 3;
    public static int PlayerLives
    {
        get { return playerLives; }
    }

    public static int Money;
    [SerializeField]
    private int startingMoney;

    void Awake()
	{
		if (gm == null)
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
	}

	public Transform player;
	public Transform spawnPoint;
	public float spawnDelay = 2f;
	public Transform spawnPrefab;
    public CameraShake cameraShake;

    public string respawnCountdownSound = "RespawnCountdown";
    public string spawnSound = "Spawn";

    [SerializeField]
    private GameObject gameoverUI;

    [SerializeField]
    private GameObject upgradeMenu;

    [SerializeField]
    private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    private AudioManager audioManager;

	public IEnumerator RespawnPlayer()
	{
        audioManager.PlaySound(respawnCountdownSound);
		yield return new WaitForSeconds(spawnDelay);

        audioManager.PlaySound(spawnSound);
		Instantiate(player, spawnPoint.position, spawnPoint.rotation);
		Transform clone = (Transform)Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);

		Destroy(clone.gameObject, 3f);
	}

    private void Start()
    {
        playerLives = maxLives;
        audioManager = AudioManager.instance;

        Money = startingMoney;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

    public void EndGame()
    {
        audioManager.PlaySound("GameOver");
        gameoverUI.SetActive(true);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        playerLives--;
        
        if(playerLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer());
        }
		
	}

    public static void KillEnemy(Enemy enemy)
    {
        gm._killEnemy(enemy);
    }

    public void _killEnemy(Enemy _enemy)
    {

        audioManager.PlaySound(_enemy.deathSoundName);

        Money += _enemy.moneyDrop;
        audioManager.PlaySound("Money");

        Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone.gameObject, 2f);
        cameraShake.Shake(_enemy.shakeAmount,_enemy.shakeLenght);
        Destroy(_enemy.gameObject);
    }
}
