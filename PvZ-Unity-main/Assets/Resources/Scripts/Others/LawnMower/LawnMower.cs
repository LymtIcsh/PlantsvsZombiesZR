using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : MonoBehaviour
{
    private Animator animator;  // Animator 组件
    public int row;
    public BoxCollider2D myCollider;
    private bool canMove;
    public float speed = 0.1f;  // 控制移动速度
    private void Start()
    {
        animator = GetComponent<Animator>();
        canMove = false;
        animator.enabled = false;  // 禁用 Animator 组件
    }

    private void Update()
    {
        if(canMove)
        {
            
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.GetComponent<Zombie>() != null)
        //{
        //    if (collision.tag == "Zombie" && collision.GetComponent<Zombie>().pos_row == row)
        //    {
        //        canMove = true;

        //        collision.GetComponent<Zombie>().die();
        //    }
        //}
        Zombie z = collision.GetComponent<Zombie>();
        if (z != null)
        {
            if ((collision.tag == "Zombie"|| collision.tag == "Tombstone") && z.pos_row == row && !z.debuff.Charmed)
            {
                
                if(!z.dying)
                {
                    if(!canMove)
                    {
                        canMove = true;
                        AudioManager.Instance.PlaySoundEffect(28);
                        animator.enabled = true;
                    }
                    
                    z.LawnMowerDie();
                }
                
            }
        }


        if (collision.tag == "LawnMowerDisappearLine")
        {
            Destroy(gameObject);
        }
    }


}
