using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CanvasMoveEffect : MonoBehaviour
{
    [Header("展示的位置")]
    public Vector2 showPos;
    
    [Header("隐藏的位置")]
    public Vector2 hidePos;
    
    [Header("展示的间隔")]
    public float showDuration;
    
    [Header("隐藏的间隔")]
    public float hideDuration;
    
    [Header("出现的效果")]
    public Ease showEaseType;
    
    [Header("隐藏的效果")]
    public Ease hideEaseType;
    
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void MoveToShow()
    {
        rectTransform.DOAnchorPos(showPos,showDuration).SetEase(showEaseType);
    }

    public void MoveToHide()
    {
        rectTransform.DOAnchorPos(hidePos,hideDuration).SetEase(hideEaseType);
    }

}
