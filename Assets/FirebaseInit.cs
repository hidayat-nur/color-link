using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;
using Firebase.RemoteConfig;
using System;

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

        #if UNITY_ANDROID
        defaults.Add("banner", "ca-app-pub-3940256099942544/6300978111");
        defaults.Add("inter", "ca-app-pub-3940256099942544/1033173712");
        defaults.Add("native", "ca-app-pub-3940256099942544/2247696110");
        defaults.Add("open", "ca-app-pub-3940256099942544/9257395921");
        defaults.Add("reward", "ca-app-pub-3940256099942544/5224354917");

        #elif UNITY_IOS
        defaults.Add("banner", "ca-app-pub-3940256099942544/2934735716");
        defaults.Add("inter", "ca-app-pub-3940256099942544/4411468910");
        defaults.Add("native", "ca-app-pub-3940256099942544/3986624511");
        defaults.Add("open", "ca-app-pub-3940256099942544/5662855259");
        defaults.Add("reward", "ca-app-pub-3940256099942544/1712485313");

        #else
        defaults.Add("banner", "test_banner");
        defaults.Add("inter", "test_interstitial");
        defaults.Add("native", "test_native");
        defaults.Add("open", "test_open");
        defaults.Add("reward", "test_rewarded");
        #endif

        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);

        // Fetch data
        FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero)
            .ContinueWithOnMainThread(fetchTask =>
            {
                if (fetchTask.IsCompleted && !fetchTask.IsFaulted)
                {
                    Debug.Log("Remote Config fetched successfully!");
                    FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                        .ContinueWithOnMainThread(activateTask =>
                        {
                            // string welcomeMessage = FirebaseRemoteConfig.DefaultInstance.GetValue("welcome_message").StringValue;
                            // Debug.Log("Remote Config value: welcome_message = " + welcomeMessage);
                            // var keys = FirebaseRemoteConfig.DefaultInstance.Keys;

                            // foreach (var key in keys)
                            // {
                            //     var value = FirebaseRemoteConfig.DefaultInstance.GetValue(key);
                            //     Debug.Log($"[RemoteConfig] Key: {key}, Value: {value.StringValue}, Source: {value.Source}");
                            // }
                        });
                }
                else
                {
                    Debug.LogWarning("Remote Config fetch failed.");
                }
            });
    }

}
