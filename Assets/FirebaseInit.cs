using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;
using Firebase.RemoteConfig;
using System;

namespace Bitberry.ColorLink
{
    public class FirebaseInit : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.IsCompleted)
                {
                    FirebaseApp app = FirebaseApp.DefaultInstance;
                    Debug.Log("Firebase is initialized!");

                    // Log the event after Firebase is initialized
                    FirebaseAnalytics.LogEvent("game_started");

                    SetupRemoteConfig();
                }
            });
        }

        private void SetupRemoteConfig()
        {
            // Set default values (optional)
            var defaults = new System.Collections.Generic.Dictionary<string, object>();
            defaults.Add("showAdBanner", "false");
            defaults.Add("showAdInter", "false");
            defaults.Add("showAdNative", "false");
            defaults.Add("showAdOpen", "false");
            defaults.Add("showAdReward", "false");
            defaults.Add("versionApp", "1");

            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread(defaultsTask => {
                if (defaultsTask.IsCompleted)
                {
                    // Baru fetch setelah set default selesai
                    FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero)
                        .ContinueWithOnMainThread(fetchTask =>
                        {
                            if (fetchTask.IsCompleted && !fetchTask.IsFaulted)
                            {
                                Debug.Log("Remote Config fetched successfully!");
                                FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                                    .ContinueWithOnMainThread(activateTask =>
                                    {
                                        Debug.Log("Remote Config values activated!");
                                        
                                        try
                                        {
                                            int remoteVersion = int.Parse(FirebaseRemoteConfig.DefaultInstance.GetValue("versionApp").StringValue);
                                            #if UNITY_ANDROID && !UNITY_EDITOR
                                            int currentAppVersion = GetAndroidBundleVersionCode();
                                            #else
                                            int currentAppVersion = 3; // fallback kalau di Editor atau platform lain
                                            #endif


                                            // Debug.Log($"Remote Config versionApp: {remoteVersion}");
                                            // Debug.Log($"Current App Version: {currentAppVersion}");

                                            if (remoteVersion > currentAppVersion)
                                            {
                                                Debug.Log("📢 Triggering update_app popup...");
                                                // PopupManager.Instance.Show("update_app");
                                            }
                                            else
                                            {
                                                // Debug.Log("✅ App is up to date.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // Debug.LogError($"❌ Failed to parse versionApp or Application.version. Error: {ex.Message}");
                                        }
                                    });
                            }
                            else
                            {
                                Debug.LogWarning("Remote Config fetch failed.");
                            }
                        });
                }
            });
        }

        private int GetAndroidBundleVersionCode()
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject packageManager = context.Call<AndroidJavaObject>("getPackageManager");
                string packageName = context.Call<string>("getPackageName");
                AndroidJavaObject packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
                return packageInfo.Get<int>("versionCode"); // untuk targetSdk < 28
            }
        }


    }
}
