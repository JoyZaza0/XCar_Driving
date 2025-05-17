using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
	public List<DialogueLine> lines;
}

[System.Serializable]
public class DialogueLine
{
	public string speaker;
	[TextArea]
	public string text;
}
