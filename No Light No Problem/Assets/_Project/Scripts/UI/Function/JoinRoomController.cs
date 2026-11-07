using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;

public class JoinRoomController : MonoBehaviour
{
    [Header("标签Button的列表")]
    public List<Button> tabButtonList = new List<Button>();

    [Header("标签Button的参数")] 
    public Color showTabBackgroundColor;
    public Color showTabTextColor;
    public Color hideTabBackgroundColor;
    public Color hideTabTextColor;
    
    public float showPosX;
    public float hidePosX;
    public float hideDuration;
    public float showDuration;
    
    [Header("基础Panel")]
    public  GameObject basicPanel;

    //呱：这里想写个很通用的 可以泛用给所有标签按钮 的函数
    public void OnChooseTab(Button chosenButton)
    {
        //呱：把被选择的按钮 放在最上面渲染
        chosenButton.transform.SetAsLastSibling();
        ShowTag(chosenButton);
        
        //呱：把基础面板放在 被选择的按钮 下面一层
        basicPanel.transform.SetSiblingIndex(1);
        
        
        foreach (Button tabButton in tabButtonList)
        {
            if (tabButton != chosenButton)
            {
                //呱：没有被选择的按钮们就请放到最下面吧
                tabButton.transform.SetAsFirstSibling();
                HideTag(tabButton);
            }
        }
    }

    //呱：所谓“隐藏” 分为两个效果
    //   向左移 + 变颜色
    private void HideTag(Button button)
    {
        MoveTagToHide(button);
        FadeTagToHide(button);
    }

    private void ShowTag(Button button)
    {
        MoveTagToShow(button);
        FadeTagToShow(button);
    }

    private void MoveTagToHide(Button button)
    {
        RectTransform rect = button.GetComponent<RectTransform>();
        Vector2 targetPos = new Vector2(hidePosX, rect.anchoredPosition.y);
        rect.DOAnchorPos(targetPos, hideDuration).SetEase(Ease.OutQuad);
    }

    private void FadeTagToHide(Button button)
    {
        button.image.DOColor(hideTabBackgroundColor,hideDuration).SetEase(Ease.OutQuad);
        button.GetComponentInChildren<TextMeshProUGUI>().DOColor(hideTabTextColor,hideDuration).SetEase(Ease.OutQuad);
        
    }
    
    private void MoveTagToShow(Button button)
    {
        RectTransform rect = button.GetComponent<RectTransform>();
        Vector2 targetPos = new Vector2(showPosX, rect.anchoredPosition.y);
        rect.DOAnchorPos(targetPos, showDuration).SetEase(Ease.InQuad);
    }

    private void FadeTagToShow(Button button)
    {
        button.image.DOColor(showTabBackgroundColor,showDuration).SetEase(Ease.InQuad);
        button.GetComponentInChildren<TextMeshProUGUI>().DOColor(showTabTextColor,showDuration).SetEase(Ease.InQuad);
    }
    
    
}
