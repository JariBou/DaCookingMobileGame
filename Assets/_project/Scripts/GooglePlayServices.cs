using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Hellcooker;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayServices : MonoBehaviour
{
    private string _token;
    [SerializeField] private GameObject _manualConnectButton;
    [SerializeField] private GameObject _achievementsButton;
    [SerializeField] private Image _statusImage;
    [SerializeField] private Slider _progressDebugSlider;

    public string Token => _token;

    private void Awake()
    {
      PlayGamesPlatform.Activate();
      PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void Start()
    {
      PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_hells_cooker, 100.0f, (bool success) => {
        if (success)
        {
          _progressDebugSlider.value = 1f;
        }
      });
    }

    public void ShowAchievements()
    {
      PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    private void ProcessAuthentication(SignInStatus status) {
      
      _statusImage.color = Color.yellow ;

      if (status == SignInStatus.Success)
      {
        // Continue with Play Games Services
        // PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
        // {
        //   _token = code;
        // });
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
