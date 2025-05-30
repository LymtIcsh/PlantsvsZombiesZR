using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZombiePendant : MonoBehaviour
{
    //public GameObject Pendant;
    //private ParticleSystem ps;


    //private ParticleSystem.Particle[] particles;

    //void Start()
    //{
    //    if(!GameManagement.isPerformance)
    //    {
    //        ps = GetComponent<ParticleSystem>();
    //        particles = new ParticleSystem.Particle[ps.main.maxParticles];  // 预分配粒子数组
    //    }
    //    else
    //    {
    //        // 生成物体
    //        Instantiate(Pendant, gameObject.transform.position, Quaternion.identity);
    //    }

        
    //}

    //void Update()
    //{
    //    if(!GameManagement.isPerformance)
    //    {
    //        // 获取当前粒子的所有信息
    //        int particleCount = ps.GetParticles(particles);

    //        for (int i = 0; i < particleCount; i++)
    //        {
    //            // 获取每个粒子的世界坐标
    //            Vector3 particleWorldPosition = ps.transform.TransformPoint(particles[i].position);

    //            // 确保粒子还没消失，且生成物体
    //            if (particles[i].remainingLifetime <= 0)
    //            {
    //                // 生成物体
    //                Instantiate(Pendant, particleWorldPosition, Quaternion.identity);
    //            }
    //        }
    //    }
        
    //}

}
