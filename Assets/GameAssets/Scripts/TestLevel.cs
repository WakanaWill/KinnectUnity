using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestLevel : MonoBehaviour
{
	public bool slideChangeWithGestures = true;
	public bool slideChangeWithKeys = true;

	public bool autoChangeAlfterDelay = false;

	[SerializeField] GameObject taskBar;
	public Text nextMoveText;
	public int movesToMake = 5;
	private int movesMade;

	public Text progress;

	public List<string> movesTextList;
	List<string> currentMovesToMakeTextList;
	string currentMoveToBeMade;

	bool isGameEnded = false;

	[SerializeField] GameObject timer;
	[SerializeField] GameObject timerBar;
	public float timeLeft;

	[SerializeField] GameObject progressCounter;
	public GameObject checkBoxPrefab;
	private PlayerGestures gestureListener;

	private int lvl = 0;

	[SerializeField] Animator animator;

	public GameObject gameOver;
	int score = 0;


	[SerializeField] Text highScoreText;

	[SerializeField] GameObject counterDown;


	[SerializeField] AudioSource counterDownSFX;
	[SerializeField] AudioSource counterDownLastSFX;
	[SerializeField] AudioSource correctMoveSFX;
	[SerializeField] AudioSource finishedRoundSFX;
	[SerializeField] AudioSource gameOverSFX;

	bool canReadInput = false;


	void Start()
	{
		Debug.Log(PlayerPrefs.GetInt("score"));
		score = 0;

		// hide mouse cursor
		Cursor.visible = false;
		timerBar.SetActive(false);
		timerBar.SetActive(false);
		progressCounter.SetActive(false);
		taskBar.SetActive(false);



		currentMovesToMakeTextList = new List<string>();

		// get the gestures listener
		gestureListener = Camera.main.GetComponent<PlayerGestures>();

		for (int i = 0; i < movesToMake; i++)
		{
			int randomIndex = Random.Range(0, movesTextList.Count);
			currentMovesToMakeTextList.Add(movesTextList[randomIndex]);
		}

		movesMade = 0;

		currentMoveToBeMade = currentMovesToMakeTextList[movesMade];
		StartCoroutine(CounterDown());
	}

	void Update()
	{

		// dont run Update() if there is no user
		KinectManager kinectManager = KinectManager.Instance;
		if (autoChangeAlfterDelay && (!kinectManager || !kinectManager.IsInitialized() || !kinectManager.IsUserDetected()))
			return;

		if (slideChangeWithKeys)
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "SwipeLeft" && canReadInput)
		{
			if (gestureListener.IsSwipeLeft())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "SwipeRight" && canReadInput)
		{
			if (gestureListener.IsSwipeRight())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "Tpose" && canReadInput)
		{
			if (gestureListener.IsTpose())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "Jump" && canReadInput)
		{
			if (gestureListener.IsJump())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "RightHandUp" && canReadInput)
		{
			if (gestureListener.IsRightHandUp())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "LeftHandUp" && canReadInput)
		{
			if (gestureListener.IsLeftHandUp())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "Psi" && canReadInput)
		{
			if (gestureListener.IsPsi())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "SwipeUp" && canReadInput)
		{
			if (gestureListener.IsSwipeUp())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "SwipeDown" && canReadInput)
		{
			if (gestureListener.IsSwipeDown())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "Squat" && canReadInput)
		{
			if (gestureListener.IsSquat())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "Wave" && canReadInput)
		{
			if (gestureListener.IsWave())
				NextMove();
		}


		if (movesMade >= movesToMake)
		{
			movesMade = 0;
			lvl++;
			StartCoroutine(NextGame());
		}

		progress.text = $"{movesMade}/{movesToMake}";

	}


	private void NextMove()
	{
		if (movesMade >= movesToMake || isGameEnded) return;
		if(movesToMake - movesMade != 1)
			correctMoveSFX.Play();
		StartCoroutine(WaitAfterReadingInput());
		score++;
		movesMade++;
		nextMoveText.text = movesMade.ToString();
		currentMoveToBeMade = currentMovesToMakeTextList[movesMade];

	}

	public void GameLost()
	{
		gameOverSFX.Play();
		if (PlayerPrefs.GetInt("score") < score)
		{
			PlayerPrefs.SetInt("score", score);
			highScoreText.text = $"New High Score: {PlayerPrefs.GetInt("score", 0)}!";
		}
		else
			highScoreText.text = $"Your Score: {score}";

		taskBar.SetActive(true);
		nextMoveText.text = "Game Over";
		isGameEnded = true;
		currentMoveToBeMade = null;
		timerBar.SetActive(false);
		progressCounter.SetActive(false);
		gameOver.SetActive(true);
	}




	IEnumerator ShowAllMovesToMake()
	{
		int movesToShow = 0;
		canReadInput = false;
		while (movesToShow < movesToMake)
		{
			animator.SetTrigger(currentMovesToMakeTextList[movesToShow]);
			nextMoveText.text = currentMovesToMakeTextList[movesToShow];
			Debug.Log(currentMovesToMakeTextList[movesToShow]);

			movesToShow++;
			yield return new WaitForSeconds(2f);
		}
		nextMoveText.text = movesMade.ToString();
		taskBar.SetActive(false);
		timerBar.SetActive(true);
		progressCounter.SetActive(true);
		timer.GetComponent<Timer>().SetTimer(60f);
		canReadInput = true;
	}

	IEnumerator NextGame()
	{
		finishedRoundSFX.Play();
		timerBar.SetActive(false);
		progressCounter.SetActive(false);
		taskBar.SetActive(true);
		nextMoveText.text = "You did it!";
		animator.SetTrigger("Clap");
		yield return new WaitForSeconds(3f);

		movesToMake++;

		int randomIndex = Random.Range(0, movesTextList.Count);
		currentMovesToMakeTextList.Add(movesTextList[randomIndex]);


		movesMade = 0;

		currentMoveToBeMade = currentMovesToMakeTextList[movesMade];
		StartCoroutine(ShowAllMovesToMake());
	}

	IEnumerator CounterDown()
	{

		int counter = 3;
		Text counterDownText = counterDown.GetComponentInChildren<Text>();

		while (counter > 0)
		{
			counterDownSFX.Play();
			counterDownText.text = counter.ToString();
			counter--;
			yield return new WaitForSeconds(1f);
		}
		counterDownLastSFX.Play();
		taskBar.SetActive(true);
		counterDown.SetActive(false);
		StartCoroutine(ShowAllMovesToMake());
	}


	IEnumerator WaitAfterReadingInput()
	{
		canReadInput = false;
		yield return new WaitForSeconds(1f);
		canReadInput = true;
	}






}
