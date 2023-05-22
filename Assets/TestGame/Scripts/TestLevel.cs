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
    public float timeLeft;


    public Transform progressBar;
	public GameObject checkBoxPrefab;
	private List<GameObject> checkBoxPrefabsList;
	private PlayerGestures gestureListener;

	private int lvl = 0;

	[SerializeField] Animator animator;




		void Start()
	{


		// hide mouse cursor
		Cursor.visible = false;
		timer.SetActive(false);

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
		StartCoroutine(ShowAllMovesToMake());
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

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "SwipeLeft")
		{
			if (gestureListener.IsSwipeLeft())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "SwipeRight")
		{
			if (gestureListener.IsSwipeRight())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "Tpose")
		{
			if (gestureListener.IsTpose())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveToBeMade == "Jump")
		{
			if (gestureListener.IsJump())
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
		movesMade++;
		nextMoveText.text = movesMade.ToString();
		currentMoveToBeMade = currentMovesToMakeTextList[movesMade];

	}

	public void GameLost()
	{
        taskBar.SetActive(true);
        nextMoveText.text = "Game Lost";
		isGameEnded = true;
		currentMoveToBeMade = null;
		timer.SetActive(false);
	}

	


	IEnumerator ShowAllMovesToMake()
	{
		int movesToShow = 0;

		while (movesToShow < movesToMake)
		{
			animator.SetTrigger(currentMovesToMakeTextList[movesToShow]);
			nextMoveText.text = currentMovesToMakeTextList[movesToShow];

			movesToShow++;
			yield return new WaitForSeconds(2f);
		}
		nextMoveText.text = movesMade.ToString();
		taskBar.SetActive(false);
		timer.SetActive(true);
		timer.GetComponent<Timer>().SetTimer(timeLeft - (lvl * 5.0f));
	}

	IEnumerator NextGame()
	{
		timer.SetActive(false);
        taskBar.SetActive(true);
        nextMoveText.text = "Congratulations";
		checkBoxPrefabsList = new List<GameObject>();
		yield return new WaitForSeconds(5f);
		

		currentMovesToMakeTextList = new List<string>();

		for (int i = 0; i < movesToMake; i++)
		{
			int randomIndex = Random.Range(0, movesTextList.Count);
			currentMovesToMakeTextList.Add(movesTextList[randomIndex]);
		}

		movesMade = 0;

		currentMoveToBeMade = currentMovesToMakeTextList[movesMade];
		StartCoroutine(ShowAllMovesToMake());
	}





}
