using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonClickOn : MonoBehaviour
{
    [SerializeField] public Transform _child;
    float scaleYFactor = 0.7f;
    float delay = 0.5f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = _child.localScale;
    }

    private void OnMouseDown()
    {
        StartCoroutine(ScaleChildTemporarily());
    }

    private IEnumerator ScaleChildTemporarily()
    {
        _child.localScale = new Vector3(originalScale.x, originalScale.y * scaleYFactor, originalScale.z);
        yield return new WaitForSeconds(delay);
        _child.localScale = originalScale;
    }
}
