using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public GameObject shovelUI;   //ShovelUI�����ƿɼ����
    public GameObject glove;
    Vector3 mouseWorldPos;   //���λ��

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);   //�����ɼ�
    }

    // Update is called once per frame
    void Update()
    {
        //Shovelʼ�ո������
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;

        //����������������ɼ���UI�ɼ�
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cursor.visible = true;
            shovelUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void clickShovel()
    {
        if (gameObject.activeSelf == false)
        {
            if(glove.activeSelf)
            {
                glove.GetComponent<Glove>().ȡ��();
            }
            //ShovelUI���ɼ�
            shovelUI.SetActive(false);
            Cursor.visible = false;
            //����ɼ����������
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            transform.position = mouseWorldPos;
            gameObject.SetActive(true);

            //������Ч
            AudioManager.Instance.PlaySoundEffect(31);
        }
        else
        {
            Cursor.visible = true;
            shovelUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void ȡ��()
    {
        Cursor.visible = true;
        shovelUI.SetActive(true);
        gameObject.SetActive(false);
    }

}
