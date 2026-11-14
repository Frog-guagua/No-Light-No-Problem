using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

public enum E_SlideDirection
{
    Up,
    Down
}



/// <summary>
/// 这个是子UICanvas需要的的参数数据包
/// </summary>
[Serializable]
public struct ParticalCanvasInfo
{
    #region 移动相关
    
    public Vector2 hidePos;
    public Vector2 showPos;
    public RectTransform rectTransform;
    public float moveDuration;
    public Ease moveEase;
    
    #endregion
    
   public CanvasGroup canvasGroup;

}

public class CanvasParticalMoveEffect : MonoBehaviour
{
    [Header("内部子Canvas以及相关参数设置")]
    public List<ParticalCanvasInfo> subCanvasGroupsList = new List<ParticalCanvasInfo>();
    public float waitingTime;
    
    //呱：排队队列
    private Queue<IEnumerator> moveProcessQueue = new Queue<IEnumerator>();

    // 呱：UI滑入
    //    本来想把泛用性写的更加好一点 但是目前似乎又变成专用脚本了……
    public void CanvasSlideIn()
    {
        
        for (int i = 0; i < subCanvasGroupsList.Count; i++)
        {
            //呱：判断当前子UI 是否为【子UI列表里的第一个】
            bool isFirst = i==0 ? true : false; 
            
            moveProcessQueue.Enqueue(MoveProcess(subCanvasGroupsList[i],E_SlideDirection.Up, isFirst));
        }
        StartCoroutine(PlayMoveQueue());
    }

    // 呱：UI滑出
    public void CanvasSlideOut()
    {
       
        for (int i = subCanvasGroupsList.Count - 1; i >= 0; i--)
        {
           
            bool isFirst = (i == 0); 
            
            moveProcessQueue.Enqueue(MoveProcess(subCanvasGroupsList[i],E_SlideDirection.Down, isFirst));
        }
        StartCoroutine(PlayMoveQueue());
    }

    private IEnumerator PlayMoveQueue()
    {
        while (moveProcessQueue.Count > 0)
        {
            IEnumerator enumerator = moveProcessQueue.Dequeue();
            
            yield return StartCoroutine(enumerator);
            
            yield return new WaitForSeconds(waitingTime);
        }
    }
    
 
    private IEnumerator MoveProcess(ParticalCanvasInfo canvasGroupInfo, E_SlideDirection direction,bool isFirst)
    {
        //呱：这里用状态机来看是上去还是下去
        //   以及对应的透明度
        Vector2 targetPos = (direction == E_SlideDirection.Up)?canvasGroupInfo.showPos : canvasGroupInfo.hidePos;
        int alpha = (direction == E_SlideDirection.Up) ? 1 : 0;

        //呱：用DOTween提供的缓动函数来实现移动
        canvasGroupInfo.rectTransform.DOAnchorPos(targetPos, canvasGroupInfo.moveDuration).SetEase(canvasGroupInfo.moveEase);
        
        //呱：不是列表第一个的子UI 在移动的时候是有 （隐藏-出现）效果的
        if (!isFirst) 
        {
            canvasGroupInfo.canvasGroup.DOFade(alpha,canvasGroupInfo.moveDuration).SetEase(canvasGroupInfo.moveEase);
        }
       
        
        yield return new WaitForSeconds(waitingTime); 
    }
}