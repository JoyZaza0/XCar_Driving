using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
	public static DialogueUI Instance;

	public GameObject panel;
	public TMP_Text speakerText;
	public TMP_Text dialogueText;
	public Button nextButton;
	[SerializeField] private float printDelay = 0.05f;

	private Dialogue currentDialogue;
	private int currentIndex = 0;
	private System.Action onDialogueEnd;

	void Awake()
	{
		Instance = this;
		nextButton.onClick.AddListener(NextLine);
		panel.SetActive(false);
	}
	
	

	public void StartDialogue(Dialogue dialogue, System.Action onEnd = null)
	{
		currentDialogue = dialogue;
		currentIndex = 0;
		onDialogueEnd = onEnd;
		panel.SetActive(true);
		ShowLine();
	}

	private void ShowLine()
	{
		if (currentIndex < currentDialogue.lines.Count)
		{
			var line = currentDialogue.lines[currentIndex];
			
			if(line != null)
			{
				speakerText.text = line.speaker;
				string newLine = null;
			
				if(line.text.Contains("Player"))
				{
					newLine = line.text.Replace("Player",CCDS.GetPlayerName());
				}
			
				string text = newLine ?? line.text;
			
				StartCoroutine(PrintText(text,printDelay));
				
				EnablePlayerUi(false);
		
			}
			
		}
		else
		{
			EndDialogue();
		}
	}

	private void NextLine()
	{
		currentIndex++;
		ShowLine();
	}

	private void EndDialogue()
	{
		EnablePlayerUi(true);
		panel.SetActive(false);
		onDialogueEnd?.Invoke();
	}
	
	private IEnumerator PrintText(string text,float delay)
	{
		dialogueText.text = "";
		
		for (int i = 0; i < text.Length; i++) 
		{
			nextButton.interactable = false;
			dialogueText.text  = text.Substring(0, i + 1);
			//dialogueText.text += text[i];
			yield return new WaitForSeconds(delay);
		}
		
		nextButton.interactable = true;
	}
	
	private void EnablePlayerUi(bool enable)
	{
		BCG_EnterExitManager.Instance.cachedCanvas.HidePlayerUi(!enable);
		BCG_EnterExitManager.Instance.cachedCanvas.playerCanvasGroup.alpha = enable? 1 : 0;
		BCG_EnterExitManager.Instance.cachedCanvas.playerCanvasGroup.blocksRaycasts = enable;
	}
}
