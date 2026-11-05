using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Dynamic;
using System.Resources;

//呱：这个是事件总线 想要通过这个向两头传达消息
//    莫名其妙觉得像三人组里那个缓和关系的人的定位
public static class EventBus
{

    #region 事件【当客户端连接上主服务器】

    
    //呱：这是一个 当客户端连接上主服务器时 就执行的事件
    public static event Action OnConnectToMasterEvent;
    
    
    //呱：在网络链接成功时 使用
    public static void SuccessConnectedToMaster()
    {
        OnConnectToMasterEvent?.Invoke();
    }
    

    #endregion

    #region 事件【当玩家确定自己的游戏名】

    
    //呱：这是一个 确定玩家本人的名字时 就执行的事件
    public static event Action<string> OnConfirmPlayerNameEvent;

    
    
    //呱：供外界执行OnConfirmNameEvent的函数
    public static void ConfirmPlayerName(string playerName)
    {
        OnConfirmPlayerNameEvent?.Invoke(playerName);
    }
    
    
    #endregion

    #region 事件【当创建房间 需要密码】

    
    //呱：这是一个 需要传房间密码的时候 就执行的事件
    public static event Action<string, string> OnNeedRoomPasswordEvent;
    
    
    //呱：在需要密码来创建房间的时候 使用
    public static void NeedRoomPassword(string roomName, string roomPassword)
    {

        OnNeedRoomPasswordEvent?.Invoke(roomName, roomPassword);
    }

    
    #endregion

    #region 事件【当创建房间 不需要密码】

    
    //呱：这是一个 不需要传房间密码的时候 就执行的事件
    public static event Action<string> OnNoNeedRoomPasswordEvent;
    
    
    //呱：不需要密码来创建房间的时候 使用
    public static void NoNeedRoomPassword(string roomName)
    {

        OnNoNeedRoomPasswordEvent?.Invoke(roomName);
    }


    #endregion

    #region 事件【房间人数发生变动】

    //呱：这是用于房间人数变动的 事件
    public static event Action<string[],int> OnPlayerNumChangedEvent;

    public static void PlayerNumChanged(string[] playNameList,int playerNum)
    {
        OnPlayerNumChangedEvent?.Invoke(playNameList,playerNum);
    }
 
    #endregion
    


}
