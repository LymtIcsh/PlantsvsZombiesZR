using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 撑杆跳子物体翻越判定
/// </summary>
public class PoleVaultingDetector : MonoBehaviour
{
    [FormerlySerializedAs("已经翻越")] [Header("已经翻越")]
    public bool hasVaulted = false;
    public int row;
    public Plant plant;
    public PlantGrid plantGrid;//防止植物被移走，在撑杆跳僵尸处更新最新植物

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!hasVaulted && collision.tag == "Plant")
        {
            plant = collision.GetComponent<Plant>();
            if(plant != null && plant.row == row && plant._plantType != PlantType.GroundHuggingPlants)
            {
                plantGrid = plant.myGrid;
                GetComponentInParent<Zombie>().CanBite = false;
                hasVaulted = true;
                GetComponentInParent<Animator>().SetBool("Fly", true);
                GetComponentInParent<Animator>().SetBool("Walk", true);
            }
            
        }
    }

}
