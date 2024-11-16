using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDefBarsController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _hpSpriteRenderer;
    [SerializeField] private SpriteRenderer _defSpriteRenderer;

    private float _initialWidthHP;
    private float _initialWidthDef;

    private void Start()
    {
        _initialWidthHP = _hpSpriteRenderer.size.x;
        _initialWidthDef = _defSpriteRenderer.size.x;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float healthPercent = currentHealth / maxHealth;
        Vector3 newScale = _hpSpriteRenderer.transform.localScale;
        newScale.x = healthPercent;
        _hpSpriteRenderer.transform.localScale = newScale;

        float newWidth = _initialWidthHP * healthPercent;
        float widthDifference = _initialWidthHP - newWidth;

        _hpSpriteRenderer.transform.localPosition = new Vector3(-widthDifference / 2, _hpSpriteRenderer.transform.localPosition.y, _hpSpriteRenderer.transform.localPosition.z);
    }

    public void UpdateDefBar(float currentDef, float maxDef)
    {
        float defPercent = currentDef / maxDef;
        Vector3 newScale = _defSpriteRenderer.transform.localScale;
        newScale.x = defPercent;
        _defSpriteRenderer.transform.localScale = newScale;

        float newWidth = _initialWidthDef * defPercent;
        float widthDifference = _initialWidthDef - newWidth;

        _defSpriteRenderer.transform.localPosition = new Vector3(-widthDifference / 2, _defSpriteRenderer.transform.localPosition.y, _defSpriteRenderer.transform.localPosition.z);
    }
}
