using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour
{
    public GameObject gloveUI;   //UI�����ƿɼ����
    public GloveUI����ʱ GloveUI;
    Vector3 mouseWorldPos;   //���λ��

    public GameObject ץȡͼ��;
    private SpriteRenderer ͼ�����;
    
    // Start is called before the first frame update
    void Start()
    {
        ͼ����� = ץȡͼ��.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);   //�����ɼ�
    }

    // Update is called once per frame
    void Update()
    {
        if (ͼ�����.sprite == null && StaticThingsManagement.glovePlant != null && StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite != null)
        {
            ͼ�����.sprite = StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite;
        }

        //Shovelʼ�ո������
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
        

        //����������������ɼ���UI�ɼ�
        if (
            (
            Input.GetKeyDown(KeyCode.Mouse1)||
            Input.GetKeyDown(KeyCode.Mouse0)
            )
            && StaticThingsManagement.glovePlant == null
           )
        {
            StaticThingsManagement.glovePlant = null;
            Cursor.visible = true;
            gloveUI.SetActive(true);
            gameObject.SetActive(false);
            ͼ�����.sprite = null;
        }
    }

    public void clickGlove()
    {
        if(!GloveUI.��ȴ��)
        {
            if (gameObject.activeSelf == false)
            {

                //ShovelUI���ɼ�
                gloveUI.SetActive(false);
                Cursor.visible = false;  // �������
                                         //����ɼ����������
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                transform.position = mouseWorldPos;
                gameObject.SetActive(true);

                AudioManager.Instance.PlaySoundEffect(31);
            }
            else
            {
                StaticThingsManagement.glovePlant = null;
                Cursor.visible = true;
                gloveUI.SetActive(true);
                gameObject.SetActive(false);
                ͼ�����.sprite = null;
            }
        }
        else
        {
            return;
        }

        




    }

    public void ȡ��()
    {
        StaticThingsManagement.glovePlant = null;
        Cursor.visible = true;
        gloveUI.SetActive(true);
        gameObject.SetActive(false);
        ͼ�����.sprite = null;
    }
}
