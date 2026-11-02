using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomController : MonoBehaviour
{
    
    
    [Header("输入房间名的CanvasGroup")]
    public CanvasGroup inputRoomNameCanvasGroup;
    
    [Header("选择输入密码的CanvasGroup")]
    public CanvasGroup passwordCanvasGroup;

    public void BackToMainMenu()
    {
        inputRoomNameCanvasGroup.alpha = 0;
        inputRoomNameCanvasGroup.interactable = false;
        passwordCanvasGroup.alpha = 0;
        passwordCanvasGroup.interactable = false;
    }
    
}
