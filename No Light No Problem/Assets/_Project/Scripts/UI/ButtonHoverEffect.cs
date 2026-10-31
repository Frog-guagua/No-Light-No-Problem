#define DebugMode

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;



public class ButtonHoverEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    #region 调整的颜色参数

    [Header("平时按钮背景颜色")]
    public Color normalBackgroundColor;
    
    [Header("平时按钮文字颜色")]
    public Color normalTextColor;
    
    [Header("悬停按钮背景颜色")]
    public Color hoverBackgroundColor;
    
    [Header("悬停按钮文字颜色")]
    public Color hoverTextColor;
    
    [Header("按下按钮背景颜色")]
    public Color pressBackgroundColor;
    
    [Header("按下按钮文字颜色")]
    public Color pressTextColor;

    #endregion

    #region 调整的渐变时间参数

    [Range(0.1f,1.5f)]
    [Header("悬停渐变时间")]
    public float hoverFadeTime = 1f;
    
    [Range(0.1f,0.5f)]
    [Header("点击渐变时间")]
    public float pressFadeTime = 0.3f;

    #endregion

    #region 调整的透明度参数

    // public float normalAlpha = 0.4f;
    // public float hoverAlpha = 1f;

    #endregion
    //呱：记录当前是否在悬停
    private bool isHovering = false;
    private bool isFading = false;
    
    private Button thisButton;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;
    
    void Start()
    {
        //呱：上来先获取一波组件
        thisButton = GetComponent<Button>();
        buttonImage = thisButton.GetComponent<Image>();
        buttonText = thisButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    
    //呱：开始悬停的时候
    public void OnPointerEnter(PointerEventData eventData)
    {
        //呱：先打个标记记录现在在悬停态
        isHovering = true;

#if DebugMode
        if (isFading)
        {
            buttonImage.DOKill();
            buttonImage.DOKill();
        }
#endif
        
        buttonImage.DOColor(hoverBackgroundColor, hoverFadeTime)
            .SetEase(Ease.InQuad)
            .OnComplete(() => isFading = false);
        
        buttonText.DOColor(hoverTextColor, hoverFadeTime)
            .SetEase(Ease.InQuad)
            .OnComplete(() => isFading = false);
        
    }

    //呱：停止悬停的时候
    public void OnPointerExit(PointerEventData eventData)
    {
        //呱：先宣告悬停结束了
        isHovering = false;
        
#if DebugMode
        if (isFading)
        {
            buttonImage.DOKill();
            buttonImage.DOKill();
        }
#endif
        
        buttonImage.DOColor(normalBackgroundColor, hoverFadeTime)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => isFading = false);
        
        buttonText.DOColor(normalTextColor, hoverFadeTime)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => isFading = false);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isHovering) return;
        buttonImage.DOColor(pressBackgroundColor, pressFadeTime)
            .SetEase(Ease.InQuad)
            .OnComplete(() => isFading = false);
        
        buttonText.DOColor(pressTextColor, pressFadeTime)
            .SetEase(Ease.InQuad)
            .OnComplete(() => isFading = false);
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHovering) return;

    }
}
