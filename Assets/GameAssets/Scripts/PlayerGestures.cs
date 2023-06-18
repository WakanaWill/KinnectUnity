using UnityEngine;
using System.Collections;
using System;

public class PlayerGestures : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public UnityEngine.UI.Text GestureInfo;

	private bool swipeLeft;
	private bool swipeRight;
	private bool tPose;
	private bool jump;
	private bool rightHandUp;
	private bool leftHandUp;
	private bool psi;
	private bool wave;
	private bool swipeUp;
	private bool swipeDown;
	private bool squat;



	public bool IsSwipeLeft()
	{
		if (swipeLeft)
		{
			swipeLeft = false;
			return true;
		}

		return false;
	}

	public bool IsSwipeRight()
	{
		if (swipeRight)
		{
			swipeRight = false;
			return true;
		}

		return false;
	}

	public bool IsTpose()
	{
		if (tPose)
		{
			tPose = false;
			return true;
		}

		return false;
	}

	public bool IsJump()
	{
		if (jump)
		{
			jump = false;
			return true;
		}

		return false;
	}

	public bool IsRightHandUp()
	{
		if (rightHandUp)
		{
			rightHandUp = false;
			return true;
		}

		return false;
	}

	public bool IsLeftHandUp()
	{
		if (leftHandUp)
		{
			leftHandUp = false;
			return true;
		}

		return false;
	}

	public bool IsPsi()
	{
		if (psi)
		{
			psi = false;
			return true;
		}

		return false;
	}

	public bool IsSwipeUp()
	{
		if (swipeUp)
		{
			swipeUp = false;
			return true;
		}

		return false;
	}

	public bool IsSwipeDown()
	{
		if (swipeDown)
		{
			swipeDown = false;
			return true;
		}

		return false;
	}

	public bool IsWave()
	{
		if (wave)
		{
			wave = false;
			return true;
		}

		return false;
	}

	public bool IsSquat()
	{
		if (squat)
		{
			squat = false;
			return true;
		}

		return false;
	}


	public void UserDetected(uint userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
		manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);
		manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
		manager.DetectGesture(userId, KinectGestures.Gestures.Psi);
		manager.DetectGesture(userId, KinectGestures.Gestures.Wave);
		manager.DetectGesture(userId, KinectGestures.Gestures.Squat);

		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);


	}

	public void UserLost(uint userId, int userIndex)
	{
		if (GestureInfo != null)
		{
			GestureInfo.text = "Player Lost";
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture,
								  float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		// don't do anything here
	}

	public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
								  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		string sGestureText = gesture + " detected";
		if (GestureInfo != null)
		{
			GestureInfo.text = sGestureText;
		}

		if (gesture == KinectGestures.Gestures.SwipeLeft)
			swipeLeft = true;
		else if (gesture == KinectGestures.Gestures.SwipeRight)
			swipeRight = true;
		else if (gesture == KinectGestures.Gestures.Tpose)
			tPose = true;
		else if (gesture == KinectGestures.Gestures.Jump)
			jump = true;
		else if (gesture == KinectGestures.Gestures.RaiseLeftHand)
			leftHandUp = true;
		else if (gesture == KinectGestures.Gestures.RaiseRightHand)
			rightHandUp = true;
		else if (gesture == KinectGestures.Gestures.Psi)
			psi = true;
		else if (gesture == KinectGestures.Gestures.SwipeUp)
			swipeUp = true;
		else if (gesture == KinectGestures.Gestures.SwipeDown)
			swipeDown = true;
		else if (gesture == KinectGestures.Gestures.Wave)
			wave = true;
		else if (gesture == KinectGestures.Gestures.Squat)
			squat = true;
		
		return true;
	}

	public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
								  KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		// don't do anything here, just reset the gesture state
		return true;
	}

}
