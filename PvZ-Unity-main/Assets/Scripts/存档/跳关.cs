//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class 跳关 : MonoBehaviour
//{
//    public GameObject 跳关界面;

//    public void Start()
//    {
//        跳关界面.SetActive(false);
//        if(!LevelManagerStatic.IsLevelCompleted(1)
//            ||!LevelManagerStatic.IsLevelCompleted(2) 
//            ||!LevelManagerStatic.IsLevelCompleted(3)
//            ||!LevelManagerStatic.IsLevelCompleted(4)
//            ||!LevelManagerStatic.IsLevelCompleted(5)
//            )
//        {
//            跳关界面.SetActive(true);
//        }
//    }
//    public void 取消()
//    {
//        跳关界面.SetActive(false);
//    }
//    public void 确定()
//    {
//        LevelManagerStatic.SetLevelCompleted(1);
//        LevelManagerStatic.SetLevelCompleted(2);
//        LevelManagerStatic.SetLevelCompleted(3);
//        LevelManagerStatic.SetLevelCompleted(4);
//        LevelManagerStatic.SetLevelCompleted(5);
//        跳关界面.SetActive(false);
//        string currentSceneName = SceneManager.GetActiveScene().name;
//        SceneManager.LoadScene(currentSceneName);
//        Time.timeScale = 1;

//    }
//}
