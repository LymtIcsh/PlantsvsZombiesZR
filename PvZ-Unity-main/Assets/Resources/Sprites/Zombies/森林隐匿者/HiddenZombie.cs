using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenZombie : ForestZombie
{
    //草丛僵尸
    private float HiddingTime = 120;
    private float Timer = 0;
    // Start is called before the first frame update
    public List<Zombie> zombieList;
    // Update is called once per frame
    protected override void Start() {
        base.Start();
        if (myAnimator.GetBool("Idle") == false) {

            InvokeRepeating("CreateZombie", 20f, 30f);
        }
    }

    protected override void Update()
    {
        this.buff.隐匿 = true;
      
        base.Update();
        if (Timer < HiddingTime)
        {
            Timer += Time.deltaTime;
        }
        else {
            myAnimator.SetBool("Hidding", false);//取消隐藏\
        }
    }

    protected void CreateZombie() { //于所在位置产生一个随机僵尸
        if (dying || !alive) return;

        GameObject randomZombie = ZombieManagement.instance.GenerateZombieInlevel();
        Vector3 vector3 = gameObject.transform.position;
        vector3.y = GameManagement.levelData.zombieInitPosY[pos_row];
        vector3.z = 0;
        GameObject spawnedZombie = Instantiate(randomZombie, vector3, Quaternion.identity, GameManagement.instance.zombieManagement.transform);
        spawnedZombie.GetComponent<Zombie>().pos_row = this.pos_row;
        spawnedZombie.GetComponent<Zombie>().setPosRow(this.pos_row);//设置图层
    }

  
}
