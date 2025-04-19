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
        defaults.Add("showAdBanner", "false");
        defaults.Add("showAdInter", "false");
        defaults.Add("showAdNative", "false");
        defaults.Add("showAdOpen", "false");
        defaults.Add("showAdReward", "false");

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

}
