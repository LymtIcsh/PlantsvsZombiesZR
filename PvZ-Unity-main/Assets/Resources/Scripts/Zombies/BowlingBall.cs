using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall_Zombie : MonoBehaviour
{
    public AudioClip startSound;
    public AudioClip collideSound;

    public int hurt;

    public float speed = 5f;
    private Vector3 direction = Vector3.left;

    private void Start()
    {
        AudioManager.Instance.PlaySoundEffect(26);
    }

    void Update()
    {
        // 持续向当前方向移动
        transform.Translate(direction * speed * Time.deltaTime);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            Plant plant = collision.GetComponent<Plant>();

            // 如果是 Zombie 类型
            if (plant != null)
            {
                if(plant.plantStruct.id == 8 || plant.plantStruct.id == 103)
                {
                    AudioManager.Instance.PlaySoundEffect(6);
                    Destroy(gameObject);
                    return;
                }
                plant.beAttacked(hurt+plant.血量/10, null,null); // 对植物执行攻击
            }
          
            AudioManager.Instance.PlaySoundEffect(27);

            // 如果当前是向右滚动
            if (direction == Vector3.left)
            {
                // 随机选择一个新的方向（向上或向下斜45度）
                if (Random.value < 0.5f)
                {
                    direction = new Vector3(-1, 1, 0).normalized; // 向上斜45度
                }
                else
                {
                    direction = new Vector3(-1, -1, 0).normalized; // 向下斜45度
                }
            }
            // 如果当前是向上或向下斜滚动
            else if (direction.y != 0)
            {
                direction.y = -direction.y; // 颠倒上下方向
            }
        }

        // 如果碰撞到 "RollingDisappearLine"，销毁该对象
        if (collision.CompareTag("RollingDisappearLine"))
        {
            Destroy(gameObject);
        }
    }
}
