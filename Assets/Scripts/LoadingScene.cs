using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingScene : MonoBehaviour
{
	[SerializeField] private Slider slider;
	
	private void Start()
	{
		StartCoroutine(LoadSceneAsync());
	}
	
	private IEnumerator LoadSceneAsync()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(CCDS.GetScene());
		
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			slider.value = progress;
						
			yield return null;
		}	
	}
}
