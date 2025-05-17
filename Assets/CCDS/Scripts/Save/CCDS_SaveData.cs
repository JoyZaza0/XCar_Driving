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

[System.Serializable]
public class CCDS_SaveData {

    public string playerName = "PlayerName";
    public int playerMoney = 0;
    public int selectedVehicle = 0;
    public int selectedScene = 1;

    public List<int> ownedVehicles = new List<int>();

    public float audioVolume = 1f;
    public float musicVolume = .65f;

    public bool imageEffects = false;
    public bool shadows = false;

	public bool firstGameplay = true;
	public bool firstMission = true;
    
	public Vector3 playerCharacterPosition = new Vector3();

    public CCDS_SaveData() { }

}
