using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Glove : MonoBehaviour
{
    public GameObject gloveUI;   //UI�����ƿɼ����
    public GloveUICountdown GloveUI;
    Vector3 mouseWorldPos;   //���λ��

    [FormerlySerializedAs("ץȡͼ��")] [Header("ץȡͼ��")]
    public GameObject grabImage;
/// <summary>
/// ͼ�����
/// </summary>
    private SpriteRenderer _grabSpriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _grabSpriteRenderer = grabImage.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);   //�����ɼ�
    }

    // Update is called once per frame
    void Update()
    {
        if (_grabSpriteRenderer.sprite == null && StaticThingsManagement.glovePlant != null && StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite != null)
        {
            _grabSpriteRenderer.sprite = StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite;
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
            _grabSpriteRenderer.sprite = null;
        }
    }

    public void clickGlove()
    {
        if(!GloveUI.isCoolingDown)
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
                _grabSpriteRenderer.sprite = null;
            }
        }
        else
        {
            return;
        }

        




    }

    /// <summary>
    /// ȡ��	
    /// </summary>
    public void Cancel()
    {
        StaticThingsManagement.glovePlant = null;
        Cursor.visible = true;
        gloveUI.SetActive(true);
        gameObject.SetActive(false);
        _grabSpriteRenderer.sprite = null;
    }
}
