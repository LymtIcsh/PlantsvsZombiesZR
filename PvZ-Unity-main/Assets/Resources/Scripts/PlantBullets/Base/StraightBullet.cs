using UnityEngine;
using UnityEngine.Serialization;

public class StraightBullet : MonoBehaviour
{
    public float speed = 4;   //�ӵ��ٶ�
    public int hurt;  //�ӵ��˺�

    protected bool boomState = false;  //�ӵ��Ƿ��ѱ�ը
    public int row;
    public bool HasShatterEffectPrefab = false;

    public int Camp = 0;//0Ϊֲ�1Ϊ��ʬ��2����

    public int peaType;//0Ϊ�㶹��1Ϊ�����㶹
    public bool canFreeze;//�Ƿ���Լ��ٵз�
    public GameObject shatterEffectPrefab;

    [FormerlySerializedAs("����ʩ���ж�״̬")] [Header("����ʩ���ж�״̬")]
    public bool _canPoisoningEffectApplied = false;
    [FormerlySerializedAs("�����ж�����")] [Header("�����ж�����")]
    public int _additionalPoisoningLevels;
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
        if (Camp == 0)//ֲ�﷽���ӵ�
        {
            if (collision.CompareTag("Zombie"))
            {
                // �ж��Ƿ��� Zombie ����
                Zombie zombieGeneric = collision.GetComponent<Zombie>();
              
                if (zombieGeneric != null && row == zombieGeneric.pos_row && zombieGeneric.buff.Stealth==false && !zombieGeneric.debuff.Charmed) // ����� Zombie
                {
                    if (boomState == false)
                    {
                       
                        attack(zombieGeneric);  // ��Ϊ���� attack(Zombie)
                    }
                }
               
            }
        }
        else
        {
            if (collision.tag == "Plant"&&row== collision.GetComponent<Plant>().row&& collision.GetComponent<Plant>()._plantType == PlantType.NormalPlants)
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
    //    //������Ч
    //    target.playAudioOfBeingAttacked();
    //    if (canFreeze)
    //    {
    //        target.beFrozeAttacked();
    //    }
    //    //��ʬ������
    //    target.beAttacked(hurt, true);
    //    if(����ʩ���ж�״̬)
    //    {
    //       target.beParasiticed();
    //        GameManagement.forestSlider.DecreaseSliderValueSmooth(1);
    //    }

    //}

    protected virtual void attack(Zombie target)
    {
        if (canFreeze)
        {
            target.ApplyDeceleration();
        }
        
        //��ʬ������
        target.beAttacked(hurt, 1, 1);

        if (_canPoisoningEffectApplied)
        {
            target.ApplyPoison(_additionalPoisoningLevels);
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
