using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSys.UnitySDK;
using TMPro;
using System.Xml.Serialization;

public class CameraController : MonoBehaviour
{
    float initialWalkSpeed, initialRunSpeed;
    public static CameraController Instance;

	// Start is called before the first frame update
	void Start()
    {
        if(Instance == null)
        {
            Instance = this;
			MakeCameraFirstPerson();
			Invoke("HideDefaultUI", 1);
		}
        else
        {
            Destroy(this.gameObject);
        }

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
        FreezeMovementAndRotation();
    }

    void FreezeMovementAndRotation()
    {
        // Freeze Rotation
        SpatialBridge.cameraService.lockCameraRotation = true;

        // Freeze Position
        initialWalkSpeed = SpatialBridge.actorService.localActor.avatar.walkSpeed;
		initialRunSpeed = SpatialBridge.actorService.localActor.avatar.runSpeed;

		SpatialBridge.actorService.localActor.avatar.walkSpeed = 0;
		SpatialBridge.actorService.localActor.avatar.runSpeed = 0;
	}

    void UnFreezeMovementAndRotation()
    {
        SpatialBridge.cameraService.lockCameraRotation = false;
        SpatialBridge.actorService.localActor.avatar.walkSpeed = initialWalkSpeed;
        SpatialBridge.actorService.localActor.avatar.runSpeed = initialRunSpeed;
    }
}
