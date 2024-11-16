using System.Collections;
using System.Collections.Generic;
using AFG;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSaleSound : MonoBehaviour
{
    [SerializeField] private Button _buttonClickSound;
    private void Start()
    {
        // _buttonClickSound.onClick.RemoveAllListeners();
        _buttonClickSound.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        if (GameController.Instance.AudioManager != null)
        {
            GameController.Instance.AudioManager.PlaySFX("saleMusic");
        }
    }
}
