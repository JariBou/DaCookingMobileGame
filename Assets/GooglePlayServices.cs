using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GooglePlayServices : MonoBehaviour
{
    private string _token;
    [SerializeField] private GameObject _manualConnectButton;
    [SerializeField] private GameObject _achievementsButton;
    [SerializeField] private Image _statusImage;
    [SerializeField] private Slider _progressDebugSlider;

    public string Token => _token;

    public void Start()
    {
      PlayGamesPlatform.Activate();
      _progressDebugSlider.value = 1 / 3f;
      PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
      _progressDebugSlider.value = 2 / 3f;
      PlayGamesPlatform.Instance.ReportProgress("CgkI_ND8rucOEAIQAQ", 100.0f, (bool success) => {
      _progressDebugSlider.value = 3 / 3f;
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
        _manualConnectButton.SetActive(false);
        _achievementsButton.SetActive(true);
        _statusImage.color = Color.green;
      } else {
        // Disable your integration with Play Games Services or show a login button
        // to ask users to sign-in. Clicking it should call
        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        _manualConnectButton.SetActive(true);
        _statusImage.color = Color.red;
      }
    }

    public void ManualConnection()
    {
      PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

}
