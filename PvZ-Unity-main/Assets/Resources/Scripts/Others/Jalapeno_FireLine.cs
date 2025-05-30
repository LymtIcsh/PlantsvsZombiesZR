using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jalapeno_FireLine : MonoBehaviour
{
    //辣椒火线
    public int Camp;//阵营
    public int Attack;
    public int Row;
    public GameObject Jalapeno_Fire;
    //private List<Plant> plants;
    //private List<Zombie> zombieGenerics;

    private void Start()
    {
        Invoke("Destroy",1f);

        Explosion(Row);
    }

    protected void Explosion(int row) {
        if (row < 0 || row >= GameManagement.levelData.rowCount) return;
        else {
            List<PlantGrid> grids = PlantingManagement.instance.GetRowGrids(row);
            foreach (PlantGrid grid in grids)
            {//在指定位置生成火焰
                GameObject jalapeno_Fire = Instantiate(Jalapeno_Fire, grid.transform.position, Quaternion.identity);
                jalapeno_Fire.transform.parent = this.transform;
            }
            grids.Clear();
        }

      
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (Camp == 0 && collision.CompareTag("Zombie")) {//植物方火焰 
            Zombie zombie = collision.GetComponent<Zombie>();
            if (zombie!=null&&zombie.pos_row == this.Row && !zombie.debuff.魅惑)
            {
                //zombieGenerics.Add(zombie);
                zombie.beAttacked(Attack, 1, 4);
            }
        }
        if (Camp == 1 && collision.tag == "Plant") {
            Plant plant = collision.GetComponent<Plant>();
            if (plant != null && plant.row == this.Row) {

                // plants.Add(plant);
                plant.beAttacked(Attack, null, null);
            }        
        }
    }

    void Destroy() {
        Destroy(gameObject);
    }
}
