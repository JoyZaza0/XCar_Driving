﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CCDS_AiVehicles", menuName = "SO/CCDS_My/AiVehicles")]
public class CCDS_AiVehicles : ScriptableObject
{

	private static CCDS_AiVehicles instance;

	/// <summary>
	/// Instance of the class.
	/// </summary>
	public static CCDS_AiVehicles Instance { get { if (instance == null) instance = Resources.Load("CCDS_AiVehicles") as CCDS_AiVehicles; return instance; } }


	/// <summary>
	/// All selectable player vehicles.
	/// </summary>
	public AiVehicle[] AiVehicles;

	/// <summary>
	/// Adds a new vehicle to the player vehicles list.
	/// </summary>
	/// <param name="newVehicle"></param>
	public void AddNewVehicle(AiVehicle newVehicle)
	{

		List<AiVehicle> currentVehicles = new List<AiVehicle>();
		currentVehicles = AiVehicles.ToList();
		currentVehicles.Add(newVehicle);
		AiVehicles = currentVehicles.ToArray();

	}
}

[System.Serializable]
public class AiVehicle
{

	public RCCP_CarController vehicle;
	//[Min(0)] public int price = 0;

	[Space()][Range(100f, 1400f)] public float engineTorque = 350f;
	[Range(0f, 1f)] public float handling = .1f;
	[Range(160f, 380f)] public float speed = 240f;

	// [Space()][Range(1f, 2f)] public float upgradedEngineEfficiency = 1.2f;
	// [Range(1f, 2f)] public float upgradedHandlingEfficiency = 1.2f;
	// [Range(1f, 2f)] public float upgradedSpeedEfficiency = 1.2f;

}
