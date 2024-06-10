using UnityEngine;
using Firebase.Extensions;
using Firebase;
using System;
using Firebase.Analytics;

public class FirebaseAppInit : MonoBehaviour
{
    public const string TIME_IN_APP_EVENT = "time_in_app";
    public const string TIME_IN_APP_PARAM = "sec";

    public const string CLICK_ON_OBJECT_EVENT = "object_interacted";
    public const string CLICK_ON_OBJECT_TYPE_PARAM = "object_type";
    public const string CLICK_ON_OBJECT_COUNT_PARAM = "count";

    public static FirebaseAppInit Instance { get; private set; }
    public FirebaseApp FirebaseApplication { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        CheckGooglePlayServices();
    }

    private void CheckGooglePlayServices()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseApplication = FirebaseApp.DefaultInstance;
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    private void InitializeFirebase()
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        AnalyticsTest(134);
    }

    public void AnalyticsTest(double val)
    {
        if (FirebaseApplication != null)
        {
            FirebaseAnalytics.LogEvent(
                "test_event",
                new Parameter("test_param", val));
        }
    }

    public void AnalyticsTimeInApp(double seconds)
    {
        if (FirebaseApplication != null)
        {
            FirebaseAnalytics.LogEvent(
                TIME_IN_APP_EVENT,
                new Parameter(TIME_IN_APP_PARAM, seconds));
        }
    }

    public void AnalyticsClicksObjects(IteractableObjectType type, int count)
    {
        var strType = Enum.GetName(typeof(IteractableObjectType), type);
        if (FirebaseApplication != null)
        {
            FirebaseAnalytics.LogEvent(
                CLICK_ON_OBJECT_EVENT,
                new Parameter(CLICK_ON_OBJECT_TYPE_PARAM, strType),
                new Parameter(CLICK_ON_OBJECT_COUNT_PARAM, count));
        }
    }
}
