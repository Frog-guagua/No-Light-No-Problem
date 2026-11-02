using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonHoverHint : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{

    [Header("悬停提示")]
    public CanvasGroup hint;
    
    [Header("提示渐入渐出时间")]
    [Range(0, 0.2f)]
    public float hintFadeDuration;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        hint.DOFade(1, hintFadeDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hint.DOFade(0, hintFadeDuration);
    }
}
