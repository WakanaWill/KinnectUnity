using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestLevel : MonoBehaviour
{
	public bool slideChangeWithGestures = true;
	public bool slideChangeWithKeys = true;

	public bool autoChangeAlfterDelay = false;

	public Text nextMoveText;
	public int movesToMake = 5;
	private int movesMade;

	public List<string> movesTextList;
	string currentMoveToBeMade;






	public Transform progressBar;
	public GameObject checkBoxPrefab;

	private PlayerGestures gestureListener;



	void Start()
	{
		// hide mouse cursor
		Cursor.visible = false;


		
		// get the gestures listener
		gestureListener = Camera.main.GetComponent<PlayerGestures>();

		movesMade = 0;
		int randomIndex = Random.Range(0, movesTextList.Count);
		currentMoveToBeMade = movesTextList[randomIndex];
		nextMoveText.text = movesTextList[randomIndex];
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


		if(movesMade >= movesToMake)
        {
			nextMoveText.text = "Congratulations";
			currentMoveToBeMade = null;
		}

	}


	private void NextMove()
    {
		movesMade++;
		Instantiate(checkBoxPrefab, progressBar);
		int randomIndex = Random.Range(0, movesTextList.Count);
		nextMoveText.text = movesTextList[randomIndex];
		currentMoveToBeMade = movesTextList[randomIndex];

	}

	


}
