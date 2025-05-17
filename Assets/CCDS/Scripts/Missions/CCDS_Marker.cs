//----------------------------------------------
//        City Car Driving Simulator
//
// Copyright � 2014 - 2025 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Mission marker. It's basically a trigger collider. Once player triggers it, gameplay manager script will be used (CCDS_GameplayManager.Instance.EnteredMarker(this)).
/// </summary>
[AddComponentMenu("BoneCracker Games/CCDS/Missions/CCDS Marker")]
public class CCDS_Marker : ACCDS_Component
{

    public CCDS_GameModes.Mode missionMode;
    public ACCDS_Mission connectedMission;
    public TextMeshProUGUI lable;
    public Transform teleportPoint;
    /// <summary>
    /// UI canvas for camera rotation.
    /// </summary>
    [Space()] public Transform UI;

    private void Update()
    {

        //  Set rotation of the UI canvas.
	    if (UI && Camera.main)
		    UI.rotation = Camera.main.transform.rotation;

    }

	//[ContextMenu("Setup")]
	//public void Setup()
    //{
    //	teleportPoint = transform.Find("Teleport Point");	
    //	lable = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    //}

    /// <summary>
    /// On trigger.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
	{
    	
		BCG_EnterExitPlayer playerCharacter = other.GetComponent<BCG_EnterExitPlayer>();

		if (playerCharacter)
		{
			CCDS_UI_Informer.Instance.Info("For This Mission Need Vehicle");
			return;
		}

        //  Return if gameplay manager not found.
        if (!CCDS_GameplayManager.Instance)
        {

            Debug.LogError("CCDS_GameplayManager couldn't found, can't start the mission! Create CCDS_SceneManager and check the scene setup. Tools --> BCG --> CCDS --> Create --> Scene Managers --> Gameplay --> CCDS Scene Manager");
            return;

        }

        //  Finding the player vehicle.
        CCDS_Player player = CCDS_GameplayManager.Instance.player;

        //  Return if player not found.
        if (!player)
        {

            Debug.LogError("Couldn't found the player vehicle!");
            return;

        }

        CCDS_Player triggeredPlayer = other.GetComponentInParent<CCDS_Player>();

        if (!triggeredPlayer)
		    return;
        
		//  If triggered vehicle and local player vehicle is the same, load the main menu.
		if (!Equals(player.gameObject, triggeredPlayer.gameObject))
			return;

		//  Calling ''EnteredMarker'' on the gameplay manager to initialize and start the mission.
		if(connectedMission.uiMode == MissionUIMode.None)
		{
			CCDS_GameplayManager.Instance.EnteredMarker(this);
		}
		else if(connectedMission.uiMode == MissionUIMode.WithPopup)
		{
			string info = connectedMission.misssionStartInfo;
			CCDS_UI_Informer.Instance.OpenMissionPoup(info, () =>
			{
				//CCDS_GameplayManager.Instance.EnteredMarker(this);
				SeparateMissions separateMissions = CCDS_MissionObjectiveManager.Instance.separateMissions;
				CCDS_GameplayManager.Instance.EnteredMarker(separateMissions.GetCurrentMission(connectedMission));
				CCDS_UI_Informer.Instance.CloseMissionPopup();
			});
		}
		else if(connectedMission.uiMode == MissionUIMode.OnlyCompletePopup)
		{
			SeparateMissions separateMissions = CCDS_MissionObjectiveManager.Instance.separateMissions;
			CCDS_GameplayManager.Instance.EnteredMarker(separateMissions.GetCurrentMission(connectedMission));
		}
          
		CCDS_Events.Event_OnEnteredMarker(this);
       
    }

    private void OnTriggerExit(Collider other)
    {
        CCDS_UI_Informer.Instance.CloseMissionPopup();
    }

}
