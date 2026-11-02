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
    public Button confirmButton;
    
    [Header("密码弹窗的CanvasGroup")]
    public CanvasGroup passwordCanvasGroup;
    
    [Header("弹窗的渐入渐出时间")]
    public float popupFadeDuration;
    
    [Header("选择是否输入密码的CanvasGroup")]
    public CanvasGroup choosePasswordCanvasGroup;
    
    [Header("输入密码的CanvasGroup")]
    public CanvasGroup setPasswordCanvasGroup;
    
    
    [Header("输入密码的InputField")]
    public TMP_InputField passwordInputField;
    

    //呱：依旧脏标记 依旧美味
    private bool hasText;

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
        
        if (confirmButton.gameObject.activeSelf != hasText)
        {
            confirmButton.gameObject.SetActive(hasText);
        }
    }
    
    

    public void OnClickBackButton()
    {
        mainMenuController.GetComponent<MainMenuController>().BackToMainMenuFrom(E_ChooseButton.CreateRoom);
        
    }

    
    //呱：如果玩家决定好了房间名字
    public void OnClickConfirmNameButton()
    {
        //呱：先创建一下
        EventBus.ConfirmRoomName(roomNameInputField.text);
        
        passwordCanvasGroup.gameObject.SetActive(true);
        //呱：弹出是否需密码的弹窗
        passwordCanvasGroup.DOFade(1, popupFadeDuration)
            .SetEase(Ease.InSine);

        
    }
    
    public void OnNeedPassword()
    {
        setPasswordCanvasGroup.gameObject.SetActive(true);
        choosePasswordCanvasGroup.gameObject.SetActive(false);
    }

    public void OnNoNeedPassword()
    {
        choosePasswordCanvasGroup.gameObject.SetActive(false);
    }
    
}
