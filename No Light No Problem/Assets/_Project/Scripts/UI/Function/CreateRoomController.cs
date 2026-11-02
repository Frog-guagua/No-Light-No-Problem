using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomController : MonoBehaviour
{
    [Header("主菜单控制器")]
    public GameObject mainMenuController;
    
    [Header("输入房间名的CanvasGroup")]
    public CanvasGroup inputRoomNameCanvasGroup;
    
    [Header("输入房间名的InputField")]
    public TMP_InputField roomNameInputField;

    
    [Header("确认房间名字的Button")]
    public Button confirmNameButton;
    
    [Header("密码弹窗的CanvasGroup")]
    public CanvasGroup passwordCanvasGroup;
    
    [Header("弹窗的渐入渐出时间")]
    public float popupFadeDuration;
    
    [Header("选择是否输入密码的CanvasGroup")]
    public CanvasGroup choosePasswordCanvasGroup;
    
    [Header("输入密码的CanvasGroup")]
    public CanvasGroup setPasswordCanvasGroup;
    
    [Header("确认密码的Button")]
    public Button confirmPasswordButton;
    
    [Header("输入密码的InputField")]
    public TMP_InputField passwordInputField;
    
    [Header("玩家等待界面的CanvasGroup")]
    public CanvasGroup waitingCanvasGroup;

    [Header("当前玩家名字列表")] 
    public List<TextMeshProUGUI> nowPlayerNameText;
    //呱：用这个判定是进人了还是少人了
    private int lastPlayerNum;
    
    [Header("玩家圆圈")]
    public List<Image> playerImageList;
    public Color activeColor;
    public Color inactiveColor;
    
    [Header("当前玩家数目")]
    public TextMeshProUGUI nowPlayerNum;
    
    [Header("圆圈变颜色的时间")]
    public float roundFadeDuration;
    
    
    [Header("玩家等待提示")]
    public TextMeshProUGUI playerWaitHint;
    


    //呱：依旧脏标记 依旧美味
    private bool hasText;
    
    //呱：用来存储目前房间里的人
    private List<string> currentNames = new List<string>();

    void Awake()
    {
        
        inputRoomNameCanvasGroup.alpha = 1;
        passwordCanvasGroup.alpha = 0;
        passwordCanvasGroup.gameObject.SetActive(false);
    }
    
    
    //呱：这个依旧是给外面 输入房间名字的输入框 的回调函数 OnValueChanged 用的
    //   如果目前还没有输入 那么就不会出现“确认按钮”
    public void SetButtonStateForRoomName(string nowInputText)
    {
        hasText = !string.IsNullOrEmpty(nowInputText);
        
        if (confirmNameButton.gameObject.activeSelf != hasText)
        {
            confirmNameButton.gameObject.SetActive(hasText);
        }
    }
    
    
    //呱：这个是给 确认密码 用的按钮
    //   只有输入4位才能确定 少了不会出现确认按钮
    public void SetButtonStateForPassword(string nowInputText)
    {

        if (nowInputText.Length <= 3) return;

            confirmPasswordButton.gameObject.SetActive(true);
        
    }

    
    //呱：当 按下 Back 按钮的时候执行
    public void OnClickBackButton()
    {
        //EventBus.ClickBackButton();
        
        mainMenuController.GetComponent<MainMenuController>().BackToMainMenuFrom(E_ChooseButton.CreateRoom);
        
    }

    
    //呱：如果玩家决定好了房间名字
    public void OnClickConfirmNameButton()
    {

        passwordCanvasGroup.gameObject.SetActive(true);
        //呱：弹出是否需密码的弹窗
        passwordCanvasGroup.DOFade(1, popupFadeDuration)
            .SetEase(Ease.InSine);
        
    }
    
    public void OnNeedPassword()
    {
        setPasswordCanvasGroup.gameObject.SetActive(true);
        choosePasswordCanvasGroup.gameObject.SetActive(false);

        //呱：先创建一下
        //EventBus.ConfirmRoomPassword(roomNameInputField.text,asswordInputField.text);
    }

    public void OnConfirmPassword()
    {
        inputRoomNameCanvasGroup.gameObject.SetActive(false);
        setPasswordCanvasGroup.gameObject.SetActive(false);
        passwordCanvasGroup.gameObject.SetActive(false);
        EventBus.NeedRoomPassword(roomNameInputField.text,passwordInputField.text);
        waitingCanvasGroup.gameObject.SetActive(true);
    }

    public void OnNoNeedPassword()
    {
        passwordCanvasGroup.gameObject.SetActive(false);
        choosePasswordCanvasGroup.gameObject.SetActive(false);
        EventBus.NoNeedRoomPassword(roomNameInputField.text);
        waitingCanvasGroup.gameObject.SetActive(true);
    }

  
 

    
}
