using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private OptionMenu _optionMenu;
        private MMSoundManager _soundManager;

        private void Start()
        {
            try
            {
                _optionMenu = FindFirstObjectByType<OptionMenu>();
            }
            catch
            {
                Debug.LogError("OptionMenu not found");
            }

            if (_optionMenu != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("TempSoundManager"));
            }

        }
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }


    }
}
