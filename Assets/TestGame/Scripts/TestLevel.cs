using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestLevel : MonoBehaviour
{
	public bool slideChangeWithGestures = true;
	public bool slideChangeWithKeys = true;
	public float spinSpeed = 5;

	public bool autoChangeAlfterDelay = false;
	public float slideChangeAfterDelay = 5;

	public List<Texture> slideTextures;
	public List<GameObject> horizontalSides;

	public List<GameObject> moves;
	private int currentMoveId;


	private PlayerGestures gestureListener;



	void Start()
	{
		// hide mouse cursor
		Cursor.visible = false;


		
		// get the gestures listener
		gestureListener = Camera.main.GetComponent<PlayerGestures>();

		currentMoveId = 0;
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

		if(slideChangeWithGestures && gestureListener && currentMoveId == 0)
				{
			if (gestureListener.IsMagdaPose())
				NextMove();
		}
		if (slideChangeWithGestures && gestureListener && currentMoveId == 1)
		{
			if (gestureListener.IsSwipeLeft())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveId == 2)
		{
			if (gestureListener.IsSwipeRight())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveId == 3)
		{
			if (gestureListener.IsTpose())
				NextMove();
		}

		if (slideChangeWithGestures && gestureListener && currentMoveId == 4)
		{
			if (gestureListener.IsJump())
				NextMove();

		}

	}


	private void NextMove()
    {
		moves[currentMoveId].SetActive(false);
		currentMoveId++;
		moves[currentMoveId].SetActive(true);

		if(currentMoveId == 3)
        {
			Debug.Log("You Win!");
        }
	}

	


}
