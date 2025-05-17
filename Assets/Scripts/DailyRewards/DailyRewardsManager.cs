using UnityEngine;
using TMPro;
using System;

public class DailyRewardsManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI remainingText;
	[SerializeField] private Color startColor;
	[SerializeField] private Color endColor;
	[SerializeField] private float colorAnimatingSpeed = 2f;
	
	private void Start()
	{
		Gley.DailyRewards.API.Calendar.AddClickListener(CalendarButtonClicked);
	}
	
	private void Update()
	{
		TimeSpan timeSpan = Gley.DailyRewards.API.Calendar.GetRemainingTimeSpan();
		
		if(timeSpan != null)
		{
			remainingText.text = timeSpan.ToString(@"dd\.hh\:mm\:ss");
			remainingText.color = Color.white;
			
			if(timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes == 0 && timeSpan.Seconds == 0)
			{
				remainingText.text = "Get Rewrd";
				
				float t = (Mathf.Sin(Time.time * colorAnimatingSpeed) + 1f) / 2f; // t колеблется от 0 до 1
				remainingText.color = Color.Lerp(startColor, endColor, t);
			}
		}
	}
	
	public void ShowCalendar()
	{
		Gley.DailyRewards.API.Calendar.Show();
	}
	
	private void CalendarButtonClicked(int dayNumber, int rewardValue, Sprite rewardSprite)
	{		
		CCDS.ChangeMoney(rewardValue);	
	}
}
