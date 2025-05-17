using UnityEngine;

[CreateAssetMenu(menuName = "Mission System/Mission Data")]
public class MissionData : ScriptableObject
{	
	public string missionName;
	[TextArea]
	public string description;
	public Dialogue startDialogue;
	public Dialogue inProgressDialogue;
	public Dialogue completeDialogue;
}
