using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;
using Photon.Pun.Demo.Cockpit;
using TMPro;
using UnityEngine.Serialization;

public class JoinRoomController : MonoBehaviour
{
    [Header("主菜单控制器")]
    public GameObject mainMenuController;
    
    [Header("标签TabChooseEffect的列表")]
    public List<TabChooseEffect> tabList = new List<TabChooseEffect>();

    [Header("基础Panel")]
    public  GameObject basicPanel;
    
    [Header("搜索房间的CanvasGroup")]
    public CanvasGroup searchRoomCanvasGroup;
    
    [Header("联机大厅的CanvasGroup")]
    public CanvasGroup lobbyCanvasGroup;
    
    [Header("切换页面的间隔")]
    public float fadeDuration = 0.3f;
    
    //呱：目前正在展示的CanvasGroup
    private CanvasGroup currentShowCanvasGroup;

    void Awake()
    {
        //呱：目前显示的CanvasGroup
        currentShowCanvasGroup = searchRoomCanvasGroup;
    }
    
    //呱：这里想写个很通用的 可以泛用给所有标签按钮 的函数
    public void OnChooseTab(TabChooseEffect chosenButton)
    {
        //呱：把被选择的按钮 放在最上面渲染
        chosenButton.transform.SetAsLastSibling();
        chosenButton.ShowTag();

        int index = 0;
        
        
        foreach (TabChooseEffect tabButton in tabList)
        {
            if (tabButton != chosenButton)
            {
                //呱：没有被选择的按钮们就请放到最下面吧
                tabButton.transform.SetSiblingIndex(index);
                tabButton.HideTag();
                index++;
            }
        }
        
        //呱：把基础面板放在 被选择的按钮 下面一层
        basicPanel.transform.SetSiblingIndex(index);
    }

    //呱：按下返回按钮的时候执行
    public void OnClickBackButton()
    {
        //呱：回到主界面
        mainMenuController.GetComponent<MainMenuController>().BackToMainMenuFrom(E_ChooseButton.JoinRoom);
        
    }
   
    
    //呱：按下搜索房间的标签
    public void OnClickSearchRoomTab()
    {
        if (currentShowCanvasGroup == searchRoomCanvasGroup) return;
            

        currentShowCanvasGroup.DOFade(0, fadeDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                currentShowCanvasGroup.gameObject.SetActive(false);
                
                searchRoomCanvasGroup.gameObject.SetActive(true);
                searchRoomCanvasGroup.DOFade(1, fadeDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        currentShowCanvasGroup =  searchRoomCanvasGroup;
                    });

            });
    }
    
    //呱：按下搜索房间的标签
    public void OnClickLobbyTab()
    {
        if (currentShowCanvasGroup == lobbyCanvasGroup) return;
        currentShowCanvasGroup.DOFade(0, fadeDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                currentShowCanvasGroup.gameObject.SetActive(false);
                
                lobbyCanvasGroup.gameObject.SetActive(true);
                lobbyCanvasGroup.DOFade(1, fadeDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        currentShowCanvasGroup =  lobbyCanvasGroup;
                    });

            });
    }
        
        
    
}
