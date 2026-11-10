using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    
    private Queue<IEnumerator> moveProcessQueue = new Queue<IEnumerator>();

    // 呱：UI滑入
    public void CanvasSlideIn()
    {
        
        for (int i = 0; i < subCanvasGroupsList.Count; i++)
        {
          
            bool isFirst = (i == 0); 
            
            moveProcessQueue.Enqueue(MoveProcess(
                subCanvasGroupsList[i].rectTransform,
                subCanvasGroupsList[i].canvasGroup,
                subCanvasGroupsList[i].endPos,
                subCanvasGroupsList[i].moveDuration,
                subCanvasGroupsList[i].moveEase,
                1, 
                isFirst 
            ));
        }
        StartCoroutine(PlayMoveQueue());
    }

    // 呱：UI滑出
    public void CanvasSlideOut()
    {
       
        for (int i = subCanvasGroupsList.Count - 1; i >= 0; i--)
        {
           
            bool isFirst = (i == 0); 
            
            moveProcessQueue.Enqueue(MoveProcess(
                subCanvasGroupsList[i].rectTransform,
                subCanvasGroupsList[i].canvasGroup,
                subCanvasGroupsList[i].startPos,
                subCanvasGroupsList[i].moveDuration,
                subCanvasGroupsList[i].moveEase,
                0,
                isFirst 
            ));
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
    
 
    private IEnumerator MoveProcess(RectTransform rectTransform, CanvasGroup canvasGroup, Vector2 pos, float moveDuration, Ease moveEase, float alpha, bool isFirst)
    {
        
        rectTransform.DOAnchorPos(pos, moveDuration).SetEase(moveEase);
        
        
        if (!isFirst) 
        {
            canvasGroup.DOFade(alpha, moveDuration).SetEase(moveEase);
        }
       
        
        yield return new WaitForSeconds(moveDuration); 
    }
}