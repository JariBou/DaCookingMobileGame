using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Serialization;

public class GooglePlayServices : MonoBehaviour
{
    private string _token;

    public string Token => _token;

    public void Awake()
    {
      PlayGamesPlatform.Activate();
      PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
      PlayGamesPlatform.Instance.ReportProgress("CgkI_ND8rucOEAIQAQ", 100.0f, (bool success) => {
        // handle success or failure
      });
    }

    public void ShowAchievements()
    {
      PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    private void ProcessAuthentication(SignInStatus status) {
      if (status == SignInStatus.Success)
      {
        // Continue with Play Games Services
        PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
        {
          _token = code;
        });
      } else {
        // Disable your integration with Play Games Services or show a login button
        // to ask users to sign-in. Clicking it should call
        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
      }
    }

}
