using System.Collections;
using System.Linq;
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
        
        public static int nowPlayerCount = 0;
        
        void Start()
        {
            //呱：建立连接
            PhotonNetwork.ConnectUsingSettings();

            #region 订阅事件

            //呱：订阅 【确认姓名】 这个事件
            EventBus.OnConfirmPlayerNameEvent += SetOwnPlayerPlayerName;
            
            //呱：订阅 【需要房间名】 这个事件
            EventBus.OnNeedRoomPasswordEvent += CreateRoomWithPassword;
            
            //呱：订阅 【不需要房间名】 这个事件
            EventBus.OnNoNeedRoomPasswordEvent += CreateRoomWithoutPassword;

            #endregion
        }

        
        //呱：如果成功连上了主服务器
        public override void OnConnectedToMaster()
        {
            Debug.Log("摩西摩西呆胶布得死噶");
            SceneTransitionManager.Instance.FadeOutSceneWithAllWhite("Menu_PlayerSetup");

        }

        //呱：如果 创建房间的时候 需要密码
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

        //呱：如果  创建房间的时候 不需要密码
        public void CreateRoomWithoutPassword(string roomName)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName,new RoomOptions(){MaxPlayers = 4},default);
            Debug.Log($"成功创建房间：{roomName},无密码");
        }

        //呱：如果玩家 需要设置自己的名字
        public void SetOwnPlayerPlayerName(string playerName)
        {
            Debug.Log($"我记住你的名字了{playerName}");
            PhotonNetwork.NickName = playerName;
        }

        public override void OnJoinedRoom()
        {
            GetRoomPlayerNamesAndNums();
        }

        //呱：当有玩家进入房间
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            GetRoomPlayerNamesAndNums();
        }
        
        //呱：当有玩家离开房间
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            GetRoomPlayerNamesAndNums();
        }

        //呱：获取当前玩家名单 并且把房主排在第一个 传给UI层
        private void GetRoomPlayerNamesAndNums()
        {
            Player master = PhotonNetwork.MasterClient;
            if (master == null) return; 
            
            string[] playerNames = new string[PhotonNetwork.PlayerList.Length];
            playerNames[0] =master.NickName;
            int count = 1;
           

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.PlayerList[i] != master )
                {
                    playerNames[count] =PhotonNetwork.PlayerList[i].NickName;
                        count++;
                }
            }
            

            nowPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            EventBus.PlayerNumChanged(playerNames, PhotonNetwork.CurrentRoom.PlayerCount); 
        }
        
        
    }
}
