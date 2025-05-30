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
        // ������ǰ�����ƶ�
        transform.Translate(direction * speed * Time.deltaTime);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            Plant plant = collision.GetComponent<Plant>();

            // ����� Zombie ����
            if (plant != null)
            {
                if(plant.plantStruct.id == 8 || plant.plantStruct.id == 103)
                {
                    AudioManager.Instance.PlaySoundEffect(6);
                    Destroy(gameObject);
                    return;
                }
                plant.beAttacked(hurt+plant.Ѫ��/10, null,null); // ��ֲ��ִ�й���
            }
          
            AudioManager.Instance.PlaySoundEffect(27);

            // �����ǰ�����ҹ���
            if (direction == Vector3.left)
            {
                // ���ѡ��һ���µķ������ϻ�����б45�ȣ�
                if (Random.value < 0.5f)
                {
                    direction = new Vector3(-1, 1, 0).normalized; // ����б45��
                }
                else
                {
                    direction = new Vector3(-1, -1, 0).normalized; // ����б45��
                }
            }
            // �����ǰ�����ϻ�����б����
            else if (direction.y != 0)
            {
                direction.y = -direction.y; // �ߵ����·���
            }
        }

        // �����ײ�� "RollingDisappearLine"�����ٸö���
        if (collision.CompareTag("RollingDisappearLine"))
        {
            Destroy(gameObject);
        }
    }
}
