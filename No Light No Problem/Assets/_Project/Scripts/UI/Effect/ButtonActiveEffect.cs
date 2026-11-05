using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonActiveEffect : MonoBehaviour
{
    [Header("按钮失活时")]
    public Color inactiveBackgroundColor;
    public Color inactiveTextColor;
    
    [Header("按钮激活时")]
    public Color activeBackgroundColor;
    public Color activeTextColor;
    
    [Header("激活渐变时长")]
    public float fadeDuration;

    private Image buttonBackground;
    private TextMeshProUGUI buttonText;
    
    void Awake()
    {
        buttonBackground = GetComponent<Image>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    
    public void InactiveButton()
    {
        buttonBackground.DOColor(inactiveBackgroundColor, fadeDuration);
        buttonText.DOColor(inactiveTextColor, fadeDuration);
    }

    public void ActiveButton()
    {
        buttonBackground.DOColor(activeBackgroundColor, fadeDuration);
        buttonText.DOColor(activeTextColor, fadeDuration);
    }
}
