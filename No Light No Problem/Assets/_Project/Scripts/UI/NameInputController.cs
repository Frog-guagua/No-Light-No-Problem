using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 这个类用来管理输入名字时的按钮
/// </summary>
public class NameInputController : MonoBehaviour
{
    [SerializeField]
    private Button nextButton;

    //呱：我去终于要用上传说中的脏标记了吗我好激动
    private bool hasText;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
    }
    
    
    //呱：这个函数是用来给回调函数 OnValueChange 用的检测器
    //    当玩家真正输入内容以后再显示next的按钮
    //    普通状态都是不可见按钮的
    public void SetButtonState(string nowInputText)
    {
        Debug.Log("卧槽！输入框里理我了！内容：" + nowInputText); 
        hasText = !string.IsNullOrEmpty(nowInputText);
        
        if (nextButton.gameObject.activeSelf != hasText)
        {
            nextButton.gameObject.SetActive(hasText);
        }

    }

    
}
