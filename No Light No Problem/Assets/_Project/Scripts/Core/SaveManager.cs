using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//呱：这是用于读写的需要的命名空间
using System.IO;

[Serializable]
public class PlayerSettings
{
    public string playerName;
    public float musicVolume;
    public float mouseSensitivity;
}

public static class SaveManager 
{
    //呱：首先是指定一条固定存储的路径
    private static string savePath = Application.persistentDataPath + "/Save/playerData.json";
    private static PlayerSettings defaultPlayerSettings 
        = new PlayerSettings()
        {
            playerName = "",
            musicVolume = 80f,
            mouseSensitivity = 50f
                
        };
    
    //呱：全局运行时来存数据的容器
    public static PlayerSettings CurrentSettings { get; private set; } = defaultPlayerSettings;

    
    //呱：对外暴露存档的方法
    public static void Save(PlayerSettings playerSettings)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        string jsonData = JsonUtility.ToJson(playerSettings,true);
        File.WriteAllText(savePath, jsonData);
        Debug.Log("噢噢噢存档成功啦！");
        
    }

    //呱：对外暴露读档的方法
    public static PlayerSettings Load()
    {
        //Debug.Log("我的存档在这里：" + savePath);
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            CurrentSettings = JsonUtility.FromJson<PlayerSettings>(json);
        }
        else
        {
            CurrentSettings = defaultPlayerSettings;
        }
        return CurrentSettings;
    }
    
}
