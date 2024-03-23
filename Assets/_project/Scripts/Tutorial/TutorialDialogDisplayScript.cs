using System;
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
        [SerializeField] private GameObject _clickVeil; // If enabled will not allow clicking outside of the tutorial
        [SerializeField] private GameObject _displayGameObject; 
        [SerializeField] private RectTransform _displayTransform;


        private void Awake()
        {
            Unveil();
            Disable();
        }

        public TutorialDialogDisplayScript UpdateInfo(DialogInfo dialogInfo)
        {
            if (dialogInfo.ShouldVeil)
            {
                Veil();
            }
            else
            {
                Unveil();
            }

            _displayTransform.position = dialogInfo.Position;
            
            return UpdateInfo(dialogInfo.CatImage, dialogInfo.Title, dialogInfo.DialogText);
        }
        
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

        public TutorialDialogDisplayScript MoveTo(Vector2 position)
        {
            _displayTransform.position = position;
            return this;
        }

        public void PassButtonCallback(Action predicate)
        {
            if (_actionButton == null) return;
            //TODO but seems fishy 
        }

        public void Veil()
        {
            _clickVeil.SetActive(true);
        }
        
        public void Unveil()
        {
            _clickVeil.SetActive(false);
        }

        public void Enable()
        {
            _displayGameObject.SetActive(true);
        }

        public void Disable()
        {
            _displayGameObject.SetActive(false);
        }

    }
}