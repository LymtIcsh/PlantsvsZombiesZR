using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : Plant
{
    public int Attack;//爆炸攻击力
    private bool rise = false;
    protected bool explode = false;
    private int TimetoRise;//准备好需要时间
    public GameObject PotatoExplosion;
    //public GameObject[] zombieToAttack;
    protected override void Start()
    {
        base.Start();
        
        TimetoRise = Random.Range(13, 17);//13-17秒准备
        Invoke("Rise", TimetoRise);

        GetComponent<Animator>().SetBool("rise", false);

        GetComponent<Animator>().SetBool("flash", false);
    }

    public override void initialize(PlantGrid grid, string sortingLayer, int sortingOrder)
    {
        base.initialize(grid, sortingLayer, sortingOrder);
        this.transform.position -= new Vector3(0, 0.2f, 0);
    }

    protected virtual void Rise()
    {
        GetComponent<Animator>().SetBool("rise", true);

        GetComponent<Animator>().SetBool("flash", true);

        rise = true;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rise == false || explode) return;
        else if(collision.CompareTag("Zombie")){
            if (collision.GetComponent<Zombie>() != null && collision.GetComponent<Zombie>().pos_row == this.row) Explode();
        }
    }


    protected virtual void Explode()
    {
        explode = true;
        AudioManager.Instance.PlaySoundEffect(22);
        CameraShake.Instance.Shake(0.2f, 0.06f);
        Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.8f);//半径为0.8的圈
        foreach (Collider2D collider2D in array)
        {
            if (collider2D.CompareTag("Zombie"))
            {
                Zombie component = collider2D.GetComponent<Zombie>();
                if (component == null) return;
                if (component.pos_row == this.row)
                {
                    component.beAttacked(Attack,2,1);//无视二类护甲
                }
            }
        }
        GameObject potatoExplosion = Instantiate(PotatoExplosion, this.transform.position,Quaternion.identity);
        Destroy(potatoExplosion, 1f);
        Invoke("DelayDie", 0.5f);
       
    }

    protected virtual void DelayDie()
    {
        die(null,this.gameObject);
    }


}
