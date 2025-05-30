using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MultiImagePlant
{
    //ƻ���Ȼ���  
    public List<GameObject> bullets;

    private Vector3 bulletOffset = new Vector3(0.656f, 0.392f, 0);   //�ӵ���ʼλ��ƫ����
    private Vector3 castEndPoint;   //����Ͷ��ʱ�յ�

    public GameObject ShootPoint;//�ڵ�����λ��

    protected override void Start()
    {
        base.Start();
        castEndPoint = new Vector3(5.3f, transform.position.y, transform.position.z);
    }

    public void fireEvent()
    {
        
        for (int i = 0; i < 3; i++)
        {
            // ʹ�����߼���������ϵĽ�ʬ
            RaycastHit2D[] hitResults =
                Physics2D.LinecastAll(transform.position + new Vector3(0, i * 0.8f - 0.8f, 0),
                castEndPoint + new Vector3(0, i * 0.8f - 0.8f, 0), LayerMask.GetMask("Zombie"));


            foreach (RaycastHit2D hitResult in hitResults)
            {

                // ��ȡ��ʬ���
                Zombie zombieGeneric = hitResult.transform.GetComponent<Zombie>();
                //Zombie zombie = hitResult.transform.GetComponent<Zombie>(); // ��ȡ Zombie



                if (zombieGeneric != null)
                {
                    if (zombieGeneric.pos_row >= row - 1 && zombieGeneric.pos_row <= row + 1)
                    {
                        // ѡ��һ������ӵ�����
                        int bulletIndex = Random.Range(0, bullets.Count);
                        GameObject bullet = Instantiate
                            (
                            bullets[bulletIndex],
                            ShootPoint.transform.position,
                            Quaternion.identity
                        );


                        bullet.GetComponent<ThrowBullet>().initialize(zombieGeneric, this, zombieGeneric.pos_row); // ���� Zombie
                        break;
                    }
                }
                //else if (zombie != null)
                //{
                //    if (zombie.pos_row >= row - 1 && zombie.pos_row <= row + 1)
                //    {
                //        {
                //            // ����������� Zombie ���͵Ľ�ʬ
                //            int bulletIndex = Random.Range(0, bullets.Count);
                //            GameObject bullet = Instantiate(
                //                bullets[bulletIndex],
                //                ShootPoint.transform.position,
                //                Quaternion.identity
                //            );


                //            bullet.GetComponent<ThrowBullet>().initialize(zombie, this, zombie.pos_row); // ���� Zombie

                //            audioSource.Play();
                //            break;
                //        }
                //    }
                //}
            }
        }
    }
}
