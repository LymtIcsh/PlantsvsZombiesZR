//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RollingObject : MonoBehaviour
//{
//    public AudioClip startSound;
//    public AudioClip collideSound;

//    private AudioSource audioSource;

//    public int hurt;

//    public float speed = 5f;
//    private Vector3 direction = Vector3.right;

//    private void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//        audioSource.enabled = true; // 启用 AudioSource
//        audioSource.PlayOneShot(startSound);
//    }

//    void Update()
//    {
//        // 持续向当前方向移动
//        transform.Translate(direction * speed * Time.deltaTime);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Bowling"))
//        {
//            // 获取 Zombie 或 Zombie 的组件
//            //Zombie zombie = collision.transform.GetComponentInParent<Zombie>();
//            Zombie zombieGeneric = collision.transform.GetComponentInParent<Zombie>();

//            // 如果是 Zombie 类型
//            //if (zombie != null)
//            //{
//            //    zombie.beAttacked(hurt, true); // 对 Zombie 执行攻击
//            //}
//            // 如果是 Zombie 类型
//            if (zombieGeneric != null)
//            {
//                zombieGeneric.beAttacked(hurt,1, 1); // 对 Zombie 执行攻击
//            }

//            audioSource.PlayOneShot(collideSound);

//            // 如果当前是向右滚动
//            if (direction == Vector3.right)
//            {
//                // 随机选择一个新的方向（向上或向下斜45度）
//                if (Random.value < 0.5f)
//                {
//                    direction = new Vector3(1, 1, 0).normalized; // 向上斜45度
//                }
//                else
//                {
//                    direction = new Vector3(1, -1, 0).normalized; // 向下斜45度
//                }
//            }
//            // 如果当前是向上或向下斜滚动
//            else if (direction.y != 0)
//            {
//                direction.y = -direction.y; // 颠倒上下方向
//            }
//        }

//        // 如果碰撞到 "RollingDisappearLine"，销毁该对象
//        if (collision.CompareTag("RollingDisappearLine"))
//        {
//            Destroy(gameObject);
//        }
//    }
//}
