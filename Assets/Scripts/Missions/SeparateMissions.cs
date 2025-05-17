using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SeparateMissions : MonoBehaviour
{
	public List<CCDS_MissionObjective_Checkpoint> MissionCheckpoints { get; private set; }
	public List<CCDS_MissionObjective_Trailblazer> MissionTrailblazers { get; private set; }
	public List<CCDS_MissionObjective_Race> MissionRaces { get; private set; }
	public List<CCDS_MissionObjective_Pursuit> MissionPursuits { get; private set; }

	private ACCDS_Mission currentMission;

	public int CurrentCheckpointIndex
	{
		get => PlayerPrefs.GetInt(CHECKPOINT_KEY, 0);

		private set
		{
			if (value < MissionCheckpoints.Count)
				PlayerPrefs.SetInt(CHECKPOINT_KEY, value);
		}
	}
	public int CurrentTrailblazerIndex
	{
		get => PlayerPrefs.GetInt(TRAILBLAZER_KEY, 0);

		private set
		{
			if (value < MissionCheckpoints.Count)
				PlayerPrefs.SetInt(TRAILBLAZER_KEY, value);
		}
	}
	public int CurrentRaceIndex
	{
		get => PlayerPrefs.GetInt(RACE_KEY, 0);

		private set
		{
			if (value < MissionRaces.Count)
				PlayerPrefs.SetInt(RACE_KEY, value);
		}
	}
	public int CurrentPusuitIndex
	{
		get => PlayerPrefs.GetInt(PURSUIT_KEY, 0);

		private set
		{
			if (value < MissionPursuits.Count)
				PlayerPrefs.SetInt(PURSUIT_KEY, value);
		}
	}

	private const string CHECKPOINT_KEY = "Checkpoint";
	private const string TRAILBLAZER_KEY = "Trailblazer";
	private const string RACE_KEY = "Race";
	private const string PURSUIT_KEY = "Pursuit";

	public int TestMissionIndex;

	[ContextMenu("Set Mission Index")]
	public void SetTestIndex()
	{
		CurrentRaceIndex = TestMissionIndex;
	}

	private void OnEnable()
	{
		CCDS_Events.OnMissionStarted += CCDS_Events_OnMissionStarted;
	}
	private void OnDisable()
	{
		CCDS_Events.OnMissionStarted -= CCDS_Events_OnMissionStarted;
	}

	private void Start()
	{
		GetMissions(CCDS_MissionObjectiveManager.Instance.allMissions);
	}

	private void CCDS_Events_OnMissionStarted()
	{
		var mission = CCDS_GameplayManager.Instance.currentMission;

		if (mission is CCDS_MissionObjective_Checkpoint)
		{
			currentMission = mission as CCDS_MissionObjective_Checkpoint;
			CCDS_Settings.Instance.showArrowIndicator = true;
		}
		else if (mission is CCDS_MissionObjective_Trailblazer)
		{
			currentMission = mission as CCDS_MissionObjective_Trailblazer;
			CCDS_Settings.Instance.showArrowIndicator = true;
		}
		else if (mission is CCDS_MissionObjective_Race)
		{
			currentMission = mission as CCDS_MissionObjective_Race;
			CCDS_Settings.Instance.showArrowIndicator = false;
			CCDS_Settings.Instance.showHealthBar = false;
		}
		else if (mission is CCDS_MissionObjective_Pursuit)
		{
			currentMission = mission as CCDS_MissionObjective_Pursuit;
			CCDS_Settings.Instance.showArrowIndicator = false;
			CCDS_Settings.Instance.showHealthBar = true;
		}
	}


	private void GetMissions(List<ACCDS_Mission> allMissions)
	{
		if (allMissions == null || allMissions.Count == 0) return;

		MissionCheckpoints = allMissions.OfType<CCDS_MissionObjective_Checkpoint>().ToList();
		MissionTrailblazers = allMissions.OfType<CCDS_MissionObjective_Trailblazer>().ToList();
		MissionRaces = allMissions.OfType<CCDS_MissionObjective_Race>().ToList();
		MissionPursuits = allMissions.OfType<CCDS_MissionObjective_Pursuit>().ToList();
	}

	public void LevelUpMission(ACCDS_Mission mission)
	{
		if (mission is CCDS_MissionObjective_Checkpoint)
		{
			CurrentCheckpointIndex++;
		}
		else if (mission is CCDS_MissionObjective_Trailblazer)
		{
			CurrentTrailblazerIndex++;
		}
		else if (mission is CCDS_MissionObjective_Race)
		{
			CurrentRaceIndex++;
		}
		else if (mission is CCDS_MissionObjective_Pursuit)
		{
			CurrentPusuitIndex++;
		}
	}

	public bool IsLastMission(ACCDS_Mission currentMission)
	{

		if (currentMission is CCDS_MissionObjective_Checkpoint)
		{
			if (CurrentCheckpointIndex == MissionCheckpoints.Count - 1)
				return true;
		}
		else if (currentMission is CCDS_MissionObjective_Trailblazer)
		{
			if (CurrentTrailblazerIndex == MissionTrailblazers.Count - 1)
				return true;
		}
		else if (currentMission is CCDS_MissionObjective_Race)
		{
			if (CurrentRaceIndex == MissionRaces.Count - 1)
				return true;
		}
		else if (currentMission is CCDS_MissionObjective_Pursuit)
		{
			if (CurrentPusuitIndex == MissionPursuits.Count - 1)
				return true;
		}

		return false;
	}

	public ACCDS_Mission GetCurrentMission()
	{
		if (currentMission is CCDS_MissionObjective_Checkpoint)
		{
			return MissionCheckpoints[CurrentCheckpointIndex];
		}
		else if (currentMission is CCDS_MissionObjective_Trailblazer)
		{
			return MissionTrailblazers[CurrentTrailblazerIndex];
		}
		else if (currentMission is CCDS_MissionObjective_Race)
		{
			return MissionRaces[CurrentRaceIndex];
		}
		else if (currentMission is CCDS_MissionObjective_Pursuit)
		{
			return MissionPursuits[CurrentPusuitIndex];
		}

		return null;
	}
	public ACCDS_Mission GetCurrentMission(ACCDS_Mission mission)
	{
		if (mission is CCDS_MissionObjective_Checkpoint)
		{
			return MissionCheckpoints[CurrentCheckpointIndex];
		}
		else if (mission is CCDS_MissionObjective_Trailblazer)
		{
			return MissionTrailblazers[CurrentTrailblazerIndex];
		}
		else if (mission is CCDS_MissionObjective_Race)
		{
			return MissionRaces[CurrentRaceIndex];
		}
		else if (mission is CCDS_MissionObjective_Pursuit)
		{
			return MissionPursuits[CurrentPusuitIndex];
		}

		return null;
	}

	public string GetMissionTitle(ACCDS_Mission mission)
	{
		if (mission is CCDS_MissionObjective_Checkpoint)
		{
			//return $"Checkpoint {CurrentCheckpointIndex + 1}";
			return $"Level {CurrentCheckpointIndex + 1} \n  Checkpoint";
		}
		else if (mission is CCDS_MissionObjective_Trailblazer)
		{
			//return $"Trailblazer {CurrentTrailblazerIndex + 1}";
			return $"Level {CurrentTrailblazerIndex + 1} \n  Trailblazer";
		}
		else if (mission is CCDS_MissionObjective_Race)
		{
			//return $"Race {CurrentRaceIndex + 1}";
			return $"Level {CurrentRaceIndex + 1} \n  Race";
		}
		else if (mission is CCDS_MissionObjective_Pursuit)
		{
			//return $"Pusuit {CurrentPusuitIndex + 1}";
			return $"Level {CurrentPusuitIndex + 1} \n  Pusuit";
		}

		return null;
	}
	
	public string GetMissionName(ACCDS_Mission mission)
	{
		if (mission is CCDS_MissionObjective_Checkpoint)
		{
			return $"Checkpoint";
		}
		else if (mission is CCDS_MissionObjective_Trailblazer)
		{
			return $"Trailblazer";
		}
		else if (mission is CCDS_MissionObjective_Race)
		{
			return $"Race";
		}
		else if (mission is CCDS_MissionObjective_Pursuit)
		{
			return $"Pusuit";
		}

		return null;
	}

}
