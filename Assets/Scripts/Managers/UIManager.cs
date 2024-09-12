using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
	public float currentScale;
	const int originalWidth = 1920;

	[Header("UI Objects")]
	public GameObject DialogueUI;


	private void Update()
	{
		PositionUI();
	}

	private void Start()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void PositionUI()
	{
		currentScale = GameManager.Instance.pixelWidth / originalWidth;

		// For Dialogue UI
		ScaleDialogueUI();
		PositionDialogueUI();
	}

	void ScaleDialogueUI()
	{
		DialogueUI.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
	}

	void PositionDialogueUI()
	{
		DialogueUI.transform.localPosition = new Vector3(0, -GameManager.Instance.pixelHeight / 2, 0);
	}
}
