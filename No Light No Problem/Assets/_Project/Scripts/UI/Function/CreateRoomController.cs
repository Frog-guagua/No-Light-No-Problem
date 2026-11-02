using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomController : MonoBehaviour
{
    [Header("主菜单控制器")]
    public GameObject mainMenuController;
    
    [Header("输入房间名的CanvasGroup")]
    public CanvasGroup inputRoomNameCanvasGroup;
    
    [Header("选择输入密码的CanvasGroup")]
    public CanvasGroup passwordCanvasGroup;


    public void OnClickBackButton()
    {
        mainMenuController.GetComponent<MainMenuController>().BackToMainMenuFrom(E_ChooseButton.CreateRoom);
    }
    

    
}
