using UnityEngine;
using OneSignalSDK;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

public class NotificationManager : MonoBehaviour
{
	private void Start()
	{
		//Debug.Log("CHAY VAO DAY");
		RequestAuthorization();

    #if UNITY_ANDROID
		// Replace 'YOUR_ONESIGNAL_APP_ID' with your OneSignal App ID from app.onesignal.com
		OneSignal.Initialize("765a0604-86de-4c8b-a6e8-bf8ebe309028");
		#endif
	}

	public void RequestAuthorization()
	{
		#if UNITY_ANDROID
		Debug.Log("RequestUserPermission : " + Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"));
		if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
		{
			Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
		}
		#endif
	}
}
