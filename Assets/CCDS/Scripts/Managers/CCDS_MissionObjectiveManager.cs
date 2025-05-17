//----------------------------------------------
//        City Car Driving Simulator
//
// Copyright © 2014 - 2025 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Mission manager. Manages and observes all mission objectives on the scene.
/// </summary>
[AddComponentMenu("BoneCracker Games/CCDS/Managers/CCDS Mission Objective Manager")]
public class CCDS_MissionObjectiveManager : ACCDS_Manager
{

    private static CCDS_MissionObjectiveManager instance;

    /// <summary>
    /// Instance of the class.
    /// </summary>
    public static CCDS_MissionObjectiveManager Instance
    {

        get
        {

            if (instance == null)
                instance = FindFirstObjectByType<CCDS_MissionObjectiveManager>();

            return instance;

        }

    }

    /// <summary>
    /// All missions.
    /// </summary>
    public List<ACCDS_Mission> allMissions = new List<ACCDS_Mission>();
    public SeparateMissions separateMissions { get; private set; }

    private void Awake()
    {

        //  Getting all missions.
        GetAllMissions();

        separateMissions = GetComponent<SeparateMissions>();
        //  Deactivating all missions before starting the game.
        DeactivateAllMissions();

    }

    /// <summary>
    /// Gets all missions.
    /// </summary>
    public void GetAllMissions()
    {

        //  Checking the list. Creating if it's null.
        if (allMissions == null)
            allMissions = new List<ACCDS_Mission>();

        //  Clearing the list.
        allMissions.Clear();

        //  Getting all missions, even if they are disabled.
        allMissions = GetComponentsInChildren<ACCDS_Mission>(true).ToList();

    }

    /// <summary>
    /// Deactivates all missions.
    /// </summary>
    public void DeactivateAllMissions()
    {

        //  Checking the list. Creating if it's null.
        if (allMissions == null)
            allMissions = new List<ACCDS_Mission>();

        for (int i = 0; i < allMissions.Count; i++)
        {

            if (allMissions[i] != null)
                allMissions[i].gameObject.SetActive(false);

        }

    }

    /// <summary>
    /// Creates new mission objective.
    /// </summary>
    /// <param name="gameMode"></param>
    /// <returns></returns>
    public ACCDS_Mission CreateNewMissionObjective(CCDS_GameModes.Mode gameMode)
    {

        GameObject newMissionObject = new GameObject("CCDS_Mission_" + gameMode.ToString());
        newMissionObject.transform.SetParent(transform);
        newMissionObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        switch (gameMode)
        {

            case CCDS_GameModes.Mode.Checkpoint:

                newMissionObject.AddComponent<CCDS_MissionObjective_Checkpoint>();

                break;

            case CCDS_GameModes.Mode.Pursuit:

                newMissionObject.AddComponent<CCDS_MissionObjective_Pursuit>();

                break;

            case CCDS_GameModes.Mode.Race:

                newMissionObject.AddComponent<CCDS_MissionObjective_Race>();

                break;

            case CCDS_GameModes.Mode.Trailblazer:

                newMissionObject.AddComponent<CCDS_MissionObjective_Trailblazer>();

                break;

        }

        AddNewMission(newMissionObject.GetComponent<ACCDS_Mission>());
        return newMissionObject.GetComponent<ACCDS_Mission>();

    }

    /// <summary>
    /// Ads a new mission.
    /// </summary>
    /// <param name="newMission"></param>
    public void AddNewMission(ACCDS_Mission newMission)
    {

        allMissions.Add(newMission);

    }

    private void Reset()
    {

        if (allMissions == null)
            allMissions = new List<ACCDS_Mission>();

    }

}
