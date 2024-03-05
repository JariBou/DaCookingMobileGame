using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class ClickableHeat : MonoBehaviour
    {
        [FormerlySerializedAs("imageComponent")] [SerializeField] private Image _imageComponent;
        [FormerlySerializedAs("grayDuration")] [SerializeField, Range(0, 0.5f)] private float _grayDuration = 1f;
        [FormerlySerializedAs("grayColor")] [SerializeField] private Color _grayColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        private bool _isGrayed = false;

        private void OnMouseDown()
        {
            if (!_isGrayed)
            {
                StartCoroutine(GrayImage());
            }
        }

        private IEnumerator GrayImage()
        {
            _isGrayed = true;
            Debug.Log("L'image a �t� cliqu�e !");

            _imageComponent.color = _grayColor;

            yield return new WaitForSeconds(_grayDuration);

            _imageComponent.color = Color.white;

            _isGrayed = false;
        }
    }
}
