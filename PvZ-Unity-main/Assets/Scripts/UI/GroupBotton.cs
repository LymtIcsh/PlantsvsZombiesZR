using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBotton : MonoBehaviour
{
    //�л���Ƭ�鰴ť
    public int groupCount;
    public void OnMouseDown()
    {
        GameObject.Find("CardFatherObject").GetComponent<CardFatherGameObject>().ChangeGroup(groupCount);
    }
}
