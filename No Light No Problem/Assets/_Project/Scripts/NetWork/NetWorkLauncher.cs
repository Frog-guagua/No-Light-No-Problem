using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
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
        }

        //呱：如果成功连上了主服务器
        public override void OnConnectedToMaster()
        {
            Debug.Log("摩西摩西呆胶布得死噶");
            
            
            //呱：嗯嗯我觉得可以在这里订阅 按下创建房间按钮后 然后订阅我们自己写个创建房间函数
            //   写一个空壳子试试~
            //   写了一个CreateRoom ↓
        }

        public bool CreateRoom(string roomName)
        {
            
            return PhotonNetwork.JoinOrCreateRoom(roomName,new RoomOptions(){MaxPlayers = 4},default);
        }
        

        void Update()
        {

        }
    }
}
