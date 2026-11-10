using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
   public static SceneTransitionManager Instance;

   public Image blackMask;
   public Image whiteMask;
   public Canvas canvas;
   public float FadeInTime = 0.5f;
   public float FadeOutTime = 0.5f;


   
   private void Awake()
   {

      #region 单例准备

      if (Instance != null && Instance != this)
      {
         Destroy(gameObject);
         return;
      }

      Instance = this;


      #endregion
         
      SetAlphaTo(blackMask,0);
      SetAlphaTo(whiteMask,0);
      
      
      DontDestroyOnLoad(gameObject);


   }

   private void SetAlphaTo(Image image,float alpha)
   {
      Color tempColor = image.color;
      tempColor.a = alpha;
      image.color = tempColor;
   }
   
   //呱：这个是渐入场景的工具函数 黑->透明
   public void FadeInSceneWithBlack(string sceneName)
   {
      canvas.enabled = true;
       SetAlphaTo(blackMask,1);
       blackMask.DOFade(0,FadeInTime).
          SetEase(Ease.InQuad).
          OnComplete(() =>
          {
             SetAlphaTo(blackMask,0);
             SceneManager.LoadScene(sceneName);
             canvas.enabled = false;
          });
       
   }
   
   //呱：这个是渐入场景的工具函数 白->透明
   public void FadeInSceneWithWhite(string sceneName)
   {
      canvas.enabled = true;
      SetAlphaTo(whiteMask,1);
      whiteMask.DOFade(0,FadeInTime).
         SetEase(Ease.InQuad).
         OnComplete(() =>
         {
            SetAlphaTo(whiteMask,0);
            SceneManager.LoadScene(sceneName);
            canvas.enabled = false;
         });
   }

   //呱：这个是渐出场景的工具函数 透明->黑
   public void FadeOutSceneWithBlack(string sceneName)
   {
      canvas.enabled = true;
      SetAlphaTo(blackMask,0);
      blackMask.DOFade(1,FadeOutTime).
         SetEase(Ease.OutQuad).
         OnComplete(() =>
         {
            SetAlphaTo(blackMask,1);
            SceneManager.LoadScene(sceneName);
            canvas.enabled = false;
         });
      
   }

   //呱：这个是渐出场景的工具函数 透明->白
   public void FadeOutSceneWithWhite(string sceneName)
   {
      canvas.enabled = true;
      SetAlphaTo(whiteMask,0);
      whiteMask.DOFade(1,FadeOutTime).
         SetEase(Ease.OutQuad).
         OnComplete(() =>
         {
            SetAlphaTo(whiteMask,1);
            SceneManager.LoadScene(sceneName);
            canvas.enabled = false;
         });
      
   }
   
   //呱：这个是渐出渐入场景的一体化工具函数 透明->白->透明
   public void FadeOutSceneWithAllWhite(string sceneName)
   {
      canvas.enabled = true;
      SetAlphaTo(whiteMask,0);
      whiteMask.DOFade(1,FadeOutTime).
         SetEase(Ease.OutQuad).
         OnComplete(() =>
         {
            SetAlphaTo(whiteMask,1);
            SceneManager.LoadScene(sceneName);
            whiteMask.DOFade(0,FadeInTime).
               SetEase(Ease.InQuad).
               OnComplete(() =>
               {
                  SetAlphaTo(whiteMask,0);
                  canvas.enabled = false;
               });
         });
      
   }
   
   //呱：这个是渐出渐入场景的一体化工具函数 透明->黑->透明
   public void FadeOutSceneWithAllBlack(string sceneName)
   {
      SetAlphaTo(blackMask,0);
      blackMask.DOFade(1,FadeOutTime).
         SetEase(Ease.OutQuad).
         OnComplete(() =>
         {
            SetAlphaTo(blackMask,1);
            SceneManager.LoadScene(sceneName);
            blackMask.DOFade(0,FadeInTime).
               SetEase(Ease.InQuad).
               OnComplete(() =>
               {
                  SetAlphaTo(blackMask,0);
                  canvas.enabled = false;
               });
         });
      
   }
   
   
}
