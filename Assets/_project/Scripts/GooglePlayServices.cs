using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Hellcooker;
using UnityEngine;

namespace _project.Scripts
{
    public class GooglePlayServices : MonoBehaviour
    {
        private void Awake()
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        public void Start() {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_hellcooker, 100.0f, (bool success) => {
                // handle success or failure
            });
        }

        private void ProcessAuthentication(SignInStatus status) {
            if (status == SignInStatus.Success) {
                // Continue with Play Games Services
            } else {
                // Disable your integration with Play Games Services or show a login button
                // to ask users to sign-in. Clicking it should call
                // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            }
        }
    }
}
