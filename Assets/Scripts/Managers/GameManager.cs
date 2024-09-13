using SpatialSys.UnitySDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum AvatarEnum { Peter_Smile, Peter_Sad, Peter_RaisedHand, Margaret_Happy, Margaret_Sad, Ms1_Happy, Ms1_Sad, Ms2_RaisedHand, _NULL};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float pixelHeight, pixelWidth;

	[Header("Avatar Variables")]
	public Dictionary<AvatarEnum, Texture2D> avatars;
	public string[] avatarNames;
	public Texture2D[] avatarTable;

	private void Start()
	{
		if(Instance == null)
		{
			Instance = this;
			CreateAvatarDictionary();			
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

	void CreateAvatarDictionary()
	{
		avatars = new Dictionary<AvatarEnum, Texture2D>();

		for(int i = 0; i < avatarNames.Length; i++)
		{
			AvatarEnum name;
			Enum.TryParse(avatarNames[i], out name);
			avatars.Add(name, avatarTable[i]);
		}
	}
}
