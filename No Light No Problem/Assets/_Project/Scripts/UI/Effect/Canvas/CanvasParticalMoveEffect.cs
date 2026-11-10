using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct ParticalCanvasInfo
{
   public Vector2 startPos;
   public Vector2 endPos;
   public CanvasGroup canvasGroup;
   public RectTransform rectTransform;
   public float moveDuration;
   public Ease moveEase;
   
}


public class CanvasParticalMoveEffect : MonoBehaviour
{
    [Header("内部子Canvas以及相关参数设置")]
    public List<ParticalCanvasInfo> subCanvasGroupsList = new List<ParticalCanvasInfo>();
    public float waitingTime;
    
    private bool IsMoving = false;
    private Queue<IEnumerator> moveProcessQueue = new Queue<IEnumerator>();

    //呱：UI向上移动 渐渐进入
    public void CanvasSlideIn()
    {
        foreach (ParticalCanvasInfo canvasInfo in subCanvasGroupsList)
        {
            moveProcessQueue.
                Enqueue(MoveProcess(canvasInfo.rectTransform,canvasInfo.canvasGroup,canvasInfo.endPos,canvasInfo.moveDuration,canvasInfo.moveEase,1));
        }
        
        StartCoroutine(PlayMoveQueue());
    }

    //呱：UI向下移动 渐渐消失
    public void CanvasSlideOut()
    {
        foreach (ParticalCanvasInfo canvasInfo in subCanvasGroupsList)
        {
            moveProcessQueue.
                Enqueue(MoveProcess(canvasInfo.rectTransform,canvasInfo.canvasGroup,canvasInfo.startPos,canvasInfo.moveDuration,canvasInfo.moveEase,0));
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
    
    private IEnumerator MoveProcess(RectTransform rectTransform,CanvasGroup canvasGroup,Vector2 pos,float moveDuration,Ease moveEase,float alpha)
    {
        rectTransform.DOAnchorPos(pos,moveDuration).SetEase(moveEase);
        canvasGroup.DOFade(alpha, moveDuration).SetEase(moveEase);
        yield return null;
    }
    
}
