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
//        audioSource.enabled = true; // ���� AudioSource
//        audioSource.PlayOneShot(startSound);
//    }

//    void Update()
//    {
//        // ������ǰ�����ƶ�
//        transform.Translate(direction * speed * Time.deltaTime);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Bowling"))
//        {
//            // ��ȡ Zombie �� Zombie �����
//            //Zombie zombie = collision.transform.GetComponentInParent<Zombie>();
//            Zombie zombieGeneric = collision.transform.GetComponentInParent<Zombie>();

//            // ����� Zombie ����
//            //if (zombie != null)
//            //{
//            //    zombie.beAttacked(hurt, true); // �� Zombie ִ�й���
//            //}
//            // ����� Zombie ����
//            if (zombieGeneric != null)
//            {
//                zombieGeneric.beAttacked(hurt,1, 1); // �� Zombie ִ�й���
//            }

//            audioSource.PlayOneShot(collideSound);

//            // �����ǰ�����ҹ���
//            if (direction == Vector3.right)
//            {
//                // ���ѡ��һ���µķ������ϻ�����б45�ȣ�
//                if (Random.value < 0.5f)
//                {
//                    direction = new Vector3(1, 1, 0).normalized; // ����б45��
//                }
//                else
//                {
//                    direction = new Vector3(1, -1, 0).normalized; // ����б45��
//                }
//            }
//            // �����ǰ�����ϻ�����б����
//            else if (direction.y != 0)
//            {
//                direction.y = -direction.y; // �ߵ����·���
//            }
//        }

//        // �����ײ�� "RollingDisappearLine"�����ٸö���
//        if (collision.CompareTag("RollingDisappearLine"))
//        {
//            Destroy(gameObject);
//        }
//    }
//}
