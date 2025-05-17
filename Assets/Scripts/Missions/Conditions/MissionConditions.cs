using UnityEngine;

public abstract class MissionConditions : MonoBehaviour
{
	public bool ShowPopup;
	public abstract bool IsMeet();
}
