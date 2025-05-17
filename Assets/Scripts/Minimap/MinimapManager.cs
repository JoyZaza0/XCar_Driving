using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MTAssets.EasyMinimapSystem;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{	
	
	[SerializeField] private MinimapCamera bigmapCamera;
	
	[SerializeField] private MinimapRenderer minimapRenderer;
	[SerializeField] private MinimapRenderer bigmapRenderer;
	[SerializeField] private GameObject minimapDisplay;
	
	private void OnEnable()
	{
		CCDS_Events.OnLocalPlayerSpawned += OnCCDSPlayerSpawned;
	}
	
	private void OnDisable()
	{
		CCDS_Events.OnLocalPlayerSpawned -= OnCCDSPlayerSpawned;
	}
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(1f);
		
		MinimapCamera cam = CCDS_GameplayManager.Instance.player.GetComponent<MinimapCamera>();
		
		if(cam)
		{
			minimapRenderer.minimapCameraToShow = cam;
			minimapRenderer.gameObject.SetActive(true);
		}
	}
	
	private void OnCCDSPlayerSpawned(CCDS_Player player)
	{
		
	}
	
	
	public void OnClickInMinimapRendererArea(Vector3 clickWorldPos, MinimapItem clickedMinimapItem) 
	{
		OpenBigmap(true);
		
	}
	
	public void OnDragInMinimapRendererArea(Vector3 onStartThisDragWorldPos, Vector3 onDraggingWorldPos)
	{
		Vector3 deltaPositionToMoveMap = (onDraggingWorldPos - onStartThisDragWorldPos) * -1.0f;
		bigmapCamera.transform.localPosition += (deltaPositionToMoveMap * 10.0f * Time.deltaTime);
	}
	
	public void OpenBigmap(bool isOpen)
	{
		
		bigmapRenderer.gameObject.SetActive(isOpen);
		minimapRenderer.gameObject.SetActive(!isOpen);
		
		bigmapRenderer.minimapCameraToShow = bigmapCamera;
		bigmapCamera.gameObject.SetActive(isOpen);
		
		var player = CCDS_GameplayManager.Instance.player;
		
		if(player == null) return;
		
		bigmapCamera.transform.position = player.transform.position;
		
		var minimapItem = player.GetComponent<MinimapItem>();
		
		var minimapItems = minimapItem.GetListOfAllMinimapItemsInThisScene();
		
		for (int i = 0; i < minimapItems.Length; i++)
		{
			minimapItems[i].customGameObjectToFollowRotation = isOpen ? bigmapCamera.transform : CCDS_SceneManager.Instance.RCCPCamera.transform;
			minimapItems[i].sizeOnMinimap = isOpen ? new Vector3(40f *2,0f,40f *2) : new Vector3(40f,0f,40f);
			minimapItems[i].sizeOnHighlight = isOpen ? 40f * 2 : 40f;
			
			if(minimapItems[i].GetComponent<RCCP_CarController>())
				continue;
				
			minimapItems[i].particlesHighlightMode = isOpen ? MinimapItem.ParticlesHighlightMode.WavesIncrease : MinimapItem.ParticlesHighlightMode.Disabled;
		}
		
		minimapDisplay.SetActive(isOpen);
		
	}
	
}
