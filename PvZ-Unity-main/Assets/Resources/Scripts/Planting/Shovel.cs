using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public GameObject shovelUI;   //ShovelUI，控制可见与否
    public GameObject glove;
    Vector3 mouseWorldPos;   //鼠标位置

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);   //自身不可见
    }

    // Update is called once per frame
    void Update()
    {
        //Shovel始终跟随鼠标
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;

        //点击鼠标左键，自身不可见，UI可见
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
                glove.GetComponent<Glove>().取消();
            }
            //ShovelUI不可见
            shovelUI.SetActive(false);
            Cursor.visible = false;
            //自身可见，跟随鼠标
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            transform.position = mouseWorldPos;
            gameObject.SetActive(true);

            //播放音效
            AudioManager.Instance.PlaySoundEffect(31);
        }
        else
        {
            Cursor.visible = true;
            shovelUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void 取消()
    {
        Cursor.visible = true;
        shovelUI.SetActive(true);
        gameObject.SetActive(false);
    }

}
