using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour
{
    public GameObject gloveUI;   //UI，控制可见与否
    public GloveUI倒计时 GloveUI;
    Vector3 mouseWorldPos;   //鼠标位置

    public GameObject 抓取图像;
    private SpriteRenderer 图像组件;
    
    // Start is called before the first frame update
    void Start()
    {
        图像组件 = 抓取图像.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);   //自身不可见
    }

    // Update is called once per frame
    void Update()
    {
        if (图像组件.sprite == null && StaticThingsManagement.glovePlant != null && StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite != null)
        {
            图像组件.sprite = StaticThingsManagement.glovePlant.GetComponent<Plant>().ownSprite;
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
            图像组件.sprite = null;
        }
    }

    public void clickGlove()
    {
        if(!GloveUI.冷却中)
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
                图像组件.sprite = null;
            }
        }
        else
        {
            return;
        }

        




    }

    public void 取消()
    {
        StaticThingsManagement.glovePlant = null;
        Cursor.visible = true;
        gloveUI.SetActive(true);
        gameObject.SetActive(false);
        图像组件.sprite = null;
    }
}
