using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBotton : MonoBehaviour
{
    //ÇÐ»»¿¨Æ¬×é°´Å¥
    public int groupCount;
    public void OnMouseDown()
    {
        GameObject.Find("CardFatherObject").GetComponent<CardFatherGameObject>().ChangeGroup(groupCount);
    }
}
