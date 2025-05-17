using UnityEngine;

public class TakeCarConditions : MissionConditions
{
	private bool isMeet;
	
	private void OnEnable()
	{
		BCG_EnterExitPlayer.OnBCGPlayerEnteredAVehicle += OnEnterVehicle;
	}

	private void OnDisable()
	{
		BCG_EnterExitPlayer.OnBCGPlayerEnteredAVehicle -= OnEnterVehicle;
	}

	private void OnEnterVehicle(BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle)
	{
		isMeet = true;
	}
	
	
	public override bool IsMeet()
	{
		return isMeet;
	}
}
