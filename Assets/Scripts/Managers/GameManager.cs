using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float pixelHeight, pixelWidth;

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

	private void Update()
    {
		pixelWidth = SpatialBridge.cameraService.pixelWidth;
		pixelHeight = SpatialBridge.cameraService.pixelHeight;
	}
}
