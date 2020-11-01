using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
////using UnityEditor.PackageManager;

public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	public static bool GameIsPaused = false;
	// public variables
	public int score=0;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;
	public AudioSource laserAudioSource;

	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

	public GameObject mainLevelButtons;
	public string mainLevelToLoad;

	public GameObject resumeButtons;
	public GameObject quitButtons;

	public GameObject spawner;
	public GameObject spawnerSuperBonus;
	public GameObject spawnerPumpkin;

	public GameObject[] LevelArray;
	public GameObject[] DifficultyMode;

	public GameObject gamenamebutton;
	public GameObject levelselbutton;
	public GameObject infobutton;
	public GameObject backbutton;

	private float currentTime;
	private bool pagb = false, nlvb = false;
	bool resettimestarted = false;

	// setup the game
	void Start () {
		GameIsPaused = false;
		// set the current time to the startTime specified
		if (playAgainLevelToLoad == "Level1")
		{
			beatLevelScore = GlobalControl.Instance.level1Beat;
			startTime = GlobalControl.Instance.level1Time;
		}

		if (playAgainLevelToLoad == "Level2")
		{
			startTime = GlobalControl.Instance.level2Time;
		}
		currentTime = startTime;
		Time.timeScale = 1.0f;
		//Debug.Log($"Timescale - {Time.timeScale}");


		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager>();

		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		// inactivate the nextLevelButtons gameObject, if it is set
		if (nextLevelButtons)
			nextLevelButtons.SetActive (false);

		if (musicAudioSource)
			musicAudioSource.pitch = 1f;

		if (laserAudioSource)
			laserAudioSource.pitch = 1f;

		// inactivate the beginAgainButtons gameObject, if it is set
		//if (mainLevelButtons)
		//	mainLevelButtons.SetActive (false);
		
	}

	// this is the main game event loop
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) && playAgainLevelToLoad != "LevelBegin")
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
		if (!gameIsOver) {
			if (canBeatLevel && (score >= beatLevelScore)) {  // check to see if beat game
				BeatLevel ();
			} else if (currentTime < 0) { // check to see if timer has run out
				EndGame ();
			} else if (!GameIsPaused) { // game playing state, so update the timer
				currentTime -= Time.deltaTime;
				mainTimerDisplay.text = currentTime.ToString ("0.00");				
			}
		}
	}

    public void Resume()
    {
		//Time.timeScale = 1.0f;
		if (nextLevelButtons)
		{
			if (nlvb)
			{
				nextLevelButtons.SetActive(true);
				nlvb = true;
			}
		}
		if (pagb)
        {
			playAgainButtons.SetActive(true);
			pagb = false;
        }
		mainLevelButtons.SetActive(false);
		resumeButtons.SetActive(false);
		quitButtons.SetActive(false);
		spawner.SetActive(true);
		if(spawnerSuperBonus)
			spawnerSuperBonus.SetActive(true);
		if (spawnerPumpkin)
			spawnerPumpkin.SetActive(true);
		GameIsPaused = false;

	}

	public void Pause()
    {
		//Time.timeScale = 0.0f;
		if (nextLevelButtons)
		{
			if (nextLevelButtons.activeSelf)
			{
				nlvb = true;
				nextLevelButtons.SetActive(false);
			}
		}
		if (playAgainButtons.activeSelf)
		{
			pagb = true;
			playAgainButtons.SetActive(false);
		}
		mainLevelButtons.SetActive(true);
		resumeButtons.SetActive(true);
		quitButtons.SetActive(true);
		spawner.SetActive(false);
		if (spawnerSuperBonus)
			spawnerSuperBonus.SetActive(false);
		if (spawnerPumpkin)
			spawnerPumpkin.SetActive(false);
		GameIsPaused = true;
	}

    void EndGame() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "GAME OVER";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
	
		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);
		
		// activate the beginAgainButtons gameObject, if it is set
		//if (mainLevelButtons)
		//	mainLevelButtons.SetActive (true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "LEVEL COMPLETE";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		// activate the nextLevelButtons gameObject, if it is set 
		if (nextLevelButtons)
			nextLevelButtons.SetActive (true);
		
		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount, float timeAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();
		
		// increase the time by the timeAmount
		currentTime += timeAmount;
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
        SceneManager.LoadScene(playAgainLevelToLoad);
	}

	public void LevelLoad(string level)
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		SceneManager.LoadScene(level);
	}

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
		// we are just loading the specified next level (scene)
        SceneManager.LoadScene(nextLevelToLoad);
	}

	public void BeginLevel()
	{
		SceneManager.LoadScene(mainLevelToLoad);
	}
	
	public void QuitGame()
    {
		Application.Quit();
    }


	public void LevelSelect()
    {
		foreach(var level in LevelArray)
        {
			level.SetActive(true);
        }
		backbutton.SetActive(true);
		gamenamebutton.SetActive(false);
		infobutton.SetActive(false);
		levelselbutton.SetActive(false);
	}

	public void DiffSelect()
	{
		foreach (var diff in DifficultyMode)
		{
			diff.SetActive(true);
		}
		backbutton.SetActive(true);
		gamenamebutton.SetActive(false);
		infobutton.SetActive(false);
		levelselbutton.SetActive(false);
	}

	public void GoBack()
    {
		foreach (var level in LevelArray)
		{
			level.SetActive(false);
		}
		foreach(var diff in DifficultyMode)
        {
			diff.SetActive(false);
        }
		gamenamebutton.SetActive(true);
		levelselbutton.SetActive(true);
		infobutton.SetActive(true);
		backbutton.SetActive(false);
	}

	public void DilateTime(float timescale)
    {
		if (!resettimestarted)
		{
			//Debug.Log("Entered dilate time");
			Time.timeScale = timescale;
			if (musicAudioSource)
				musicAudioSource.pitch = timescale;
			if (laserAudioSource)
				laserAudioSource.pitch = timescale;
			StartCoroutine(ResetTime());
		}
		
    }

	IEnumerator ResetTime()
    {
		resettimestarted = true;
		//Debug.Log("entered reset time");
		yield return new WaitForSecondsRealtime(5f);
		//Debug.Log("waited 10 seconds");
		Time.timeScale = 1f;
		if (musicAudioSource)
			musicAudioSource.pitch = 1f;
		if (laserAudioSource)
			laserAudioSource.pitch = 1f;
		resettimestarted = false;
		
	}
}
