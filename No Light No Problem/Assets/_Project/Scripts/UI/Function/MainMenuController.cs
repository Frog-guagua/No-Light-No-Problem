using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum E_ChooseButton
{
    CreateRoom,
    JoinRoom,
    Setting
}


public class MainMenuController : MonoBehaviour
{
   [Header("主菜单的UI")]
   public GameObject mainMenu;
   private CanvasGroup mainMenuCanvasGroup;
   
   [Header("创建房间的UI")]
   public GameObject createRoom;
   private CanvasGroup createRoomCanvasGroup;
   
   [Header("加入房间的UI")]
   public GameObject joinRoom;
   private CanvasGroup joinRoomCanvasGroup;
   
   [Header("设置的UI")]
   public GameObject setting;
   private CanvasGroup settingCanvasGroup;
   
   [Header("UI之间切换时间")]
   [Range(0,1f)]
   public float switchUIDuration;

   void Awake()
   {
       mainMenuCanvasGroup = mainMenu.GetComponent<CanvasGroup>();
       mainMenuCanvasGroup.alpha = 1;
       
       createRoomCanvasGroup = createRoom.GetComponent<CanvasGroup>();
       createRoomCanvasGroup.alpha = 0;
       
       joinRoomCanvasGroup = joinRoom.GetComponent<CanvasGroup>();
       joinRoomCanvasGroup.alpha = 0;
       
       settingCanvasGroup = setting.GetComponent<CanvasGroup>();
       settingCanvasGroup.alpha = 0;
       
       
   }

   public void BackToMainMenuFrom(E_ChooseButton choice)
   {
       switch (choice)
       {
           case E_ChooseButton.CreateRoom:
               createRoomCanvasGroup.DOFade(0, switchUIDuration / 2)
                   .SetEase(Ease.OutQuad)
                   .OnComplete(() =>
                   {
                       createRoom.SetActive(false);
                       mainMenu.SetActive(true);
                       mainMenuCanvasGroup.DOFade(1, switchUIDuration / 2)
                           .SetEase(Ease.InQuad);
                   });
               break;
           case E_ChooseButton.JoinRoom:
               joinRoomCanvasGroup.DOFade(0, switchUIDuration / 2)
                   .SetEase(Ease.OutQuad)
                   .OnComplete(() =>
                   {
                       joinRoom.SetActive(false);
                       mainMenu.SetActive(true);
                       mainMenuCanvasGroup.DOFade(1, switchUIDuration / 2)
                           .SetEase(Ease.InQuad);
                   });
               break;
           case E_ChooseButton.Setting:
               settingCanvasGroup.DOFade(0, switchUIDuration / 2)
                   .SetEase(Ease.OutQuad)
                   .OnComplete(() =>
                   {
                       setting.SetActive(false);
                       mainMenu.SetActive(true);
                       mainMenuCanvasGroup.DOFade(1, switchUIDuration / 2)
                           .SetEase(Ease.InQuad);
                   });
               break;
               
       }
   }

   private void FadeTo(CanvasGroup cg, GameObject o)
   {
       mainMenuCanvasGroup.DOFade(0, switchUIDuration / 2)
           .SetEase(Ease.OutQuad)
           .OnComplete(() =>
           {
               mainMenu.SetActive(false);
               o.SetActive(true);
               cg.DOFade(1, switchUIDuration / 2)
                   .SetEase(Ease.InQuad);
           });
   }
   
   
   public void OnClickCreateRoomButton()
   {
       FadeTo(createRoomCanvasGroup,createRoom);
   }

   public void OnClickJoinRoomButton()
   {
     FadeTo(joinRoomCanvasGroup,joinRoom);
   }
   
   public void OnClickSettingButton()
   {
       FadeTo(settingCanvasGroup,setting);
   }
   
   
}
