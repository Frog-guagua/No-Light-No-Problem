using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 这个类用来管理输入名字时的按钮
/// </summary>
public class NameInputController : MonoBehaviour
{
    [SerializeField]
    private Button nextButton;

    [SerializeField] private TMP_InputField inputField;

    //呱：输入名字的那一套UI
    [SerializeField] private GameObject inputNameUI;
    private CanvasGroup inputNameUICanvasGroup;
    
    //呱：显示名字的那一套UI
    [SerializeField] private GameObject displayerNameUI;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [SerializeField] 
    [Range(2f,4f)]
    private float ShowNameDuration = 1f;
    private CanvasGroup displayerNameUICanvasGroup;
    
    
    //呱：我去终于要用上传说中的脏标记了吗我好激动
    private bool hasText;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        inputNameUICanvasGroup = inputNameUI.GetComponent<CanvasGroup>();
        displayerNameUICanvasGroup = displayerNameUI.GetComponent<CanvasGroup>();
    }
    
    
    //呱：这个函数是用来给回调函数 OnValueChange 用的检测器
    //    当玩家真正输入内容以后再显示next的按钮
    //    普通状态都是不可见按钮的
    public void SetButtonState(string nowInputText)
    {
        
        hasText = !string.IsNullOrEmpty(nowInputText);
        
        if (nextButton.gameObject.activeSelf != hasText)
        {
            nextButton.gameObject.SetActive(hasText);
        }

    }
    
    //呱：按下确认按钮后执行的函数
    public void ConfirmPlayerName()
    {
        DeliverName();
        
        nameText.text = 
            $"<color=#4C4C4C><size=60>Hello!</size></color>\n<color=#1B1B1B><size=96>{inputField.text}</size></color>";
        StartCoroutine(UIManager());
        
    }

    private void DeliverName()
    {
        //呱：将当前名字传给服务器
        EventBus.ConfirmPlayerName(inputField.text);
    }
    
    private void SetUIState(float alpha,float duration ,CanvasGroup UICanvasGroup,GameObject UI,Ease ease)
    {
        UICanvasGroup.DOFade(alpha, duration).
            SetEase(ease).
            OnComplete(() =>
            {
                UI.SetActive(alpha !=0);
            });
    }

    private IEnumerator UIManager()
    {
        SetUIState(0,0.5f,inputNameUICanvasGroup,inputNameUI,Ease.OutQuart);
        yield return new WaitForSeconds(0.5f);
        SetUIState(1,1,displayerNameUICanvasGroup,displayerNameUI,Ease.InQuad);
        yield return new WaitForSeconds(ShowNameDuration);
        SceneTransitionManager.Instance.FadeOutSceneWithAllWhite("Menu_Start");
    }
    
}
