//----------------------------------------------
//        City Car Driving Simulator
//
// Copyright � 2014 - 2025 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Race mission.
/// </summary>
[AddComponentMenu("BoneCracker Games/CCDS/Missions/CCDS MissionObjective Race")]
public class CCDS_MissionObjective_Race : ACCDS_Mission, ICCDS_CheckEditorError
{

    /// <summary>
    /// All racers.
    /// </summary>
    public List<ACCDS_Vehicle> racers = new List<ACCDS_Vehicle>();

    /// <summary>
    /// Waypoint path for the pursuit vehicle.
    /// </summary>
    public RCCP_AIWaypointsContainer waypointPath;

    /// <summary>
    /// Finish line.
    /// </summary>
    public CCDS_MissionObjective_Race_Finisher finisher;

    /// <summary>
    /// Positioners. Will be used on all racers to track their positions in the race.
    /// </summary>
    private List<CCDS_RacePositioner> positioners = new List<CCDS_RacePositioner>();

    /// <summary>
    /// Remaining racers.
    /// </summary>
    [Space()] public int remainingRacers = 0;

    /// <summary>
    /// Total racers.
    /// </summary>
    public int totalRacers = 0;
	[SerializeField] private VehicleSpawner vehicleSpawner;
	[SerializeField] private CanvasGroup[] uiGroups;

    private void SpawnVehicles()
    {
        if (racers == null)
            racers = new List<ACCDS_Vehicle>();

        racers.Clear();

        racers = vehicleSpawner.GetVehicles(transform);

        if (racers == null)
            throw new NullReferenceException("Racers is null");

        for (int i = 0; i < racers.Count; i++)
        {
            if (racers[i] != null)
            {
                racers[i].CarController.canControl = false;
                racers[i].waypointPath = waypointPath;
                racers[i].gameObject.SetActive(true);
            }
        }
    }
   
    private void OnEnable()
	{
		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "City")
		{
			CCDS.SetScene(2);
			UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
			return;
		}
		
        SpawnVehicles();

        if (racers == null || (racers != null && racers.Count == 0))
        {

            Debug.LogError("Racers couldn't found on this " + transform.name + ", please assign or create new racers for this manager!");
            enabled = false;
            return;

        }

        for (int i = 0; i < racers.Count; i++)
        {

            if (racers[i] != null)
            {

                //  Setting canControl bool of the racer to false before the race begins..
                racers[i].CarController.canControl = false;

                //  Setting waypoint paths of the racers.
                racers[i].waypointPath = waypointPath;

                //  Adding or getting the race positioner component on the racer. 
                if (!racers[i].TryGetComponent(out CCDS_RacePositioner positioner))
                {

                    //  Adding or getting the race positioner component on the racer, and setting waypoint path and getting the closest waypoint in that path.
                    positioner = racers[i].gameObject.AddComponent<CCDS_RacePositioner>();
                    positioner.waypointPath = waypointPath;
                    //positioner.GetClosestWaypoint();

                }

            }

        }

        //  Getting the player.
        CCDS_Player player = CCDS_GameplayManager.Instance.player;

        //  If player found...
        if (player)
        {

            //  Adding or getting the race positioner component on the player. 
            if (!player.TryGetComponent(out CCDS_RacePositioner positioner))
            {

                //  Adding or getting the race positioner component on the player, and setting waypoint path and getting the closest waypoint in that path.
                positioner = player.gameObject.AddComponent<CCDS_RacePositioner>();
                positioner.waypointPath = waypointPath;
                //positioner.GetClosestWaypoint();

            }

        }

        if (positioners == null)
            positioners = new List<CCDS_RacePositioner>();

        positioners.Clear();

        if (player && player.transform.TryGetComponent(out CCDS_RacePositioner poser))
            positioners.Add(poser);

        //  Getting all positioners.
        for (int i = 0; i < racers.Count; i++)
        {

            if (racers[i] != null && racers[i].transform.TryGetComponent(out CCDS_RacePositioner playerPoser))
                positioners.Add(playerPoser);

        }

        //  Make sure everything is back to default when re-enabling the race.
        Restart();

        CCDS_Events.OnAdditionalCountdownStarted += CCDS_Events_OnAdditionalCountdownStarted;
        CCDS_Events.OnMissionCountdownStarted += CCDS_Events_OnMissionCountdownStarted;

        //  Listening an event when the race starts. It will be used to set canControl bool of the racers to true.
        CCDS_Events.OnMissionStarted += CCDS_Events_OnMissionStarted;

    }

    private void CCDS_Events_OnAdditionalCountdownStarted()
    {
        RCCP_SceneManager.Instance.activePlayerCamera.ChangeCamera(RCCP_Camera.CameraMode.CINEMATIC);
	    RCCP_SceneManager.Instance.activePlayerVehicle.SetCanControl(false); 
	    
	    foreach (var group in uiGroups)
	    {
	    	group.alpha = 0;
	    	group.interactable = false;
	    }
    }
    
    private void CCDS_Events_OnMissionCountdownStarted()
    {
	    RCCP_SceneManager.Instance.activePlayerCamera.ChangeCamera(RCCP_Camera.CameraMode.TPS);
	    
	    foreach (var group in uiGroups)
	    {
	    	group.alpha = 1;
	    	group.interactable = true;
	    }
    }

    /// <summary>
    /// When mission started.
    /// </summary>
    private void CCDS_Events_OnMissionStarted()
    {
        //  Setting canControl bool of the racers to true when race starts.
        for (int i = 0; i < racers.Count; i++)
	        racers[i].CarController.canControl = true;
            
    }

    private void OnDisable()
    {

        //  Removing the positioners from the racers. Looping all racers...
        if (racers != null && racers.Count > 0)
        {
            for (int i = 0; i < racers.Count; i++)
            {
                // //  Getting the race positioner component on the racer, and destroying it.
                // if (racers[i] != null && racers[i].TryGetComponent(out CCDS_RacePositioner positioner))
                // {
                //     if (positioner != null)
                //         Destroy(positioner);
                // }

                if (racers[i] != null)
                {
                    Destroy(racers[i].gameObject);
                }
            }
        }

        //  Setting defaults to zero.
        remainingRacers = 0;
        totalRacers = 0;
        percentage = -1f;
        percentageOver = racers.Count + 1;

        CCDS_Events.OnAdditionalCountdownStarted -= CCDS_Events_OnAdditionalCountdownStarted;

        CCDS_Events.OnMissionCountdownStarted -= CCDS_Events_OnMissionCountdownStarted;

        //  Not listening to event when the race starts.
        CCDS_Events.OnMissionStarted -= CCDS_Events_OnMissionStarted;


    }

    /// <summary>
    /// Restarts the mission. Everything goes back to the default settings.
    /// </summary>
    public void Restart()
    {

        //  Setting defaults to zero.
        remainingRacers = 0;
        totalRacers = 0;
        percentage = -1f;
        percentageOver = (float)(racers.Count + 1);

        //  Setting canControl bool of the racers to false and their damage to 0. Looping all racers...
        for (int i = 0; i < racers.Count; i++)
        {

            //  Setting canControl bool of the racers to false and their damage to 0.
            racers[i].CarController.canControl = false;
            racers[i].CarController.OtherAddonsManager.AI.currentWaypointIndex = 0;
            racers[i].damage = 0f;

            // //  Setting default positions and rotations.
            // if (defaultPositions[i] != Vector3.zero)
            //     RCCP.Transport(racers[i].CarController, defaultPositions[i], defaultRotations[i]);

            racers[i].GetClosestWaypoint();

        }

    }

    private void Update()
    {

        //  Getting the player.
        CCDS_Player player = CCDS_GameplayManager.Instance.player;

        //  If racers found, get remaining and total racer count. Otherwise set them to 0.
        if (racers != null && racers.Count > 0)
        {

            remainingRacers = 0;
            totalRacers = racers.Count;

            for (int i = 0; i < racers.Count; i++)
            {

                if (racers[i] != null && !racers[i].finished)
                    remainingRacers++;

            }

        }
        else
        {

            remainingRacers = 0;
            totalRacers = 0;

        }

        //  Calculating the percentage.
        if (remainingRacers > 0 && totalRacers > 0)
            percentage = Mathf.Lerp(100f, 0f, (float)remainingRacers / (float)totalRacers);
        else
            percentage = -1f;

        //  Sorting the positions.
        positioners.Sort(SortOrder);

        //  Player position in the race.
        int playerPosition = 0;

        //  Looping all positioners.
        for (int i = 0; i < positioners.Count; i++)
        {

            if (positioners[i] == null)
                continue;

            if (positioners[i].currentWaypoint == null)
                continue;

            //  If positioner is attached on the player vehicle, set player position.
            if (Equals(positioners[i].gameObject, player.gameObject))
            {

                playerPosition = i + 1;
                currentTarget = positioners[i].currentWaypoint.position;

            }

        }

        //  Percentage is player position.
        percentage = playerPosition;

        //  If there are not any remaining racers, and total racers above 0, complete the mission with failure.
        if (remainingRacers == 0 && totalRacers > 0)
            Completed(false);
		
    }

    /// <summary>
    /// Gets all racers.
    /// </summary>
    public void GetAllRacers()
    {

        //  Checking the list. Creating if it's null.
        if (racers == null)
            racers = new List<ACCDS_Vehicle>();

        //  Clearing the list.
        racers.Clear();

        //  Getting all checkpoints.
        racers = GetComponentsInChildren<ACCDS_Vehicle>(true).ToList();

    }

    /// <summary>
    /// Sets the new vehicle as racer vehicle.
    /// </summary>
    /// <param name="newVehicle"></param>
    public void SetRacer(ACCDS_Vehicle newVehicle)
    {

        if (transform.GetComponentInChildren<ACCDS_Vehicle>())
        {

            if (!Equals(transform.GetComponentInChildren<ACCDS_Vehicle>().gameObject, newVehicle.gameObject))
                DestroyImmediate(transform.GetComponentInChildren<ACCDS_Vehicle>().gameObject);

        }

        GetAllRacers();
        racers.Add(newVehicle);

    }

    /// <summary>
    /// Sorting the positioners depending their total distances.
    /// </summary>
    /// <param name="positioner1"></param>
    /// <param name="positioner2"></param>
    /// <returns></returns>
    public int SortOrder(CCDS_RacePositioner positioner1, CCDS_RacePositioner positioner2)
    {

        return positioner2.totalDistance.CompareTo(positioner1.totalDistance);

    }

    /// <summary>
    /// Completes the mission with stated success.
    /// </summary>
    /// <param name="success"></param>
    public void Completed(bool success)
    {

        //  Mission completed.
        CCDS_GameplayManager.Instance.MissionCompleted(success);

    }

    public string[] CheckErrors()
    {

        List<string> errorStrings = new List<string>();

        if (racers == null)
            errorStrings.Add("Missing racers!");

        if (racers != null && racers.Count == 0)
            errorStrings.Add("Missing racer vehicles, assign or create new racer vehicles in the scene!");

        if (waypointPath == null)
            errorStrings.Add("Waypoint path is not selected for " + transform.name + ", please select it in the scene or create a new one!");

        if (finisher == null)
            errorStrings.Add("Finisher is not selected for " + transform.name + ", please select it in the scene or create a new one!");

        return errorStrings.ToArray();

    }

}
