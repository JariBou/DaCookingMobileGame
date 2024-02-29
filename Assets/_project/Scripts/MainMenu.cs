using UnityEngine;
using UnityEngine.SceneManagement;

namespace _project.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
