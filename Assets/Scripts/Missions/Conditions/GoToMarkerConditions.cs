using UnityEngine;

public class GoToMarkerConditions : MissionConditions
{
	private bool isMeet;
	
	private void OnEnable()
	{
		CCDS_Events.OnEnteredMarker += OnEnteredMarker;
	}
	
	private void OnDisable()
	{
		CCDS_Events.OnEnteredMarker -= OnEnteredMarker;
	}
	
	private void OnEnteredMarker(CCDS_Marker marker)
	{
		isMeet = true;
	}
	
	public override bool IsMeet()
	{
		return isMeet;
	}
}
