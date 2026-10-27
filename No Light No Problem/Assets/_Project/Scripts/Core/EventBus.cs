using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//呱：这个是事件总线 想要通过这个向两头传达消息
//    莫名其妙觉得像三人组里那个缓和关系的人的定位
public static class EventBus
{

    //呱：这是一个 当客户端连接上主服务器时 就执行的事件
    public static event Action OnConnectToMasterEvent;
    
    

}
