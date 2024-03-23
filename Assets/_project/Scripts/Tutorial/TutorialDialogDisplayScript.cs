using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Tutorial
{
    public class TutorialDialogDisplayScript : MonoBehaviour
    {

        [SerializeField] private Image _catImage;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _dialog;
        [SerializeField] private Button _actionButton; // If needed


        public TutorialDialogDisplayScript UpdateInfo(Sprite image, string dialog)
        {
            return UpdateInfo(image, "", dialog);
        }
        
        public TutorialDialogDisplayScript UpdateInfo(Sprite image, string title, string dialog)
        {
            _catImage.sprite = image;
            if (_title != null) _title.text = title;
            _dialog.text = dialog;
            return this;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

    }
}