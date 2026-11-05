using System.Collections;
using System.Collections.Generic;
using System.Text;
using _Project.Scripts.NetWork;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LoadingTextEffect : MonoBehaviour
{
    
    public TextMeshProUGUI waitingText;
    public float typingSingleLetterTime;
    public float displayWholeSentenceTime;
    
    private char[] copy;
    private char[] typingText;
    private StringBuilder showingText = new StringBuilder();
    
    void Awake()
    {
        copy = waitingText.text.ToCharArray();
        waitingText.text = "";
    }

    void Start()
    {

        LoadingText();
    }
    

    public void LoadingText()
    {

        StartCoroutine(LoadingTextUI());
       
        
    }
    
    IEnumerator LoadingTextUI()
    {
        while (true)
        {
            if (NetWorkLauncher.nowPlayerCount == 4)
            {
                waitingText.text = "";
                yield break; 
            }
        
            
            showingText.Clear();

            for (int i = 0; i < copy.Length; i++)
            {
                yield return new WaitForSeconds(typingSingleLetterTime);
                showingText.Append(copy[i].ToString()); 
                waitingText.text = showingText.ToString(); 
            }
            
            yield return new WaitForSeconds(displayWholeSentenceTime);
        
            
            for (int i = 0; i < copy.Length; i++)
            {
                yield return new WaitForSeconds(typingSingleLetterTime);
                showingText.Remove(showingText.Length - 1, 1); 
                waitingText.text = showingText.ToString(); 
            }
        }
        
    }
}
