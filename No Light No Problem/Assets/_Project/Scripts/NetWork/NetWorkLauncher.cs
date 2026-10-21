using Photon.Pun;
using UnityEngine;

//呱：这里使用photon官方提供的库

namespace _Project.Scripts.NetWork
{
    public class NetWorkLauncher : MonoBehaviourPunCallbacks
    {
        private static NetWorkLauncher instance;

        public static NetWorkLauncher Instance
        {
            //呱：在Lambda表达式里面 => 已经包含了return的意思了 那么就是说 我们不用写成return instance
            get => instance;


            private set => instance = value;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        
        
        void Start()
        {
            //呱：建立连接
            PhotonNetwork.ConnectUsingSettings();
        }

        //呱：如果连上了主服务器
        public override void OnConnectedToMaster()
        {
            Debug.Log("摩西摩西呆胶布得死噶");
        }
    
        void Update()
        {
        
        }
    }
}
