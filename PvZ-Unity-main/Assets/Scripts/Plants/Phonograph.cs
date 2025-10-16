using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phonograph : Plant
{
    protected override void Start()
    {
        base.Start();
        SetAchievement.SetAchievementCompleted("ÈçÌıÏÉÀÖ¶úÔİÃ÷");
        GameManagement.instance.lockMusic = true;
        GameManagement.instance.background.GetComponent<BGMusicControl>().changeMusicSmoothly("Music_WWR");
        GameManagement.instance.background.GetComponent<BGMusicControl>().isClimax = false;
    }

    public override void AfterDestroy()
    {
        base.AfterDestroy();
        GameManagement.instance.lockMusic = false;
        GameManagement.instance.background.GetComponent<BGMusicControl>().changeMusicSmoothly("Music" + GameManagement.levelData.backgroundSuffix);
        GameManagement.instance.background.GetComponent<BGMusicControl>().isClimax = false;
    }
}
