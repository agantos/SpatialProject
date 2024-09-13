using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
	public string[] texts;
	
	int currIndex;
	bool isOnFinalText = false;
	
	TextTypingAnimation text;
	Canvas UIOverlay;

	[Header("Icons")]
	public GameObject NextIcon;
	public GameObject EndIcon;

	[Header("Avatars")]
	public GameObject AvatarLeft;
	public GameObject AvatarMiddle;
	public GameObject AvatarRight;

	[Header("Steps (each three are one step)")]
	public AvatarEnum[] steps;


	private void Start()
	{
		currIndex = 0;
		text = GetComponentInChildren<TextTypingAnimation>();
		UIOverlay = FindAnyObjectByType<Canvas>();
		Invoke("OpenDialogue", 0.5f);
	}

	public void ButtonClick()
	{
		if(!isOnFinalText)
		{
			PlayNext();
		}
		else
		{
			UIOverlay.gameObject.SetActive(false);
		}
	}

	public void OpenDialogue()
	{
		PlayNext();
	}

	void PlayNext()
	{
		if(currIndex < texts.Length)
		{
			if (text.HasTextCompleted())
			{
				SetAvatars();
				text.Play(texts[currIndex]);
				currIndex++;
			}
			else
			{
				text.CompleteText();
			}			
		}
		else
		{
			isOnFinalText = true;
			SpawnExitUI();
		}
	}

	private void SetAvatars()
	{
		AvatarEnum[] step = GetStep(currIndex);

		if(step[0] != AvatarEnum._NULL)
		{
			AvatarLeft.SetActive(true);
			AvatarLeft.GetComponentInChildren<RawImage>().texture = GameManager.Instance.avatars[step[0]];
		}
		else
		{
			AvatarLeft.SetActive(false);
		}

		if (step[1] != AvatarEnum._NULL)
		{
			AvatarMiddle.SetActive(true);
			AvatarMiddle.GetComponentInChildren<RawImage>().texture = GameManager.Instance.avatars[step[1]];
		}
		else
		{
			AvatarMiddle.SetActive(false);
		}

		if (step[2] != AvatarEnum._NULL)
		{
			AvatarRight.SetActive(true);
			AvatarRight.GetComponentInChildren<RawImage>().texture = GameManager.Instance.avatars[step[2]];
		}
		else
		{
			AvatarRight.SetActive(false);
		}
	}

	private void SpawnExitUI()
	{
		NextIcon.SetActive(false);
		EndIcon.SetActive(true);
	}

	private AvatarEnum[] GetStep(int i)
	{
		int firstIndex = i*3;
		AvatarEnum[] step = { steps[firstIndex], steps[firstIndex + 1], steps[firstIndex + 2] };
		return step;
	}
}