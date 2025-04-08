using UnityEngine;
using OneSignalSDK;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class NotificationManager : MonoBehaviour
{
	private void Start()
	{
		//Debug.Log("CHAY VAO DAY");
		RequestAuthorization();

		// Replace 'YOUR_ONESIGNAL_APP_ID' with your OneSignal App ID from app.onesignal.com
		OneSignal.Initialize("5cff07b2-03f9-4f8a-98c2-72284f471e3a");
	}

	public void RequestAuthorization()
	{
		Debug.Log("RequestUserPermission : " + Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"));
		if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
		{
			Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
		}

	}
}
