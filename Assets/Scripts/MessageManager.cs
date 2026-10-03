using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance; 
    
    [SerializeField]
    private Transform textLoc;

    [SerializeField]
    private List<OneMessage> messages;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowText(string text, Color color)
    {
        bool done = false;
        for (int i = 0; i < messages.Count; i++)
        {
            if (!messages[i].gameObject.activeSelf)
            {
                messages[i].ShowMessage(textLoc.position, text, color);
                done = true;
                break;
            }
        }
        if(!done)
            messages[0].ShowMessage(textLoc.position, text, color);
    }
}
