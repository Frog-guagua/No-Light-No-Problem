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
    private CanvasMoveEffect searchRoomCanvasMoveEffect;
    private CanvasParticalMoveEffect canvasParticalMoveEffect;
    
    [Header("联机大厅的CanvasGroup")]
    public CanvasGroup lobbyCanvasGroup;
    private CanvasMoveEffect lobbyCanvasMoveEffect;
    
    [Header("切换页面的间隔")]
    public float fadeDuration = 0.3f;
    
    [Header("搜索框的确认Button")]
    public Button searchButton;
    

    
    //呱：目前正在展示的CanvasGroup
    private CanvasGroup currentShowCanvasGroup;
    
    //呱：给按钮的状态用的脏标记
    private bool hasText;

    void Awake()
    {
        //呱：目前显示的CanvasGroup
        currentShowCanvasGroup = searchRoomCanvasGroup;
        
        searchRoomCanvasMoveEffect = searchRoomCanvasGroup.GetComponent<CanvasMoveEffect>();
        canvasParticalMoveEffect = searchRoomCanvasGroup.GetComponent<CanvasParticalMoveEffect>();
        lobbyCanvasMoveEffect = lobbyCanvasGroup.GetComponent<CanvasMoveEffect>();
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
            
        CanvasMoveEffect currentShowCanvasMoveEffect = currentShowCanvasGroup.GetComponent<CanvasMoveEffect>();
        currentShowCanvasMoveEffect.MoveToHide();
        
        
        currentShowCanvasGroup.DOFade(0, fadeDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                currentShowCanvasGroup.gameObject.SetActive(false);
                
                searchRoomCanvasGroup.gameObject.SetActive(true);
                searchRoomCanvasMoveEffect.MoveToShow();
                searchRoomCanvasGroup.DOFade(1, fadeDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        currentShowCanvasGroup =  searchRoomCanvasGroup;
                    });

            });
    }
    
    //呱：按下联机大厅的标签
    public void OnClickLobbyTab()
    {
        if (currentShowCanvasGroup == lobbyCanvasGroup) return;
        
        CanvasMoveEffect currentShowCanvasMoveEffect = currentShowCanvasGroup.GetComponent<CanvasMoveEffect>();
        currentShowCanvasMoveEffect.MoveToHide();
        
        currentShowCanvasGroup.DOFade(0, fadeDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                currentShowCanvasGroup.gameObject.SetActive(false);
                
                lobbyCanvasGroup.gameObject.SetActive(true);
                lobbyCanvasMoveEffect.MoveToShow();
                lobbyCanvasGroup.DOFade(1, fadeDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        currentShowCanvasGroup =  lobbyCanvasGroup;
                    });

            });
    }
        
     
    //呱：这是用于控制 输入房间名字 的确认按钮 的显示
    //    拖动赋值的时候拖给 InputField的 OnValueChanged函数
    public void SetButtonStateForSearchRoom(string nowInputText)
    {
        hasText = !string.IsNullOrEmpty(nowInputText);
        if (searchButton.gameObject.activeSelf != hasText)
        {
            searchButton.gameObject.SetActive(hasText);
        }
        
    }


    //呱：当按下搜索按钮的时候
    public void OnClickSearchRoomButton()
    {
        //呱：这里留一行逻辑 就是要把获取的房间名拿去搜索
        //    根据返回的结果 来指定 显示的界面
        //    就先按那边获取资料的函数的返回值 是布尔变量 来进行判断
        
        canvasParticalMoveEffect.CanvasSlideIn();

    }    
    
}
