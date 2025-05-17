using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontReplacerSceneOnly : EditorWindow
{
	TMP_FontAsset newTMPFont;
	Font newUIFont;

	[MenuItem("Tools/Replace Fonts In Current Scene")]
	public static void ShowWindow()
	{
		GetWindow<FontReplacerSceneOnly>("Change Fonts");
	}

	void OnGUI()
	{
		GUILayout.Label("Выбор шрифтов", EditorStyles.boldLabel);
		newTMPFont = (TMP_FontAsset)EditorGUILayout.ObjectField("TMP Font Asset", newTMPFont, typeof(TMP_FontAsset), false);
		newUIFont = (Font)EditorGUILayout.ObjectField("UI Font (Text)", newUIFont, typeof(Font), false);

		if (GUILayout.Button("Replace Fonts"))
		{
			ReplaceFontsInCurrentScene();
		}
	}

	void ReplaceFontsInCurrentScene()
	{
		int replacedTMP = 0;
		int replacedUI = 0;

		var allObjects = GameObject.FindObjectsOfType<Transform>(true);

		foreach (var t in allObjects)
		{
			var tmp = t.GetComponent<TextMeshProUGUI>();
			if (tmp && newTMPFont && tmp.font != newTMPFont)
			{
				Undo.RecordObject(tmp, "Replace TMP Font");
				tmp.font = newTMPFont;
				EditorUtility.SetDirty(tmp);
				replacedTMP++;
			}

			var uiText = t.GetComponent<Text>();
			if (uiText && newUIFont && uiText.font != newUIFont)
			{
				Undo.RecordObject(uiText, "Replace UI Font");
				uiText.font = newUIFont;
				EditorUtility.SetDirty(uiText);
				replacedUI++;
			}
		}

		Debug.Log($"Заменено шрифтов: TextMeshProUGUI — {replacedTMP}, UI.Text — {replacedUI}");
	}
}
