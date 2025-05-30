using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLevel : MonoBehaviour
{
    public CrazyDave crazyDave;   //疯狂戴夫的脚本组件
    public SpeechBubble peaSpeechBubble;    //豌豆对话框
    public SpeechBubble daveSpeechBubble;   //疯狂戴夫对话框
    public int LevelNumber;

    int count = 1;  //对话计数，当前是第几条对话
                    // Start is called before the first frame update
                    //void Update()
                    //{

    //    switch(LevelNumber)
    //    {
    //        case 1:LevelDialog1(); break;
    //        case 2: LevelDialog2(); break;
    //        case 3: LevelDialog3(); break;
    //        case 4: LevelDialog4(); break;
    //        case 5: LevelDialog5(); break;
    //        case 6: LevelDialog6(); break;
    //        case 7: LevelDialog7(); break;
    //        case 8: LevelDialog8(); break;
    //        case 9: LevelDialog9(); break;
    //        case 10: LevelDialog10(); break;
    //        case 12: LevelDialog12(); break;
    //        case 13: LevelDialog13(); break;
    //        case 51: LevelDialog51(); break;
    //        case 52: LevelDialog52(); break;
    //        case 53: LevelDialog53(); break;
    //        case 54: LevelDialog54(); break;
    //        case 55: LevelDialog55(); break;
    //        case 56: LevelDialog56(); break;
    //        case 57: LevelDialog57(); break;
    //        case 58: LevelDialog58(); break;
    //        case 59: LevelDialog59(); break;
    //        case 241: LevelDialog241(); break;
    //        case 242: LevelDialog242(); break;
    //        case 243: LevelDialog243(); break;
    //        case 244: LevelDialog244(); break;
    //        case 245: LevelDialog245(); break;
    //        case 246: LevelDialog246(); break;
    //        case 247: LevelDialog247(); break;
    //        case 253: LevelDialog253(); break;
    //        default: 
    //            GameManagement.instance.GetComponent<GameManagement>().awakeAll();
    //            count++; 
    //            gameObject.SetActive(false); 
    //            break;
    //    }
    //}
    void Update()
    {
        string methodName = "LevelDialog" + LevelNumber;
        var method = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            GameManagement.instance.GetComponent<GameManagement>().awakeAll();
            count++;
            gameObject.SetActive(false);
        }
    }



    public void activeTofalse()
    {
        
        gameObject.SetActive(false);
    }

    public void LevelDialog1()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("欢迎来到自然群系版冒险模式");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("这部分的关卡大多为复刻原版内容，并有更深度的机制教学");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("如果想体验该版本特色，请游玩环境模式");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("冒险模式仅供解锁植物");
                    count++;
                    break;
                case 5:
                    crazyDave.smallTalk("游戏愉快！");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog2()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("向日葵是重要的阳光获取单元！");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("可以在设置界面调整难度。" + "\n" + "（默认难度2，推荐调整至难度2-3）");
                    count++;
                    break;
                case 3:
                    crazyDave.smallTalk("阳光受到难度设置的影响。");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("难度1-难度2时，每个普通阳光价值50个阳光点。" + "\n" + "难度3-难度5时，每个普通阳光价值25个阳光点。");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog3()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.talk("自然群系版的僵尸拥有减伤效果" + "\n" + "（详情见 主界面>选项 右侧说明）");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("对于僵尸本体，减伤效果会叠加两次。" + "\n" + "（详情见 主界面>选项 右侧说明）");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog4()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.talk("在局内可以用手套（快捷键2）移动植物。");
                    count++;
                    break;
                case 2:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog5()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("每大波僵尸仅会在场上僵尸清零后刷新。");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("树桩类植物可以强化并反弹敌方子弹，反弹后的敌方子弹将转化为我方子弹，反弹会扣除植物生命值");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog6()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("在自然群系版中，植物可以根据环境进行变种。");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("让我们用一棵原始树桩作演示");
                    count++;
                    break;
                case 3:
                    PlantGrid allObjects = FindFirstObjectByType<PlantGrid>(); // 查找所有 PlantGrid 对象，包括非激活的
                    allObjects.plantByGod("Wood");
                    crazyDave.talk("现在种下一棵原始树桩");
                    count++;
                    break;
                case 4:
                    PlantGrid Objects = FindFirstObjectByType<PlantGrid>();
                    if(Objects.nowPlant != null)
                    {
                        PlantManagement.RemovePlant(Objects.nowPlant);
                        Destroy(Objects.nowPlant);
                    }
                    Objects.plantByGod("TorchWood");
                    
                    crazyDave.talk("它变成了火炬树桩！" + "\n" + "这就是环境变种机制。");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("原始树桩在白天-草地环境种植，自动变种成了火炬树桩");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("原始树桩为基础植物，火炬树桩为变种植物。");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("基础植物在不同环境拥有不同的变种植物。");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("通过环境变种来获得变种植物不会消耗多余的阳光");
                    count++;
                    break;
                case 9:
                    crazyDave.talk("大多数的草地植物都是基础植物，原始树桩较为特殊");
                    count++;
                   
                    break;
                case 10:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog7()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    peaSpeechBubble.showDialog("为什么我看到了鸡？");                   
                    count++;
                    break;
                case 2:                
                    crazyDave.talk("鸡在移动时有概率无视植物");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog8()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("刚才的蓝色僵尸叫“胖哥哥”");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("他可以释放保龄球在植物间滚动，造成大额伤害。");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("地刺可以立刻扎破胖哥哥的保龄球");
                    count++;
                    break;
                case 4:
                    crazyDave.smallTalk("一击毙命！");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog9()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("鸡贼僵尸收到攻击或啃咬时，会释放大量的鸡");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("如果血量较大时，受到攻击不会释放鸡");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("在你的实力不够时，不要攻击他");
                    count++;
                    break;
                case 4:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog10()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("【光合议会】收割光的权柄，【荧光菌丝】蚕食能量的脉络。");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("草地黑夜植物和大部分草地白天植物一样，同属于基础植物");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("在夜晚，会掉落三种不同的阳光");
                    count++;
                    break;
                case 4:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog12()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("向日葵僵尸在场时会不断扣除你的阳光");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("阳光可以被扣为负数");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog13()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("大喷菇可以穿透敌人的二类防具，如铁网门等");
                    count++;
                    break;
                case 2:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog14()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("大嘴花可以吞噬大部分的僵尸");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("当遇到铁桶铁网门或橄榄球僵尸时，可以尝试用大嘴花吞噬他们");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog15()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("魅惑菇可以使一只僵尸为你作战！");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("可以尝试魅惑鸡贼僵尸，让他生产魅惑鸡来组成你的魅惑大军！");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog16()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("毁灭菇可以毁灭一大片僵尸");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("但是在被种植后会留下存在180秒的坑洞");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog51()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    peaSpeechBubble.showDialog("欢迎来到森林环境！");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("不同环境的环境特色是自然版的核心玩法");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("大部分基础植物在不同环境种植可以获得变种植物");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("在森林环境种植原始树桩，可以获得森林树桩");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("森林树桩将子弹强化为森林子弹，每次强化扣除自身一定血量");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("当森林树桩强化火焰豌豆子弹时，可以生产阳光");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("森林子弹可以为敌方施加毒素");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("在森林环境下，我方和敌方的行动可以积攒森林值和污染森林值");
                    count++;
                    break;
                case 9:
                    crazyDave.talk("当（污染）森林值达到一定限度后，会触发森林特殊效果");
                    count++;
                    break;

                case 10:
                    crazyDave.talk("我方积攒森林值，可以为敌方全体附加大量毒素");
                    count++;
                    break;
                case 11:
                    crazyDave.talk("敌方积攒污染森林值，可以使敌方全体进入狂暴状态");
                    count++;
                    break;
                case 12:
                    crazyDave.talk("难度不低于三时，每次触发效果会使敌方的生命值和最大生命值都变为二倍");
                    count++;
                    break;
                case 13:
                    crazyDave.talk("敌方的狂暴状态可以通过毒属性攻击解除");
                    count++;
                    break;
                case 14:
                    crazyDave.talk("积攒（污染）森林值的速度受到难度设置的影响");
                    count++;
                    break;
                case 15:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog52()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("森林向日葵有更强的阳光生产能力和治疗能力");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("森林向日葵在产生阳光的同时治疗附近的所有植物");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("阳光小于1000时产生阳光，半径1格内每有一个森林向日葵额外产生一个阳光");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("阳光大于1000时产生阳光，会根据植物生命值提高治疗量，但会消耗少量阳光");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("森林植物的各种行为都可以积攒森林值。");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("比如森林向日葵生产阳光、森林树桩强化子弹、森林子弹命中敌人...");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("所有的森林僵尸在出场一段时间后都有概率死亡。");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("当森林僵尸在难度4-5自动死亡时，将产生特殊负面效果。");
                    count++;
                    break;
                case 9:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog53()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("森林坚果受到治疗时，会大幅提高治疗量");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("森林坚果在被啃食时，每次为僵尸附加一层毒素");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("每层毒素每两秒造成一次伤害，在被附加六秒后解除");
                    count++;
                    break;
                case 4:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog54()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("森林铁网门僵尸的铁网门受到攻击后会大幅积累污染森林值");
                    count++;
                    break;
                case 2:
                    crazyDave.smallTalk("通过地刺进行穿透攻击吧");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("森林地刺每次攻击可以引爆敌方所有毒素，造成大额伤害");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("引爆伤害根据敌方最高生命值的百分比和毒素层数计算");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("但是森林地刺自己不能附加中毒效果，需要配合使用");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog55()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("在森林环境中，每隔一段时间在场上随机出现森林灌木丛（中立）。");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("灌木丛免疫大部分直射子弹伤害，只可被森林豌豆攻击");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("进入灌木丛中的僵尸拥有「隐匿」效果，不会被大多数植物锁定和大多数子弹攻击。");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("灌木丛每经过一段时间会随机生成一个僵尸");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("灌木丛死亡后，大幅降低污染森林值");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("森林撑杆跳僵尸在进入灌木丛时会立刻进入狂暴状态");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }
    public void LevelDialog56()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("森林隐匿僵尸把自己变身为了灌木丛");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("森林隐匿僵尸自身带有「隐匿」效果");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("并且每隔一段时间会在自身位置召唤一个僵尸");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("橡木弓箭手可以无视「隐匿」效果攻击敌人");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("并且每次攻击可以根据敌方毒素层数造成大额伤害");
                    count++;
                    break;
                case 7:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog57()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("毒烟小喷菇是小喷菇的森林变种");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("荧光蘑菇是阳光菇的森林变种");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("荧光蘑菇与周围的自己通过光照建立联系");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("当生产阳光时，它根据周围荧光蘑菇的数量提高产量");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("毒烟小喷菇和荧光蘑菇在死亡后产生污染区，为敌方附加大量毒素");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("幽灵会在后半场随机出现，永久带有「隐匿」效果");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog58()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("毒烟大喷菇是大喷菇的森林变种");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("毒烟大喷菇是极其强力的植物");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("大喷菇类植物可以攻击到带有「隐匿」效果的单位");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("毒烟大喷菇攻击为敌方附加毒素");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("在死亡后产生污染区，为敌方附加大量毒素并引爆");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("草丛制造者在残血后制造森林草丛，躲在草丛后恢复生命值");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("森林草丛被攻击时会积累大量污染森林值");
                    count++;
                    break;
                case 9:
                    crazyDave.talk("森林草丛视作敌方单位");
                    count++;
                    break;

                case 10:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog59()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("在污染环境的部分种植格子种植时，植物不会变种");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("污染模式下的植物会根据生命值上限持续受到伤害");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("木壳坚果和完全污染僵尸都在森林都拥有三层「坚韧」");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("当受到敌方攻击后，每层坚韧可转化为50点护甲");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("木壳坚果受到攻击后，向八个方向发射木刺子弹");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("每个子弹造成少量伤害，附加一层毒素");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("木壳坚果受到的治疗量大幅降低");
                    count++;
                    break;
                case 9:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog241()
    {
        //点击鼠标左键，进入下一事件
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("随机礼盒可以产生随机植物！");
                    count++;
                    break;
                case 2:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    //public void LevelDialog242()
    //{
    //    //点击鼠标左键，进入下一事件
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        switch (count)
    //        {
    //            case 1:
    //                crazyDave.gameObject.SetActive(true);
    //                crazyDave.talk("欢迎来到冰雪关卡前瞻！");
    //                count++;
    //                break;
    //            case 2:
    //                crazyDave.talk("此关卡为冰雪关卡的基础植物Cost。");
    //                count++;
    //                break;
    //            case 3:
    //                crazyDave.leave();
    //                GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
    //                Invoke("activeTofalse", 1.5f);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    public void LevelDialog243()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("排山倒海模式下手套没有CD");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("排山倒海模式下种植非紫卡植物时将同时在整列种植");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("双发二爷在愤怒后会发射穿透性子弹，每隔一段时间发射燃烧整行的火焰子弹");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog244()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("游玩树桩的梦想模式前请先游玩冒险和环境模式，了解火炬与森林树桩");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("自然版中树桩受到敌方可强化子弹攻击后，会反弹子弹对应的强化子弹");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("树桩的梦想模式下，树桩反弹子弹不会扣除自身生命值");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("点击WASD或虚拟按键可移动树桩");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("点击鼠标右键或Q键或虚拟键盘可切换树桩");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("<color=red>请注意，该模式下经过火炬树桩反弹的子弹会大幅增加子弹伤害</color>");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog245()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("火焰豌豆僵尸发射可被森林树桩反弹的火焰豌豆");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("森林树桩反弹火焰豌豆时，将生产阳光");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("阳光达到1000后可消耗1000阳光释放技能");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("阳光充足后点击鼠标中键或E键或虚拟按键即可释放技能");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("火炬树桩可以释放一行的火焰，森林树桩可以触发弱化后的森林祝福");
                    count++;
                    break;
                case 7:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog246()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("双发射手二爷在报纸掉落前不会攻击");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("可以使用其它僵尸的子弹或技能攻击他");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("双发射手二爷报纸掉落后发射超级火焰子弹，可被森林树桩反弹");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("或许他还可以发射出来一些离谱的东西？");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog247()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("此模式为树桩梦想娱乐模式，性能开销较大");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("娱乐模式下初始拥有大量阳光");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("娱乐模式只会出现机枪射手僵尸");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("机枪射手僵尸的头盔视作一类防具");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog253()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("炸！炸！炸！");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("狂轰滥炸模式下土豆雷可以无限爆炸");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("使用WASD键或虚拟按键移动土豆雷");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("僵尸的速度将根据难度设置提升");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog201()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("欢迎来到自然版与嫁接版的联动关卡！");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("通过这个关卡，你将会获得来自异世界的辣椒机枪射手");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("辣椒机枪射手发射类似于火焰豌豆的子弹，死亡后产生单格火焰灰烬");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("联动关卡内不进行梯度出怪，且会出现大部分僵尸");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("运用我给你选出的卡牌与他们战斗！");
                    count++;
                    break;
                case 7:
                    crazyDave.smallTalk("加油吧！");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog202()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("欢迎来到自然版与杂交版的联动关卡！");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("关卡内会出现树人僵尸");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("树人僵尸在不啃咬时带有「隐匿」效果，用无视「隐匿」效果的植物对付他！");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("联动关卡内不进行梯度出怪，且会出现大部分僵尸");
                    count++;
                    break;
                case 6:
                    crazyDave.smallTalk("战斗！");
                    count++;
                    break;
                case 7:
                    crazyDave.smallTalk("加油吧！");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }
}

