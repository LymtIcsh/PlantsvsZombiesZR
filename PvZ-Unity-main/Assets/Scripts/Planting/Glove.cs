using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Glove : MonoBehaviour
{
    public GameObject gloveUI;   //UI，控制可见与否
    public GloveUICountdown GloveUI;
    Vector3 mouseWorldPos;   //鼠标位置

     [Header("抓取图像")]
    public GameObject grabImage;
/// <summary>
/// 图像组件
/// </summary>
    private SpriteRenderer _grabSpriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _grabSpriteRenderer = grabImage.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);   //自身不可见
    }

    // Update is called once per frame
    void Update()
    {
        if (_grabSpriteRenderer.sprite == null && StaticThingsManagement.glovePlant != null && StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite != null)
        {
            _grabSpriteRenderer.sprite = StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite;
        }

        //Shovel始终跟随鼠标
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
        

        //点击鼠标左键，自身不可见，UI可见
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

                //ShovelUI不可见
                gloveUI.SetActive(false);
                Cursor.visible = false;  // 隐藏鼠标
                                         //自身可见，跟随鼠标
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
    /// 取消	
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
