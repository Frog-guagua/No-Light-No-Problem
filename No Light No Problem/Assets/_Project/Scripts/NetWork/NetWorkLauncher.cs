using System.Collections;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;
//呱：这里使用photon官方提供的库

namespace _Project.Scripts.NetWork
{
    public class NetWorkLauncher : MonoBehaviourPunCallbacks
    {

        #region 单例配置

        //呱：c#的自动属性会帮你生成一个隐藏的私有字段 所以不用担心 唯一的单例走丢啦
        public static NetWorkLauncher Instance{get; private set;}
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        
        
        void Start()
        {
            //呱：建立连接
            PhotonNetwork.ConnectUsingSettings();
            
            //呱：订阅 【确认姓名】 这个事件
            EventBus.OnConfirmPlayerNameEvent += SetOwnPlayerPlayerName;
            
            //呱：订阅 【需要房间名】 这个事件
            EventBus.OnNeedRoomPasswordEvent += CreateRoomWithPassword;
            
            //呱：订阅 【不需要房间名】 这个事件
            EventBus.OnNoNeedRoomPasswordEvent += CreateRoomWithoutPassword;
            
            EventBus.OnClickBackButton += CloseCurrentRoom;
        }

        //呱：如果成功连上了主服务器
        public override void OnConnectedToMaster()
        {
            Debug.Log("摩西摩西呆胶布得死噶");
            
    
            SceneTransitionManager.Instance.FadeOutSceneWithAllWhite("Menu_PlayerSetup");

        }

        
        public void CreateRoomWithPassword(string roomName, string roomPassword)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            
            ExitGames.Client.Photon.Hashtable password =  new ExitGames.Client.Photon.Hashtable();
            password["password"] = roomPassword;
            roomOptions.CustomRoomProperties = password;
            
            PhotonNetwork.JoinOrCreateRoom(roomName,roomOptions,default);
            Debug.Log($"成功创建房间：{roomName},密码为{roomPassword}");
        }

        
        public void CreateRoomWithoutPassword(string roomName)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName,new RoomOptions(){MaxPlayers = 4},default);
            Debug.Log($"成功创建房间：{roomName},无密码");
        }

        
        public void SetOwnPlayerPlayerName(string playerName)
        {
            Debug.Log($"我记住你的名字了{playerName}");
            PhotonNetwork.NickName = playerName;
        }

        
        public void CloseCurrentRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        
    }
}
