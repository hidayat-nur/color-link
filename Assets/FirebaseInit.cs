using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

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
            }
        });
    }
}
