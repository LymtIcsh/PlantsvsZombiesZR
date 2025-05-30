using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    public float speed = 4;   //子弹速度
    public int hurt;  //子弹伤害

    protected bool boomState = false;  //子弹是否已爆炸
    public int row;
    public bool HasShatterEffectPrefab = false;

    public int Camp = 0;//0为植物，1为僵尸，2中立

    public int peaType;//0为豌豆，1为火焰豌豆
    public bool canFreeze;//是否可以减速敌方
    public GameObject shatterEffectPrefab;

    public bool 可以施加中毒状态 = false;
    public int 附加中毒层数;
    // Update is called once per frame
    protected virtual void Update()
    {

        if (!boomState)
        {
            if (Camp!=1)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (Camp == 0)//植物方的子弹
        {
            if (collision.CompareTag("Zombie"))
            {
                // 判断是否是 Zombie 类型
                Zombie zombieGeneric = collision.GetComponent<Zombie>();
              
                if (zombieGeneric != null && row == zombieGeneric.pos_row && zombieGeneric.buff.隐匿==false && !zombieGeneric.debuff.魅惑) // 如果是 Zombie
                {
                    if (boomState == false)
                    {
                       
                        attack(zombieGeneric);  // 改为调用 attack(Zombie)
                    }
                }
               
            }
        }
        else
        {
            if (collision.tag == "Plant"&&row== collision.GetComponent<Plant>().row&& collision.GetComponent<Plant>().植物类型 == PlantType.正常植物)
            {
                
                if(peaType == 0)
                {
                    System.Random random = new System.Random();
                    System.Random rand = new System.Random();
                    int result = rand.Next(2, 4);
                    AudioManager.Instance.PlaySoundEffect(result);
                }
                else if (peaType == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(30);
                }

                Plant plant = collision.GetComponent<Plant>();
                plant.beAttacked(hurt, "BeHit", null);
                boom();
            }

        }
        if (collision.CompareTag("BulletDisappearLine"))
        {
            Destroy(gameObject);
        }
    }



    protected virtual void boom()
    {
        boomState = true;

        if (HasShatterEffectPrefab && !GameManagement.isPerformance)
        {
            if (!GameManagement.isPerformance)
            {
                GameObject shatterEffect = Instantiate(shatterEffectPrefab, transform.position, Quaternion.identity);
            }
            
        }


        disappear();
    }

    //protected virtual void attack(Zombie target)
    //{
    //    //播放音效
    //    target.playAudioOfBeingAttacked();
    //    if (canFreeze)
    //    {
    //        target.beFrozeAttacked();
    //    }
    //    //僵尸被攻击
    //    target.beAttacked(hurt, true);
    //    if(可以施加中毒状态)
    //    {
    //       target.beParasiticed();
    //        GameManagement.forestSlider.DecreaseSliderValueSmooth(1);
    //    }

    //}

    protected virtual void attack(Zombie target)
    {
        if (canFreeze)
        {
            target.附加减速();
        }
        
        //僵尸被攻击
        target.beAttacked(hurt, 1, 1);

        if (可以施加中毒状态)
        {
            target.附加中毒(附加中毒层数);
            GameManagement.instance .forestSlider.DecreaseSliderValueSmooth(1);
        }
        boom();
    }

    protected void disappear()
    {
        Destroy(gameObject);
    }

    public void initialize(int row)
    {
        this.row = row;
    }

    public void initialize(int row, int hurt)
    {
        this.row = row;
        this.hurt = hurt;
    }
}
