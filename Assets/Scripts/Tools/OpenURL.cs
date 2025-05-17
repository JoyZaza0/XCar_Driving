using UnityEngine;

public class OpenURL : MonoBehaviour
{
	private const string MORE_GAMES = "https://play.google.com/store/apps/developer?id=Open+World+Games";
	private const string PRIVACY_POLICY = "https://sites.google.com/view/openwoldgames/%D0%B3%D0%BB%D0%B0%D0%B2%D0%BD%D0%B0%D1%8F";
	private const string AD_GAME = "https://play.google.com/store/apps/details?id=com.drivers.gotocity";
	
	public void MoreGames()
	{
		Open(MORE_GAMES);
	}
	
	public void PrivacyPolicy()
	{
		Open(PRIVACY_POLICY);
	}
	
	public void AdGame()
	{
		Open(AD_GAME);
	}
	
	public void Open(string url)
	{
		Application.OpenURL(url);
	}
}
