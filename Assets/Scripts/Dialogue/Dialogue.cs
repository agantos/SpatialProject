using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
	public string[] texts;
	int currIndex;

	TextTypingAnimation text;

	private void Start()
	{
		currIndex = 0;
		text = GetComponentInChildren<TextTypingAnimation>();
		Invoke("PlayNext", 0.1f);
	}

	public void PlayNext()
	{
		if(currIndex < texts.Length)
		{
			if (text.HasTextCompleted())
			{
				text.Play(texts[currIndex]);
				currIndex++;
			}
			else
			{
				text.CompleteText();
			}			
		}
	}

}
