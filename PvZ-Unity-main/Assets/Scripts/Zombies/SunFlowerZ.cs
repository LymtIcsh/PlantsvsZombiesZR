using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerZ : Zombie
{
    public GameObject SunPrefabZ;//僵尸阳光
    public GameObject SunPrefab;
    public GameObject CreatePoint;//生成位置
    Transform sunManagement;   //太阳管理器对象Tranform组件，为所有太阳父对象
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
    private void createToTruth()//用于切换动画状态
    {
        gameObject.GetComponent<Animator>().SetBool("create", true);
    }
    protected virtual void CreateSun() {

        gameObject.GetComponent<Animator>().SetBool("create", false);
        //播放音效
        //audioSource.Play();

        if(!debuff.Charmed)
        {
            //生成太阳
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
