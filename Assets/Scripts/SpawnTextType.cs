using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnTextType : MonoBehaviour
{
	string text_string;
	TextMeshProUGUI text_TMP;

	private void Start()
	{
		text_string = "Hello! My name is Peter, and I am nine years old!";
		text_TMP = GetComponent<TextMeshProUGUI>();

		StartCoroutine(StartSpawnRoutine());
	}
	IEnumerator StartSpawnRoutine()
	{
		int characters = text_string.Length;
		int i = 0;

		while (i != characters)
		{
			text_TMP.text = text_string.Substring(0,i) + "|";
			i++;

			yield return new WaitForSeconds(0.03f);
		}
		text_TMP.text = text_string;
	}


	void CompleteText()
	{
		StopCoroutine(StartSpawnRoutine());
		text_TMP.text = text_string;
	}
	
}
