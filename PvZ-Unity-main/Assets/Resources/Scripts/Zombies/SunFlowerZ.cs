using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerZ : Zombie
{
    public GameObject SunPrefabZ;//��ʬ����
    public GameObject SunPrefab;
    public GameObject CreatePoint;//����λ��
    Transform sunManagement;   //̫������������Tranform�����Ϊ����̫��������
    float createDuration = 24f;
    protected override void Start()
    {
        base.Start();
        
        GameObject sunManage = GameManagement.instance.sunManagement;

        if (sunManage != null)
        {
            sunManagement = sunManage.GetComponent<Transform>();
        }
        if (myAnimator.GetBool("Idle")==false) {

            Invoke("createToTruth", 5f);
        }
    }
    private void createToTruth()//�����л�����״̬
    {
        gameObject.GetComponent<Animator>().SetBool("create", true);
    }
    protected virtual void CreateSun() {

        gameObject.GetComponent<Animator>().SetBool("create", false);
        //������Ч
        //audioSource.Play();

        if(!debuff.Charmed)
        {
            //����̫��
            Instantiate(SunPrefabZ, CreatePoint.transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
        }
        else
        {
            Instantiate(SunPrefab, CreatePoint.transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
        }
        

      
        if (debuff.Deceleration > 0) {
            createDuration = 48f;
        }
        Invoke("createToTruth", createDuration);

    }
}
