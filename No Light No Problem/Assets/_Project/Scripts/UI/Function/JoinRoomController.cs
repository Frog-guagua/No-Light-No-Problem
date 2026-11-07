using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;

public class JoinRoomController : MonoBehaviour
{
    [Header("标签TabChooseEffect的列表")]
    public List<TabChooseEffect> tabList = new List<TabChooseEffect>();

    [Header("基础Panel")]
    public  GameObject basicPanel;

    //呱：这里想写个很通用的 可以泛用给所有标签按钮 的函数
    public void OnChooseTab(TabChooseEffect chosenButton)
    {
        //呱：把被选择的按钮 放在最上面渲染
        chosenButton.transform.SetAsLastSibling();
        chosenButton.ShowTag();

        int index = 0;
        
        
        foreach (TabChooseEffect tabButton in tabList)
        {
            if (tabButton != chosenButton)
            {
                //呱：没有被选择的按钮们就请放到最下面吧
                tabButton.transform.SetSiblingIndex(index);
                tabButton.HideTag();
                index++;
            }
        }
        
        //呱：把基础面板放在 被选择的按钮 下面一层
        basicPanel.transform.SetSiblingIndex(index);
    }

   
    
}
