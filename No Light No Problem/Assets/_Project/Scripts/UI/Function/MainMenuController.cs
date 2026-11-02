using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
       createRoomCanvasGroup = createRoom.GetComponent<CanvasGroup>();
       createRoomCanvasGroup = createRoom.GetComponent<CanvasGroup>();
       joinRoomCanvasGroup = joinRoom.GetComponent<CanvasGroup>();
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
