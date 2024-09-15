using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
	public string[] texts;	
	int currIndex;
	bool isOnFinalText = false;

	TextTypingAnimation dialogueText;
	[Header("Minimizable Dialogue UI")]
	public GameObject UIOverlay;

	[Header("Icons")]
	public GameObject NextIcon;
	public GameObject EndIcon;

	[Header("Avatars")]
	public GameObject AvatarLeft;
	public GameObject AvatarMiddle;
	public GameObject AvatarRight;

	[Header("Steps (each three are one step)")]
	public AvatarEnum[] steps;

	[Header("Options UI")]
	public GameObject Options;

	[Header("Dialogue Controls")]
	public int stepToSpawnOptions = -1; // -1 means no options in this dialogue
	public int goodOptionSteps;
	public int badOptionSteps;


	private void Start()
	{
		currIndex = 0;
		dialogueText = GetComponentInChildren<TextTypingAnimation>();
		UIOverlay.gameObject.SetActive(false);
	}

	public void ButtonClick()
	{
		if(!isOnFinalText && stepToSpawnOptions != currIndex)
		{
			PlayNext();
		}		
		
		if(isOnFinalText)
		{
			EndDialogue();
		}

		if (stepToSpawnOptions == currIndex)
		{
			SetOptionUIActive(true);
		}
	}

	public void StartDialogue()
	{
		UIOverlay.gameObject.SetActive(true);
		CameraController.Instance.FreezeMovementAndRotation();
		PlayIndex(0);
	}

	public void EndDialogue()
	{
		UIOverlay.gameObject.SetActive(false);
		CameraController.Instance.UnFreezeMovementAndRotation();
	}

	void PlayIndex(int i) {		
		currIndex = i;
		SetAvatars();
		dialogueText.SpawnText(texts[i]);		
	}

	void PlayNext()
	{
		if (dialogueText.HasTextCompleted())
		{
			currIndex++;
			if(currIndex < texts.Length)
			{
				PlayIndex(currIndex);
			}
		}
		else
		{
			dialogueText.CompleteText();
		}

		if (currIndex >= texts.Length - 1)
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

	void SetOptionUIActive(bool active)
	{
		Options.SetActive(active);
		NextIcon.SetActive(!active);
	}

	// Steps are structured as follows:
	// [step, step, ... good_1, ..., good_last, bad_0, ... bad_last, step, ...] 
	public void GoodDialoguePressed()
	{
		if (dialogueText.HasTextCompleted())
		{
			// Remove the bad dialogue steps.
			int firstIndexOfBadDialogue = currIndex + 1 + goodOptionSteps;
			List<AvatarEnum> stepsList = new List<AvatarEnum>(steps);

			int firstStepIndex_Bad = firstIndexOfBadDialogue * 3;

			// Start Removing from the last element to not mess with the list.
			for (int i = badOptionSteps; i > 0; i--)
			{
				int lastStepIndex_Bad = firstStepIndex_Bad + i - 1;
				stepsList.RemoveAt(lastStepIndex_Bad + 2);
				stepsList.RemoveAt(lastStepIndex_Bad + 1);
				stepsList.RemoveAt(lastStepIndex_Bad + 0);
			}

			steps = stepsList.ToArray();

			// We also remove the bad dialogue from texts
			List<string> textsList = new List<string>(texts);

			// Start Removing from the last element to not mess with the list.
			for (int i = badOptionSteps; i > 0; i--)
			{
				int lastBadIndex = firstIndexOfBadDialogue + (i - 1);
				textsList.RemoveAt(lastBadIndex);
			}

			texts = textsList.ToArray();

			PlayNext();
			SetOptionUIActive(false);
		}		
	}

	public void BadDialoguePressed()
	{
		if (dialogueText.HasTextCompleted())
		{
			// Remove the good dialogue steps.
			int firstIndexOfGoodDialogue = currIndex + 1;
			List<AvatarEnum> stepsList = new List<AvatarEnum>(steps);

			int firstStepIndex_Good = firstIndexOfGoodDialogue * 3;

			// Start Removing from the last element to not mess with the list.
			for (int i = goodOptionSteps; i > 0; i--)
			{
				int lastStepIndex_Good = firstStepIndex_Good + i - 1;
				stepsList.RemoveAt(lastStepIndex_Good + 2);
				stepsList.RemoveAt(lastStepIndex_Good + 1);
				stepsList.RemoveAt(lastStepIndex_Good + 0);
			}

			steps = stepsList.ToArray();

			// We also remove the good dialogue from texts
			List<string> textsList = new List<string>(texts);

			// Start Removing from the last element to not mess with the list.
			for (int i = goodOptionSteps; i > 0; i--)
			{
				int lastGoodIndex = firstIndexOfGoodDialogue + (i - 1);
				textsList.RemoveAt(lastGoodIndex);
			}

			texts = textsList.ToArray();
			
			PlayNext();
			SetOptionUIActive(false);
			UIManager.Instance.LoseLife();
		}
	}
}