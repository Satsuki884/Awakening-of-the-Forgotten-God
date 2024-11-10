using System.Collections;
using TMPro;
using UnityEngine;

namespace AFG
{
    public class LoadingController : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingPage;
        [SerializeField] private TMP_Text[] _loadingTexts;
        private CanvasGroup _canvasGroup;
        private static bool _isFirstLaunchInSession = true;

        void Start()
        {
            if (_isFirstLaunchInSession)
            {
                _canvasGroup = _loadingPage.GetComponent<CanvasGroup>();
                if (_canvasGroup == null)
                {
                    _canvasGroup = _loadingPage.AddComponent<CanvasGroup>();
                }
                StartCoroutine(ShowLoadingPage());
                StartCoroutine(BlinkLoadingTexts());
                
            }
            else
            {
                _loadingPage.SetActive(false);
            }
        }

        void Update()
        {
            if (Input.anyKeyDown)
            {
                StartCoroutine(FadeOutLoadingPage());
            }
        }

        IEnumerator ShowLoadingPage()
        {
            _loadingPage.SetActive(true);
            _canvasGroup.alpha = 1;
            yield return null;
        }

        IEnumerator FadeOutLoadingPage()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / 2; // Adjust the duration as needed
                yield return null;
            }
            _loadingPage.SetActive(false);
            _isFirstLaunchInSession = false;
        }

        IEnumerator BlinkLoadingTexts()
        {
            int index = 0;
            while (_loadingPage.activeSelf)
            {
                for (int i = 0; i < _loadingTexts.Length; i++)
                {
                    _loadingTexts[i].enabled = (i <= index);
                }
                index = (index + 1) % (_loadingTexts.Length + 1);
                yield return new WaitForSeconds(0.5f); // Adjust the blink speed as needed
            }
        }
    }
}