using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
	public float currentScale;
	const int originalWidth = 1920;

	[Header("UI Objects")]
	public GameObject DialogueUI;
	public GameObject LifeUI;

	[Header("Life Objects")]
	public GameObject[] livesGameObjects;
	public Texture2D texture_life;
	public Texture2D texture_noLife;
	int livesNum;

	private void Update()
	{
		PositionUI();
	}

	private void Start()
	{
		if(Instance == null)
		{
			Instance = this;
			livesNum = 3;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void PositionUI()
	{
		currentScale = GameManager.Instance.pixelWidth / originalWidth;

		ScaleDialogueUI();
		PositionDialogueUI();

		ScaleLifeUI();
		PositionLifeUI();

	}

	void ScaleDialogueUI()
	{
		DialogueUI.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
	}

	void PositionDialogueUI()
	{
		DialogueUI.transform.localPosition = new Vector3(0, -GameManager.Instance.pixelHeight / 2, 0);
	}

	void PositionLifeUI()
	{
		float width = GameManager.Instance.pixelWidth;
		float height = GameManager.Instance.pixelHeight;
		LifeUI.transform.localPosition = new Vector3(-(width / 2 - width / 15), (height / 2 - height / 15), 0);
	}

	void ScaleLifeUI()
	{
		LifeUI.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
	}

	public void LoseLife()
	{
		livesNum--;
		livesGameObjects[livesNum].GetComponent<RawImage>().texture = texture_noLife;
	}
}

