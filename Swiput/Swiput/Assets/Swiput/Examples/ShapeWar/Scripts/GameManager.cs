using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public enum WhoWon
	{
		PlayerWon,
		EnemyWon,
		NobodyYet
	}

	public enum PowerUps
	{
		Multiply,
		Invincible,
		ExtraHealth
	}

	public static GameManager Instance;

	public Camera Cam;

	public Image[] ControlVisualizers;

	[HideInInspector] 
	public bool IsGameOver = false, FirstTime = false, IsStartGame = false,
	IsMultiplier = false, IsInvincible = false, IsExtraHealth = false;


	[HideInInspector]
	public float TimeElasped;

	[HideInInspector]
	public int PlayerDamageAmount = 0, EnemyDamageAmount = 0;

	public Transform[] PowerUpSpawnPoints;

	public Transform LeftEndWayPoint, RightEndWayPoint, 
	LeftPowerUpPoint, RightPowerUpPoint;

	public WhoWon HasAnybodyWon = WhoWon.NobodyYet;

	public GameObject StartPanel;
	public GameObject LosePanel;
	public GameObject WinPanel;
	public GameObject HudPanel;

	public GameObject MultiplierPanel;
	public GameObject InvinciblePanel;

	public GameObject MultiplierPowerUp;
	public GameObject InvinciblePowerUp;
	public GameObject ExtraHealthPowerUp;

	public Image EnemyEnergy, PlayerEnergy, MultiplierRadial, InvincibleRadial;

	public Text StartCounterText;

	public Text WinScore, WinHighScore, LoseHighScore, CurrentScore;

	public float StartDelay = 5, PowerUpDelay = 20f,
	MultiPlierWaitTime = 30f, InvincibleWaitTime = 30f, ExtraHealthWaitTime = 20f;

	public int SetPlayerDamageAmount = 10, SetEnemyDamageAmount = 1, ExtraHealth = 50;
	public int EnemyMaxHealth = 100, PlayerMaxHealth = 100;

	private float startCounter = 0f, multiplierCountDown = 0f, invincibleCountDown = 0f, 
	MultiplierElapsed = 0f, InvincibleElapsed = 0f, ExtraHealthElapsed = 0f;

	const string highScoreKey = "CaptainJeoy", ColourKey = "SydneyPark";

	float HalfScreenWidth;

	int LastHighScore, ColourCode = 0;

	public Toggle tog;

	void Awake()
	{
		Instance = this;

		if (PlayerPrefs.HasKey(highScoreKey)) 
		{
			FirstTime = false;

			LastHighScore = PlayerPrefs.GetInt (highScoreKey);
		} 
		else 
		{
			FirstTime = true;

			PlayerPrefs.SetInt (highScoreKey, 1000000);

			LastHighScore = PlayerPrefs.GetInt (highScoreKey);
		}

		if (PlayerPrefs.HasKey(ColourKey)) 
		{
			if (PlayerPrefs.GetInt (ColourKey) == 0) 
			{
				tog.isOn = true;
				foreach (Image item in ControlVisualizers) 
				{
					item.enabled = true;
				}
			} 
			else 
			{
				tog.isOn = false;
				foreach (Image item in ControlVisualizers) 
				{
					item.enabled = false;
				}
			}
		}
	}

	void Start()
	{
		HalfScreenWidth = Cam.aspect * Cam.orthographicSize;
		startCounter = StartDelay;
		IsGameOver = false;

		PlayerDamageAmount = SetPlayerDamageAmount;
		EnemyDamageAmount = SetEnemyDamageAmount;

		MultiplierElapsed = MultiPlierWaitTime + StartDelay;
		InvincibleElapsed = InvincibleWaitTime + StartDelay;
		ExtraHealthElapsed = ExtraHealthWaitTime + StartDelay;

		multiplierCountDown = invincibleCountDown = PowerUpDelay;

		//Auto Adjust Enemy WayPoint
		float autoDistance = (2.25f - 2.7f);

		Vector3 tempRightPos = RightEndWayPoint.position;
		tempRightPos.x = HalfScreenWidth - autoDistance;
		RightEndWayPoint.position = tempRightPos;

		Vector3 tempLeftPos = LeftEndWayPoint.position;
		tempLeftPos.x = -HalfScreenWidth + autoDistance;
		LeftEndWayPoint.position = tempLeftPos;

		//Auto Adjust PowerUp SpawnPoint
		float autoDistancePowerUp = (2.25f - 1.3f);

		Vector3 tempRightPosPower = RightPowerUpPoint.position;
		tempRightPosPower.x = HalfScreenWidth - autoDistancePowerUp;
		RightPowerUpPoint.position = tempRightPosPower;

		Vector3 tempLeftPosPower = LeftPowerUpPoint.position;
		tempLeftPosPower.x = -HalfScreenWidth + autoDistancePowerUp;
		LeftPowerUpPoint.position = tempLeftPosPower;
	}

	void Update()
	{
		PreStart ();
	
		CheckWhetherYouLost ();
		CheckWhetherYouWin ();

		UpdateEnergyBar ();

		SpawnMultiplier ();
		SpawnInvincible ();
		SpawnExtraHealth ();

		if (IsStartGame && !IsGameOver) 
			CurrentScore.text = Mathf.RoundToInt (Time.time - TimeElasped).ToString ();

		if (IsMultiplier)
			PowerUpMultiplier ();

		if (IsInvincible)
			PowerUpInvincible ();

		if (IsExtraHealth)
			PowerUpExtraHealth ();
	}

	void PreStart()
	{
		if (IsStartGame)
			return;

		StartCounterText.text = Mathf.RoundToInt(startCounter).ToString ();
		startCounter -= Time.deltaTime;

		if (startCounter <= 0f) 
		{
			IsStartGame = true;
			IsGameOver = false;
			HasAnybodyWon = WhoWon.NobodyYet;

			StartPanel.SetActive (false);

			TimeElasped = Time.time;

			HudPanel.SetActive (true);
		}
	}

	void UpdateEnergyBar()
	{
		EnemyEnergy.fillAmount = float.Parse(Enemy.Instance.EnemyHealth.ToString()) / float.Parse(EnemyMaxHealth.ToString());
		PlayerEnergy.fillAmount = float.Parse(Player.Instance.PlayerHealth.ToString()) / float.Parse(PlayerMaxHealth.ToString());
	}

	void CheckWhetherYouLost()
	{
		if (HasAnybodyWon == WhoWon.EnemyWon)
		{
			HudPanel.SetActive (false);

			WinPanel.SetActive (false);
			LosePanel.SetActive (true);

			if (LastHighScore == 1000000) 
			{
				LoseHighScore.gameObject.SetActive (false);
			} 
			else 
			{
				LoseHighScore.gameObject.SetActive (true);
				LoseHighScore.text = "BEST TIME : " + LastHighScore.ToString ();
			}
				
			HasAnybodyWon = WhoWon.NobodyYet;
		}
	}

	void CheckWhetherYouWin()
	{
		if (HasAnybodyWon == WhoWon.PlayerWon)
		{
			HudPanel.SetActive (false);

			LosePanel.SetActive (false);
			WinPanel.SetActive (true);

			WinScore.text = Mathf.RoundToInt (Time.time - TimeElasped).ToString ();

			if (LastHighScore < Mathf.RoundToInt (Time.time - TimeElasped)) 
			{
				WinHighScore.text = "BEST TIME : " + LastHighScore.ToString ();
			} 
			else
			{
				WinHighScore.text = "BEST TIME : " + Mathf.RoundToInt (Time.time - TimeElasped).ToString ();

				PlayerPrefs.SetInt (highScoreKey, Mathf.RoundToInt (Time.time - TimeElasped));
			}

			HasAnybodyWon = WhoWon.NobodyYet;
		}
	}

	void SpawnMultiplier()
	{
		if ((Time.time - TimeElasped) > MultiplierElapsed && IsStartGame && !IsGameOver) 
		{
			MultiplierElapsed = Time.time + MultiPlierWaitTime + Random.Range(-10f, 5f);

			Instantiate (MultiplierPowerUp, PowerUpSpawnPoints [Random.Range (0, PowerUpSpawnPoints.Length)].position, Quaternion.identity);
		}
	}

	void SpawnInvincible()
	{
		if ((Time.time - TimeElasped) > InvincibleElapsed && IsStartGame && !IsGameOver) 
		{
			InvincibleElapsed = Time.time + InvincibleWaitTime + Random.Range(-10f, 5f);

			Instantiate (InvinciblePowerUp, PowerUpSpawnPoints [Random.Range (0, PowerUpSpawnPoints.Length)].position, Quaternion.identity);
		}
	}

	void SpawnExtraHealth()
	{
		float currentHealthPercent = ((float)Player.Instance.PlayerHealth / (float)PlayerMaxHealth) * 100f;

		if ((Time.time - TimeElasped) > ExtraHealthElapsed && IsStartGame && !IsGameOver && currentHealthPercent < 80f) 
		{
			ExtraHealthElapsed = Time.time + ExtraHealthWaitTime + Random.Range(-5f, 10f);

			Instantiate (ExtraHealthPowerUp, PowerUpSpawnPoints [Random.Range (0, PowerUpSpawnPoints.Length)].position, Quaternion.identity);
		}
	}

	void PowerUpMultiplier ()
	{
		if (multiplierCountDown <= 0f || IsGameOver) 
		{
			IsMultiplier = false;
			MultiplierPanel.SetActive (false);
			multiplierCountDown = PowerUpDelay;
			EnemyDamageAmount = SetEnemyDamageAmount;

			Player.Instance.ChooseCurrentBullet = Player.Bullets.NormalSquare;

			return;
		}

		MultiplierPanel.SetActive (true);

		Player.Instance.ChooseCurrentBullet = Player.Bullets.DiamondMultiplier;

		EnemyDamageAmount = 3;

		multiplierCountDown -= Time.deltaTime;
		float countDown = multiplierCountDown / PowerUpDelay;

		MultiplierRadial.fillAmount = countDown;
	}

	void PowerUpInvincible ()
	{
		if (invincibleCountDown <= 0f || IsGameOver) 
		{
			IsInvincible = false;
			InvinciblePanel.SetActive (false);
			invincibleCountDown = PowerUpDelay;

			Player.Instance.PlayerSprite.color = Color.white;
		
			return;
		}

		InvinciblePanel.SetActive (true);

		Player.Instance.PlayerSprite.color = Color.blue;

		invincibleCountDown -= Time.deltaTime;
		float countDown = invincibleCountDown / PowerUpDelay;

		InvincibleRadial.fillAmount = countDown;
	}

	void PowerUpExtraHealth ()
	{
		if (IsGameOver)
			return;
		
		if (Player.Instance.PlayerHealth + ExtraHealth >= PlayerMaxHealth) 
			Player.Instance.PlayerHealth = PlayerMaxHealth;
		else
			Player.Instance.PlayerHealth += ExtraHealth;

		IsExtraHealth = false;
	}

	public void ToggleControlsVisualizers()
	{
		foreach (Image item in ControlVisualizers) 
		{
			item.enabled = !item.enabled;

			if (item.enabled)
				ColourCode = 0;
			else
				ColourCode = 1;
		}

		PlayerPrefs.SetInt (ColourKey, ColourCode);
	}

	public void RestartLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
