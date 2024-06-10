using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLifecycle : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        FirebaseAppInit.Instance.AnalyticsTimeInApp(Time.realtimeSinceStartup);
    }
}
