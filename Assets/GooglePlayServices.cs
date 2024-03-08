using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayServices : MonoBehaviour
{

    public void Start() {
      PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
      PlayGamesPlatform.Instance.ReportProgress("CgkI_ND8rucOEAIQAQ", 100.0f, (bool success) => {
        // handle success or failure
      });
    }

    internal void ProcessAuthentication(SignInStatus status) {
      if (status == SignInStatus.Success) {
        // Continue with Play Games Services
      } else {
        // Disable your integration with Play Games Services or show a login button
        // to ask users to sign-in. Clicking it should call
        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
      }
    }

}
