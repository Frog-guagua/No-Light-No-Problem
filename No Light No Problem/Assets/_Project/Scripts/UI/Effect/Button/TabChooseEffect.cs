using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TabChooseEffect : MonoBehaviour
{
    [Header("标签Button的本体与子物体")] 
    public Button button;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI subText;
    
    [Header("标签Button的参数")] 
    public Color showTabBackgroundColor;
    public Color showTabMainTextColor;
    public Color showTabSubTextColor;
    public Color hideTabBackgroundColor;
    public Color hideTabMainTextColor;
    public Color hideTabSubTextColor;
    
    public float showPosX;
    public float hidePosX;
    public float hideMoveDuration;
    public float showMoveDuration;
    public float hideFadeDuration;
    public float showFadeDuration;
    
    //呱：所谓“隐藏” 分为两个效果
    //   向左移 + 变颜色
    public void HideTag()
    {
        button.image.DOKill();
        mainText.DOKill();
        subText.DOKill();
        MoveTagToHide(button);
        FadeTagToHide(button);
    }

    public void ShowTag()
    {
       
        button.image.DOKill();
        mainText.DOKill();
        subText.DOKill();
        MoveTagToShow(button);
        FadeTagToShow(button);
    }

    private void MoveTagToHide(Button button)
    {
        RectTransform rect = button.GetComponent<RectTransform>();
        Vector2 targetPos = new Vector2(hidePosX, rect.anchoredPosition.y);
        rect.DOAnchorPos(targetPos, hideMoveDuration).SetEase(Ease.OutQuad);
    }

    private void FadeTagToHide(Button button)
    {
        button.image.DOColor(hideTabBackgroundColor,hideFadeDuration).SetEase(Ease.OutQuad);
        mainText.DOColor(hideTabMainTextColor,hideFadeDuration).SetEase(Ease.OutQuad);
        subText.DOColor(hideTabSubTextColor,hideFadeDuration).SetEase(Ease.OutQuad);
    }
    
    private void MoveTagToShow(Button button)
    {
        RectTransform rect = button.GetComponent<RectTransform>();
        Vector2 targetPos = new Vector2(showPosX, rect.anchoredPosition.y);
        rect.DOAnchorPos(targetPos, showMoveDuration).SetEase(Ease.InQuad);
    }

    private void FadeTagToShow(Button button)
    {
        button.image.DOColor(showTabBackgroundColor,showFadeDuration).SetEase(Ease.InQuad);
        mainText.DOColor(showTabMainTextColor,showFadeDuration).SetEase(Ease.InQuad);
        subText.DOColor(showTabSubTextColor,showFadeDuration).SetEase(Ease.InQuad);
    }

}
