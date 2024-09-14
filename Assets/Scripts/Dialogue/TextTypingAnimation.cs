using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTypingAnimation : MonoBehaviour
{
	string text_string;
	TextMeshProUGUI text_TMP;

	private Coroutine typingCoroutine;

	bool isCoroutineRunning;

	private void Start()
	{
		text_TMP = GetComponent<TextMeshProUGUI>();
	}

	IEnumerator StartSpawnRoutine(string text)
	{
		text_string = text;

		int characters = text_string.Length;
		int i = 0;

		while (i != characters)
		{
			text_TMP.text = text_string.Substring(0,i) + "|";
			i++;

			yield return new WaitForSeconds(0.03f);
		}

		isCoroutineRunning = false;
		text_TMP.text = text_string;

		AudioManager.Instance.StopTypingSoundEffect();
	}

	public void SpawnText(string text)
	{
		typingCoroutine = StartCoroutine(StartSpawnRoutine(text));
		isCoroutineRunning = true;

		AudioManager.Instance.PlayTypingSoundEffect();
	}

	public void CompleteText()
	{
		StopCoroutine(typingCoroutine);
		text_TMP.text = text_string;
		isCoroutineRunning = false;

		AudioManager.Instance.StopTypingSoundEffect();
	}

	public bool HasTextCompleted()
	{
		return !isCoroutineRunning;
	}
}
