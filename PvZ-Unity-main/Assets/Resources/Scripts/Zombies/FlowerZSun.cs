using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerZSun : SunBase
{
    // Start is called before the first frame update
    protected Vector3 highestPoint, lowestPoint;

    //0:上升，1:下降，2:停止
    int moveState = 0;
    public int sunType = 0;//0为普通阳光，1为小阳光


    protected override void Start()
    {
        if (sunType == 0)
        {
            if (GameManagement.GameDifficult <= 3)
            {
                sunNumber = -25;
            }
            else
            {
                sunNumber = -50;
            }
        }
        else if (sunType == 1)
        {
            if (GameManagement.GameDifficult <= 3)
            {
                sunNumber = -5;
            }
            else
            {
                sunNumber = -10;
            }
        }

        base.Start();

        float xOffset = Random.Range(-0.4f, 0.4f);
        highestPoint = transform.position +
                        new Vector3(xOffset, 0.5f, 0);
        lowestPoint = transform.position +
                        new Vector3(xOffset, -0.5f, 0);
    }

    public override void drop()
    {
        if (moveState == 0)
        {
            transform.Translate((highestPoint - transform.position) * 2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, highestPoint) < 0.05f) moveState = 1;
        }
        else if (moveState == 1)
        {
            transform.Translate((lowestPoint - transform.position) * 2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, lowestPoint) < 0.05f) moveState = 2;
        }
        else
        {
            dropState = false;
            bePickedUp();//僵尸的阳光自动拾取
        }
    }
}
