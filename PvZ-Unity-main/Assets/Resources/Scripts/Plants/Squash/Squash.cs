using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plant
{
    public BoxCollider2D collider_Idle;   //����ʱ̽�⽩ʬ����������ײ��
    public Collider2D collider_attack;   //ѹ�⽩ʬ������ײ��

    public GameObject lockedZombie;   //�����Ľ�ʬ


    bool jumpingUp = false;   //�Ƿ�������
    bool jumpingDown = false;   //�Ƿ������
    public bool idle = true;
    Vector3 speed_jumpUp;   //������ٶ�
    Vector3 speed_jumpDown;   //���µ��ٶ�

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if(jumpingUp == true)
        {
            transform.Translate(speed_jumpUp * Time.deltaTime);
        }
        else if(jumpingDown == true)
        {
            transform.Translate(speed_jumpDown * Time.deltaTime);
        }
    }

    public override int beAttacked(int hurt, string form, GameObject zombieObject)
    {
        if(idle == true)
        {
            Ѫ�� -= hurt;
            ����Ѫ���ı�();
            if (Ѫ�� <= 0)
            {
                die(form, gameObject);
            }
        }
        return Ѫ��;
    }

    public void hmm()
    {
        AudioManager.Instance.PlaySoundEffect(23);
    }

    public void jumpUp()
    {
        Debug.Log("up");
        Vector3 peak = lockedZombie.transform.position + new Vector3(0, 1.3f, 0);
        Vector3 destination = new Vector3(lockedZombie.transform.position.x, transform.position.y, 0);
        speed_jumpUp = (peak - transform.position) / 0.133f;   //���𶯻�0.133��
        speed_jumpDown = (destination - peak) / 0.13f;   //���¶���0.117��
        transform.Find("Shadow").gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().sortingLayerName = "PlantBullet";
        //�ѹ���ײ��ʧЧ����ֹ���������ֱ��Ե�
        GetComponent<BoxCollider2D>().enabled = false;
        jumpingUp = true;
    }

    public void reachPeak()
    {
        jumpingUp = false;
    }

    public void jumpDown()
    {
        jumpingDown = true;
    }

    public void reachDest()
    {
        collider_attack.enabled = false;
        jumpingDown = false;
    }

    public void squashZombie()
    {
        collider_attack.enabled = true;
        AudioManager.Instance.PlaySoundEffect(25);
    }

    public void disappear()
    {
        die("",gameObject);
    }

    //�ѹϺ���ʱ���������٣�����ԭ�ٶ����������Զ
    public override void cold()
    {
        if (state == PlantState.Normal)
        {
            state = PlantState.Cold;
            AudioManager.Instance.PlaySoundEffect(24);
            GetComponent<SpriteRenderer>().color = new Color(0.33f, 0.54f, 1f);
            Invoke("coldHurt", 1f);
        }
    }

    protected override void intensify_specific()
    {
        Vector2 colliderIdleSize = collider_Idle.size;
        collider_Idle.size = new Vector2(colliderIdleSize.x * 2, colliderIdleSize.y);
    }

    protected override void cancelIntensify_specific()
    {
        Vector2 colliderIdleSize = collider_Idle.size;
        collider_Idle.size = new Vector2(colliderIdleSize.x / 2, colliderIdleSize.y);
    }
}
