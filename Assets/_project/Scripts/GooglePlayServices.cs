using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace _project.Scripts
{
    public class GooglePlayServices : MonoBehaviour
    {
        private void Awake()
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        public void Start() {
            AchievementsHandler.UnlockAchievement(GPGSIds.achievement_bienvenue_en_enfer);
        }

        public static void ProcessAuthentication(SignInStatus status) {
            if (status == SignInStatus.Success) {
                // Continue with Play Games Services
            } else {
                // Disable your integration with Play Games Services or show a login button
                // to ask users to sign-in. Clicking it should call
                // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            }
        }

        public static void ShowAchievements()
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
    }
}
