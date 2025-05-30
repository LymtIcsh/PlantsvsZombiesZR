using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SymbioticForestTorchWood : Plant
{
    public GameObject fireParticleSystem;
    public GameObject diamonPea;
    public GameObject FlowersunPrefab;
    private int times = 20;//可以产阳光的次数
    protected override void Start()
    {
        base.Start();
        fireParticleSystem.SetActive(false);
        int i = 0;
        foreach(GameObject plant in PlantManagement.场上植物.ToList())
        {
            Plant plantS = plant.GetComponent<Plant>();
            if (plantS != null)
            {
                if(plantS.plantStruct.id == plantStruct.id)
                {
                    i++;
                }
            }
        }
        if(i >= 2)
        {
            Resonance();
        }
    }

    public void Resonance()
    {
        fireParticleSystem.SetActive(true);
        AudioManager.Instance.PlaySoundEffect(56);
        foreach (GameObject plant in PlantManagement.场上植物.ToList())
        {
            Plant plantS = plant.GetComponent<Plant>();
            if (plantS != null)
            {
                plantS.recover(plantS.最大血量 / 5);
                WallNut wallNut = plant.GetComponent<WallNut>();
                if (wallNut != null)
                {
                    plantS.increaseMaxHP(plantS.最大血量 / 10);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if ((collision.CompareTag("FirePea")|| collision.CompareTag("Pea")) && collision.GetComponent<StraightBullet>().row == row)
        {
            times--;

            forestSlider.DecreaseSliderValueSmooth(2);
            
            GameObject diamondpea = Instantiate(diamonPea, collision.transform.position, Quaternion.identity);

            diamondpea.GetComponent<StraightBullet>().initialize(row);
            diamondpea.GetComponent<StraightBullet>().hurt += 最大血量 / 100;

            Destroy(collision.gameObject);

            if(times > 0)
            {
                GameObject flowersunPrefab = Instantiate(FlowersunPrefab, transform.position, Quaternion.Euler(0, 0, 0), GameManagement.instance.sunManagement.transform);
            }
            
        }
    }
}
