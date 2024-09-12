using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSys.UnitySDK;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MakeCameraFirstPerson();
        HideDefaultUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeCameraFirstPerson()
    {
		SpatialBridge.cameraService.forceFirstPerson = true;
	}

	void HideDefaultUI()
    {
        SpatialBridge.coreGUIService.CloseAllCoreGUI();
    }
}
