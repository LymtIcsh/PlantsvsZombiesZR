using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrophiesWin : MonoBehaviour
{
    public ManagedAudioSource backgroundAudio;   //�������ֵĲ������
    public Animator animator;
    public bool isCollect = false;

    private void Start()
    {
        if (GameManagement.levelData.MustLost == true) {
            OnMouseDown();
        }
    }
    void OnMouseDown()
    {
        if(!isCollect)
        {
            isCollect = true;
            Debug.Log(gameObject.name);
            animator.SetBool("Collect", true);
        }
        
    }

    public void IdleΪ��()
    {
        animator.SetBool("Idle", true);
    }
   public void win_real()
    {
        //������Ч
        backgroundAudio.Stop();
        AudioManager.Instance.PlaySoundEffect(36);
    }

    public void exitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ChooseGame");
    }
}
