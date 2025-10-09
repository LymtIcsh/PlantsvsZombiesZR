using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Zombie: MonoBehaviour
{
    #region 定义

    public bool 可以啃咬 = true;

    protected bool armIsDrop = false;
    protected bool headIsDrop = false;
    protected bool level1ArmorIsDrop = false;
    protected bool level2ArmorIsDrop = false;
    
    public bool 铁门类僵尸 = false;

    public int zombieID;
    /// <summary>
    /// 0为普通僵尸，1为路障，2为铁桶
    /// </summary>
    public int 基础僵尸类型;

    //[HideInInspector]
    public int pos_row;   //位于第几行

    //生命相关
    [HideInInspector]
    public int 血量;   //血量

    [HideInInspector]
    public int 最大血量;

    /// <summary>
    /// 一类防具
    /// </summary>
    [HideInInspector]
    public int level1ArmorHealth;
    [HideInInspector]
    public int level1ArmorMaxHealth;



    /// <summary>
    /// 二类防具
    /// </summary>
    [HideInInspector]
    public int level2ArmorHealth;
    [HideInInspector]
    public int level2ArmorMaxHealth;

    protected bool 一类防具半破损已经切换 = false;
    protected bool 一类防具完全破损已经切换 = false;
    [HideInInspector]
    public bool 二类半破损已经切换 = false;
    [HideInInspector]
    public bool 二类完全破损已经切换 = false;

    [HideInInspector]
    public bool alive = true;

    //攻击相关
    [HideInInspector]
    public int 攻击力;  //攻击力
    protected Plant attackPlant;   //当前所攻击植物的Plant组件
    protected Zombie attackZombie;

    protected Animator myAnimator;   //动画组件
    

    [HideInInspector]
    public int audioIndex = 1;

    //static int orderOffset = 0;

    [HideInInspector]
    public Material highlightMaterial; // 在Inspector中指定的高光材质

    [HideInInspector]
    public const float highlightDuration = 0.06f; // 高光效果的持续时间（秒）

    [HideInInspector]
    public Renderer[] allRenderers;    // 所有 Renderer 组件

    private float 减速时间 = 5f; // 假设第一次冻结的时间为5秒（可以根据需求调整）
    private Coroutine 减速协程; // 用来存储协程的引用

    private bool 中毒效果中 = false;
    private bool 冰冻效果中 = false;


    public Sprite coneBroken1;//路障三个
    public Sprite coneBroken2;
    public Sprite coneBroken3;
    public GameObject coneDrop;

    public Sprite bucketBroken1; //铁桶三个
    public Sprite bucketBroken2;
    public Sprite bucketBroken3;
    public GameObject bucketDrop;

    public Sprite fullHead;

    public Sprite brokenArm;//受伤的手臂图片

    public GameObject zombieHeadDrops;
    public GameObject zombieArmDrops;


    public bool dontHaveDropHead;

    public Coroutine 高亮;

    public bool isEating;

    public ZombieForestSlider zombieForestSlider;

    public GameObject 正在啃咬目标;

    //[HideInInspector]
    public bool dying = false;

    public Sprite[] DoorSprite;//铁门图片
    private GameObject[] 铁门僵尸啃咬显示的;
    private GameObject[] 铁门僵尸行走显示的;
    private GameObject[] 铁门僵尸无铁门行走时显示的;
    private GameObject[] 铁门;
    private GameObject[] 铁门断臂时强制不显示;
    private GameObject[] 铁门断臂时强制显示;
    private GameObject[] 铁门全部;

    private float 狂暴速度乘区 = 1f;
    private float 减速速度乘区 = 1f;
    private float 随机速度乘区 = 1f;
    private float 关卡特殊乘区 = 1f;
    [SerializeField]
    private float _环境速度乘区 = 1f;

    
    protected virtual float 环境速度乘区//用于覆写
    {
        get => _环境速度乘区;
        set => _环境速度乘区 = value;
    }

    private TMP_Text 血量显示;

    public 僵尸debuff debuff;
    public 僵尸buff buff;

    public bool newZombie = false;

    private bool hasFaded = false;

    private bool isEntrance = false;
    #endregion

    #region 开始时执行
    protected virtual void Awake()
    {
        //highlightMaterial = Resources.Load<Material>("Shader/highLight");
        myAnimator = gameObject.GetComponent<Animator>();
        allRenderers = GetComponentsInChildren<Renderer>(true);

        if (GameManagement.instance != null && GameManagement.instance.游戏进行 && ZombieManagement.instance != null)
        {
            ZombieManagement.instance.GetComponent<ZombieManagement>().addZombieNumAll(gameObject);
        }
        else
        {
            展示动画初始化();
        }

        TMP_Text 加载血量物体 = Resources.Load<TMP_Text>("Prefabs/Effects/血量显示/普通僵尸血量显示");
        血量显示 = Instantiate(加载血量物体, transform.position, Quaternion.identity, transform);
        血量显示.gameObject.SetActive(false);

        随机速度乘区 = Random.Range(0.8f, 1.2f);
        加载动画速度();
        //foreach (Renderer renderer in allRenderers)
        //{
        //    if (renderer != null && renderer.material != highlightMaterial)
        //    {
        //        renderer.material = highlightMaterial;
        //    }
        //}

        GetComponent<Collider2D>().enabled = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        血量显示.gameObject.SetActive(true);
        Vector3 parentScale = transform.localScale;
        float safeX = parentScale.x != 0 ? parentScale.x : 1f;
        float safeY = parentScale.y != 0 ? parentScale.y : 1f;
        float safeZ = parentScale.z != 0 ? parentScale.z : 1f;
        血量显示.transform.localScale = new Vector3(
            0.05f / safeX,
            0.05f / safeY,
            0.05f / safeZ
        );
        血量显示.gameObject.SetActive(false);
        血量显示.gameObject.SetActive(true);
        血量显示.gameObject.SetActive(GameManagement.是否显示血量);

        铁门初始化();

        zombieForestSlider = GameManagement.instance.zombieForestSlider;

        游戏动画初始化();

        血量 = ZombieStructManager.GetZombieStructById(zombieID).BaseHP;
        level1ArmorHealth = ZombieStructManager.GetZombieStructById(zombieID).Armor1HP;
        level2ArmorHealth = ZombieStructManager.GetZombieStructById(zombieID).Armor2HP;
        level1ArmorMaxHealth = level1ArmorHealth;
        level2ArmorMaxHealth = level2ArmorHealth;

        攻击力 = 50;

        switch (基础僵尸类型)
        {
            case 1: loadCone(1); break;
            case 2: loadBucket(1); break;
            default: break;
        }
        if(铁门类僵尸)
        {
            List<GameObject> combinedList = new List<GameObject>();
            AddUnique(铁门僵尸啃咬显示的, combinedList);
            AddUnique(铁门僵尸行走显示的, combinedList);
            AddUnique(铁门僵尸无铁门行走时显示的, combinedList);
            AddUnique(铁门, combinedList);
            AddUnique(铁门断臂时强制不显示, combinedList);
            AddUnique(铁门断臂时强制显示, combinedList);
            铁门全部 = combinedList.ToArray();
            铁门行走显示逻辑();

        }


        if(GameManagement.levelData.LevelType == levelType.TheDreamOfPotatoMine)
        {
            关卡特殊乘区 *= 1.5f * GameManagement.GameDifficult;
        }
        //添加随机速度增幅
        
        加载动画速度();


        activate();
        
        
        最大血量 = 血量;

        InvokeRepeating("计算毒伤", 0, 2f);

        加载血量文本();
    }


    protected virtual void activate()
    {
        Invoke("doAfterStartSomeTimes", Random.Range(15, 80));
        gameObject.SetActive(true);
    }

    protected virtual void doAfterStartSomeTimes()//用于森林僵尸10 - 30秒后生成草丛并杀死自己
    {
        
    }

    private void OnEnable()
    {
        if (gameObject != null)
        {
            ZombieManagement.allZombies.Add(gameObject.GetComponent<Zombie>());
        }
    }
    private void OnDisable()
    {
        try
        {
            if (ZombieManagement.instance != null && ZombieManagement.instance.isActiveAndEnabled && this != null && gameObject != null && !GameManagement.instance.游戏进行) 
            {
                ZombieManagement.instance.minusZombieNumAll(gameObject);
            }

            if (this != null)
            {
                var zombieComp = GetComponent<Zombie>();
                if (zombieComp != null)
                {
                    ZombieManagement.allZombies.Remove(zombieComp);
                }
            }
        }
        catch
        {

        }
        
    }



    #endregion

    protected virtual void Update()
    {
        if(!isEntrance && transform.position.x <= 5.3f)
        {
            isEntrance = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    #region 啃咬与攻击
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(可以啃咬 && !dying)
        {
            if (!debuff.魅惑
            && collision.tag == "Plant"
            && myAnimator.GetBool("Attack") == false
            )
            {
                attackPlant = collision.GetComponent<Plant>();
                attackZombie = collision.GetComponent<Zombie>();
                if(attackPlant != null && attackPlant.row == pos_row && attackPlant.植物类型 != PlantType.地刺类植物 && attackPlant.可被攻击)
                {

                }
                else if(attackZombie != null && attackZombie.pos_row == pos_row && attackZombie.debuff.魅惑 != debuff.魅惑)
                {

                }
                else
                {
                    return;
                }

                正在啃咬目标 = collision.gameObject;
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Attack", true);
                isEating = true;

                if (铁门类僵尸)
                {
                    铁门啃咬显示逻辑();
                }
            }
            else if (collision.tag == "Zombie"
            && collision.GetComponent<Zombie>().pos_row == pos_row
            && collision.GetComponent<Zombie>().debuff.魅惑 != debuff.魅惑
            && myAnimator.GetBool("Attack") == false
            )
            {
                正在啃咬目标 = collision.gameObject;
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Attack", true);
                isEating = true;
                attackZombie = collision.GetComponent<Zombie>();

                if (铁门类僵尸)
                {
                    铁门啃咬显示逻辑();
                }

            }
        }
        
        if (!debuff.魅惑 && collision.tag == "GameOverLine" && !dying && alive)
        {
            GameManagement.instance.GetComponent<GameManagement>().gameOver();
        }
        else if(collision.tag == "GameOverLine")
        {
            Destroy(gameObject);
            ZombieManagement.instance.minusZombieNumAll(gameObject);
        }

        if (collision.tag == "ZombieDisappearLine")
        {

            if (ZombieManagement.instance != null && gameObject != null)
            {
                ZombieManagement.instance.minusZombieNumAll(gameObject);
                Destroy(gameObject);
            }
        }

    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == 正在啃咬目标)
        {
            myAnimator.SetBool("Attack", false);
            myAnimator.SetBool("Walk", true);
            isEating = false;
            正在啃咬目标 = null;
            attackPlant = null;
            attackZombie = null;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = true;

            if (铁门类僵尸)
            {
                if (!level2ArmorIsDrop)
                {
                    铁门行走显示逻辑();
                }
                else
                {
                    铁门无门行走显示逻辑();
                }
            }
        }
        //else if (collision.tag == "界外死亡")
        //{
        //    die();
        //}
    }

    //protected virtual void OnTriggerStay2D(Collider2D collision)
    //{
    //    ////if (可以啃咬 &&
    //    ////    !dying &&
    //    ////    !isEating && collision.tag == "Plant" && collision.GetComponent<Plant>().row == pos_row && collision.GetComponent<Plant>().plantType == 0 && collision.GetComponent<Plant>().canBeAttacked)
    //    ////{
    //    ////    myAnimator.SetBool("Walk", false);
    //    ////    myAnimator.SetBool("Attack", true);
    //    ////    isEating = true;
    //    ////    plant = collision.GetComponent<Plant>();
    //    ////    正在啃咬目标 = collision.gameObject;
    //    ////    啃咬目标方法 = 正在啃咬目标.GetComponent<Plant>();
    //    ////    if (isDoorZombie)
    //    ////    {
    //    ////        铁门啃咬显示逻辑();
    //    ////    }
    //    ////}
    //    //if (isEating && plant.row != pos_row)
    //    //{
    //    //    myAnimator.SetBool("Attack", false);
    //    //    myAnimator.SetBool("Walk", true);
    //    //    isEating = false;
    //    //    正在啃咬目标 = null;
    //    //    plant = null;
    //    //    if (isDoorZombie)
    //    //    {
    //    //        if (!level2ArmorIsDrop)
    //    //        {
    //    //            铁门行走显示逻辑();
    //    //        }
    //    //        else
    //    //        {
    //    //            铁门无门行走显示逻辑();
    //    //        }
    //    //    }
    //    //}
    //}
    public virtual void attack()
    {
        if (attackPlant != null && !dying)
        {
            attackPlant.beAttacked(攻击力, "beEated", gameObject);
        }
        else if(attackZombie != null && !dying)
        {
            attackZombie.beAttacked(攻击力, 1, -1);
        }
    }
    #endregion

    #region 受击
    public virtual void beAttacked(int hurt, int BulletType ,int AttackedMusicType)//AttackedMusicType为2时是地刺攻击，仅播放本体受伤音效，为-1时不播放音效
    {
        if(AttackedMusicType != -1)
        {
            if (AttackedMusicType == 1)
            {
                playAudioOfBeingAttacked(BulletType);
            }
            else if (AttackedMusicType == 2)
            {
                playAudioOfBodyBeingAttacked();
            }
        }
        

        TriggerHighlight(BulletType);

        if (BulletType == 0) // 本体伤害，扣除本体血量
        {
            HandleBodyDamage(hurt); // 调用本体受伤的处理方法
        }
        else if (BulletType == 1 || BulletType==3) // 一类伤害
        {
            if (level2ArmorHealth > 0) // 如果二类防具有血量
            {
                // 二类防具先扣血
                int level2Damage = Mathf.Min(hurt, level2ArmorHealth);
                level2ArmorHealth -= level2Damage;
                hurt -= level2Damage; // 剩余伤害继续处理
                HandleLevel2ArmorDamage(level2Damage); // 调用二类防具受伤处理

                if (hurt > 0) // 如果仍有伤害未被抵消
                {
                    // 一类防具扣血
                    int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                    level1ArmorHealth -= level1Damage;
                    hurt -= level1Damage; // 剩余伤害继续处理
                    HandleLevel1ArmorDamage(level1Damage); // 调用一类防具受伤处理

                    if (hurt > 0) // 如果仍有伤害未被抵消
                    {
                        HandleBodyDamage(hurt); // 继续扣除本体血量
                    }
                }
            }
            else // 二类防具没有血量，直接扣一类防具和本体
            {
                // 一类防具先扣血
                int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                level1ArmorHealth -= level1Damage;
                hurt -= level1Damage; // 剩余伤害继续处理
                HandleLevel1ArmorDamage(level1Damage); // 调用一类防具受伤处理

                if (hurt > 0) // 如果仍有伤害未被抵消
                {
                    HandleBodyDamage(hurt); // 继续扣除本体血量
                }
            }
        }
        else if (BulletType == 2) // 二类伤害
        {
            if (level1ArmorHealth > 0) // 如果一类防具有血量
            {
                // 先扣除一类防具的血量
                int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                level1ArmorHealth -= level1Damage;
                hurt -= level1Damage; // 剩余伤害继续处理
                HandleLevel1ArmorDamage(level1Damage); // 调用一类防具受伤处理
            }

            if (hurt > 0) // 如果一类防具已经没有血量，继续扣除本体血量
            {
                HandleBodyDamage(hurt); // 继续扣除本体血量
            }
        }
        loadArmorStatus();
    }

    // 一类防具受到伤害时调用的处理方法
    protected virtual void HandleLevel1ArmorDamage(int hurt)
    {
        // 这里可以添加一类防具受伤后的逻辑，例如播放一类防具的损坏动画、音效等
        //Debug.Log($"一类防具受到 {hurt} 点伤害");
    }

    // 二类防具受到伤害时调用的处理方法
    protected virtual void HandleLevel2ArmorDamage(int hurt)
    {
        // 这里可以添加二类防具受伤后的逻辑，例如播放二类防具的损坏动画、音效等
        //Debug.Log($"二类防具受到 {hurt} 点伤害");
    }

    // 本体受到伤害时调用的处理方法
    protected virtual void HandleBodyDamage(int hurt)
    {
        // 这里可以处理本体受伤后的逻辑，例如扣除血量、播放受伤音效、动画等
        血量 -= hurt;
        //Debug.Log($"本体受到 {hurt} 点伤害，剩余血量: {bloodVolume}");
        // 可以在这里添加本体受伤后的其他逻辑，例如播放受伤动画、音效等
    }
    //被灼伤，被火焰攻击时调用
    public virtual void beBurned(int damage)
    {
        beAttacked(damage, 1, 1);
        切换冰冻状态(false);
    }
    public virtual void beSquashed()
    {
        if (buff.隐匿) return;
        beAttacked(1800, 1, -1);
        if (血量 <= 0 && !alive)
        {
            ZombieManagement.instance.minusZombieNumAll(gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void beChompered()
    {
        ZombieManagement.instance.minusZombieNumAll(gameObject);
        Destroy(gameObject);
    }

    public void 计算坚韧(){
        level1ArmorHealth += buff.坚韧 * 50;
    }

    public void beAshAttacked()
    {
        if(血量 <= 1800)
        {
            Vector3 offset = new Vector3(0.137f, -0.088f, 0f); // z轴保持不变
            GameObject charredZombie = Instantiate(
                Resources.Load<GameObject>("Prefabs/Zombies/Zombie_Other/Zombie_charred"),
                gameObject.transform.position + offset,
                Quaternion.identity
            );
            ZombieManagement.instance.minusZombieNumAll(gameObject);
            Destroy(gameObject);
            charredZombie.GetComponent<SortingGroup>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
        }
        else
        {
            beAttacked(1800, 1, -1);
        }
    }

    #endregion

    #region 音效播放
    // 播放被攻击音效
    public virtual void playAudioOfBeingAttacked(int bulletType)
    {
        if (铁门类僵尸 && !level2ArmorIsDrop && bulletType == 1)
        {
            // 使用 AudioManager 播放音效
            AudioManager.Instance.PlaySoundEffect(11); // 对应 shieldhit 音效
        }
        else
        {
            if (!level1ArmorIsDrop)
            {
                switch (基础僵尸类型)
                {
                    case 0:
                        // 使用 AudioManager 播放不同的音效
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // 对应 bodyhit1 或 bodyhit2
                        break;
                    case 1:
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 9 : 10); // 对应 conehit1 或 conehit2
                        break;
                    case 2:
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 11 : 12); // 对应 shieldhit1 或 shieldhit2
                        break;
                }
            }
            else
            {
                // 使用 AudioManager 播放默认的音效
                AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // 对应 bodyhit1 或 bodyhit2
            }
        }

        // 切换音效索引
        audioIndex = (audioIndex == 1) ? 2 : 1;
    }

    // 播放身体受伤音效
    public virtual void playAudioOfBodyBeingAttacked() // 地刺攻击使用，播放身体受伤
    {
        // 使用 AudioManager 播放音效
        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // 对应 bodyhit1 或 bodyhit2

        // 切换音效索引
        audioIndex = (audioIndex == 1) ? 2 : 1;
    }

    // 播放僵尸啃咬的音效
    public virtual void PlayEatAudio()
    {
        // 播放随机的啃咬音效 (chomp1 或 chomp2)
        AudioManager.Instance.PlaySoundEffect(Random.Range(13, 15)); // 对应 chomp1 或 chomp2
    }

    public virtual void fallDown()
    {
        AudioManager.Instance.PlaySoundEffect(20);
    }
    #endregion

    #region 中毒、冰冻等状态切换
    private void 切换材质状态(int 状态类型)
    {
        switch(状态类型)
        {
            case 0:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(1.0f, 1.0f, 1.0f, 1f));
                    }
                }
                break;
            case 1:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.0f, 0.279f, 1.0f, 1f));
                    }
                }
                break;
            case 2:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.0f, 0.279f, 1.0f, 1f));
                    }
                }
                break;
            case 3:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.0f, 0.647f, 0.165f, 1f));
                    }
                }
                break;
            case 4:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(1.0f, 0.0f, 0.787f, 1f));
                    }
                }
                break;
            case 5:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.729f, 0.053f, 0.0f, 1f));
                    }
                }
                break;
        }
    }

    private void 切换冰冻状态(bool 开始)//0为解除，1为开始
    {
        if (冰冻效果中 == 开始) return; // 如果当前状态已经是目标状态，则直接返回
        if(开始)
        {
            切换中毒状态(false);
        }
        冰冻效果中 = 开始;
        减速速度乘区 = 开始 ? 0.5f : 1f;  // 使用三元运算符来简化赋值
        切换材质状态(开始 ? 1 : 0);
        加载动画速度();
    }
    public virtual void 附加减速() // 用于减速
    {
        if (减速协程 == null)
        {
            debuff.减速 = 减速时间;
            减速协程 = StartCoroutine(减速效果协程(debuff.减速));
        }
        else
        {
            debuff.减速 = 减速时间;
            StopCoroutine(减速协程);
            减速协程 = StartCoroutine(减速效果协程(debuff.减速));
        }
    }
    private IEnumerator 减速效果协程(float 减速时间)
    {
        切换冰冻状态(true);
        float elapsedTime = 0f;
        while (elapsedTime < 减速时间)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        解除减速状态();
    }
    protected void 解除减速状态()
    {
        切换冰冻状态(false);
    }

    private void 切换中毒状态(bool 开始)//0为解除，1为开始
    {
        if (中毒效果中 == 开始) return;
        if(开始)
        {
            切换冰冻状态(false);
            切换狂暴状态(false);
        }
        if(!开始)
        {
            debuff.中毒 = 0;
        }
        中毒效果中 = 开始;

        
        切换材质状态(开始 ? 3 : 0);
      
        加载动画速度();

        加载血量文本();
    }

    public void 附加中毒(int 附加中毒层数)
    {
        debuff.中毒 += 附加中毒层数;

        切换狂暴状态(false);


       
        for(int i = 0;i < 附加中毒层数;i++)
        {
            Invoke("DecreasePoisonLayer", 6f);
        }
        if(debuff.中毒 > 60)
        {
            SetAchievement.SetAchievementCompleted("我不是毒神");
        }
        if (debuff.中毒 > 0 && !中毒效果中)
        {
            切换中毒状态(true);
            中毒效果中 = true;
        }

        加载血量文本();
    }
    public void 引爆毒伤(int 引爆次数)
    {
        if (debuff.中毒 > 0 && 引爆次数 > 0)
        {
            beAttacked(debuff.中毒 * 最大血量 / 100 * 引爆次数, 3, -1);
        }
        else
        {
            return;
        }
    }
    public void DecreasePoisonLayer()
    {
        debuff.中毒 -= 1;
        if (debuff.中毒 < 0)
            debuff.中毒 = 0;

        if (debuff.中毒 == 0 && 中毒效果中)
        {
            切换中毒状态(false);
        }

        加载血量文本();
    }
    public void DecreasePoisonLayer(int 减少的层数)
    {
        debuff.中毒 -= 减少的层数;
        if (debuff.中毒 < 0)
            debuff.中毒 = 0;

        if (debuff.中毒 == 0 && 中毒效果中)
        {
            切换中毒状态(false);
        }

        加载血量文本();
    }
    public void 计算毒伤()
    {
        if(debuff.中毒 > 0)
        {
            beAttacked(debuff.中毒 * 3, 1,-1);
        }
        else
        {
            return;
        }
    }

    public void 切换狂暴状态(bool 开)//0为解除，1为开始
    {
        
        if(debuff.狂暴 == 开)
        {
            return;
        }
        else
        {
            if(开)
            {
                if (开)
                {
                    切换冰冻状态(false);
                    切换中毒状态(false);
                }
                切换材质状态(5);
                debuff.狂暴 = true;
                狂暴速度乘区 = 3f;
            }
            else
            {
                切换材质状态(0);
                debuff.狂暴 = false;
                狂暴速度乘区 = 1f;
            }
            
        }
        加载动画速度();

    }

    public void 切换魅惑状态()//魅惑只能进入，无法退出
    {
        if (debuff.魅惑)
            return;
        debuff.魅惑 = true;
        transform.Rotate(0f, 180f, 0f);
        血量显示.transform.Rotate(0f, 180f, 0f);
        切换中毒状态(false);
        切换冰冻状态(false);
        切换狂暴状态(false);
        切换材质状态(4);
        ZombieManagement.instance.minusZombieNumAll(gameObject);
        gameObject.tag = "Plant";
        //myAnimator.SetBool("Attack", false);
        //myAnimator.SetBool("Walk", true);
        //isEating = false;
        //正在啃咬目标 = null;
        //attackPlant = null;
        //if (铁门类僵尸)
        //{
        //    if (!level2ArmorIsDrop)
        //    {
        //        铁门行走显示逻辑();
        //    }
        //    else
        //    {
        //        铁门无门行走显示逻辑();
        //    }
        //}
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }

    public void 展示动画初始化()
    {
        if (newZombie)
        {
            string[] walkAnimations = { "anim_idle", "anim_idle2" };
            string randomAnim = walkAnimations[Random.Range(0, walkAnimations.Length)];

            if (HasAnimation(randomAnim))
            {
                myAnimator.Play(randomAnim);
            }
            else
            {
                Debug.LogWarning($"动画 '{randomAnim}' 不存在，无法播放");
            }
        }
    }

    public void 游戏动画初始化()
    {
        if (!newZombie)
        {
            if (myAnimator.HasParameter("Walk"))
                myAnimator.SetBool("Walk", true);

            if (myAnimator.HasParameter("Idle"))
                myAnimator.SetBool("Idle", false);
        }
        else
        {
            string[] walkAnimations = { "anim_walk", "anim_walk2" };
            string randomAnim = walkAnimations[Random.Range(0, walkAnimations.Length)];

            if (HasAnimation(randomAnim))
            {
                myAnimator.Play(randomAnim);
            }
            else
            {
                Debug.LogWarning($"动画 '{randomAnim}' 不存在，无法播放");
            }
        }
    }

    private bool HasAnimation(string animationName)
    {
        if (myAnimator == null || myAnimator.runtimeAnimatorController == null)
            return false;

        foreach (var clip in myAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
                return true;
        }

        return false;
    }



    private void 加载动画速度()
    {
        if(myAnimator != null)
        {
            myAnimator.speed = 狂暴速度乘区 * 减速速度乘区 * 随机速度乘区 * 关卡特殊乘区 * 环境速度乘区;
        }
    }

    #endregion

    #region 加载不同状态（铁门独立在铁门代码中）
    protected virtual void hideHead()
    {
        if(!dying)
        {
            dying = true;

            StartCoroutine(濒死扣血());

            AudioManager.Instance.PlaySoundEffect(59);
            if(!newZombie)
            {
                Transform createPosition = FindInChildren(transform, "Zombie_head");
                Transform hidePosition = FindInChildren(transform, "Zombie_jaw");
                Transform showPosition = FindInChildren(transform, "Zombie_neck_0");
                if (createPosition != null)
                {
                    createPosition.gameObject.SetActive(false);
                }
                if (hidePosition != null)
                {
                    SpriteRenderer shouldBeHide2 = hidePosition.GetComponent<SpriteRenderer>();
                    shouldBeHide2.enabled = false;
                }
                if (showPosition != null)
                {
                    showPosition.gameObject.SetActive(true);
                }
                if (!dontHaveDropHead && !GameManagement.isPerformance)
                {
                    GameObject gameObject = Instantiate(zombieHeadDrops, createPosition.position, Quaternion.identity);

                    gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                }
            }
            else
            {
                Transform createPosition = FindInChildren(transform, "ZOMBIE_HEAD");
                Transform hidePosition = FindInChildren(transform, "ZOMBIE_JAW");
                Transform hidePosition2 = FindInChildren(transform, "ZOMBIE_HAIR");
                createPosition.gameObject.SetActive(false);
                hidePosition.gameObject.SetActive(false);
                hidePosition2.gameObject.SetActive(false);

                if (!dontHaveDropHead && !GameManagement.isPerformance)
                {
                    GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombieNormalHeadDrop);

                    if(fullHead != null)
                    {
                        go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                        go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(fullHead);
                    }

                    go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;


                    go.transform.position = createPosition.transform.position;
                    go.transform.rotation = Quaternion.identity;

                }
            }
            
            
        }
    }
    protected Transform FindInChildren(Transform parent,string beFind)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true)) // 第二个参数设置为true，查找所有子物体（包括未激活的）
        {
            if (child.name == beFind)
            {
                return child;
            }
        }
        return null; // 没有找到名为子物体
    }
    public void loadCone(int loadType)//根据不同伤害加载路障
    {
        if(!newZombie)
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform coneTransform = FindInChildren(transform, "Cone");

            if (coneTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer coneSpriteRenderer = coneTransform.GetComponent<SpriteRenderer>();

                if (coneSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: coneTransform.gameObject.SetActive(true); coneSpriteRenderer.sprite = coneBroken1; break;
                        case 2: coneSpriteRenderer.sprite = coneBroken2; break;
                        case 3: coneSpriteRenderer.sprite = coneBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject shatterEffect = Instantiate(coneDrop, coneTransform.position, Quaternion.identity);
                                shatterEffect.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }

                            coneSpriteRenderer.enabled = false;
                            coneTransform.gameObject.SetActive(false);

                            break;
                        default: break;
                    }

                }
            }
        }
        else
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform coneTransform = FindInChildren(transform, "ZOMBIE_CONE1");

            if (coneTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer coneSpriteRenderer = coneTransform.GetComponent<SpriteRenderer>();

                if (coneSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: coneTransform.gameObject.SetActive(true); coneSpriteRenderer.sprite = coneBroken1; break;
                        case 2: coneSpriteRenderer.sprite = coneBroken2; break;
                        case 3: coneSpriteRenderer.sprite = coneBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombiePendantDrop);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(coneSpriteRenderer.sprite);
                                go.transform.position = coneTransform.position;
                                go.transform.rotation = Quaternion.identity;

                                go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }

                            coneTransform.gameObject.SetActive(false);

                            break;
                        default: break;
                    }

                }
            }
        }
        
    }
    public virtual void loadBucket(int loadType)//根据不同伤害加铁桶
    {
        if(!newZombie)
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform bucketTransform = FindInChildren(transform, "Bucket");

            if (bucketTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer bucketSpriteRenderer = bucketTransform.GetComponent<SpriteRenderer>();

                if (bucketSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: bucketTransform.gameObject.SetActive(true); bucketSpriteRenderer.sprite = bucketBroken1; break;
                        case 2: bucketSpriteRenderer.sprite = bucketBroken2; break;
                        case 3: bucketSpriteRenderer.sprite = bucketBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject shatterEffect = Instantiate(bucketDrop, bucketTransform.position, Quaternion.identity);
                                shatterEffect.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }


                            bucketTransform.gameObject.SetActive(false);
                            bucketSpriteRenderer.enabled = false;
                            break;
                        default: break;
                    }

                }
            }
        }
        else
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform bucketTransform = FindInChildren(transform, "ZOMBIE_BUCKET1");

            if (bucketTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer bucketSpriteRenderer = bucketTransform.GetComponent<SpriteRenderer>();

                if (bucketSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: bucketTransform.gameObject.SetActive(true); bucketSpriteRenderer.sprite = bucketBroken1; break;
                        case 2: bucketSpriteRenderer.sprite = bucketBroken2; break;
                        case 3: bucketSpriteRenderer.sprite = bucketBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombiePendantDrop);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(bucketSpriteRenderer.sprite);
                                go.transform.position = bucketTransform.position;
                                go.transform.rotation = Quaternion.identity;
                                go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }
                            bucketTransform.gameObject.SetActive(false);

                            break;
                        default: break;
                    }

                }
            }
        }

        
    }

    protected virtual void dropArm()//手臂掉落
    {
        if (!armIsDrop)
        {
            armIsDrop = true;
            AudioManager.Instance.PlaySoundEffect(59);
            if (!newZombie)
            {
                // 查找名为 "Cone" 的所有子物体（包括未激活的）
                Transform createPosition = FindInChildren(transform, "CreateDropArmPosition");
                Transform shouldBeHide1 = FindInChildren(transform, "Zombie_outerarm_lower");
                Transform shouldBeHide2 = FindInChildren(transform, "Zombie_outerarm_hand");

                Transform shouldBeExchange = FindInChildren(transform, "Zombie_outerarm_upper");
                if (shouldBeExchange != null)
                {

                    shouldBeExchange.GetComponent<SpriteRenderer>().sprite = brokenArm;
                    shouldBeHide1.GetComponent<SpriteRenderer>().enabled = false;
                    shouldBeHide2.GetComponent<SpriteRenderer>().enabled = false;
                    if (!GameManagement.isPerformance)
                    {
                        GameObject gameObject = Instantiate(zombieArmDrops, createPosition.position, Quaternion.identity);
                        gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                    }
                }
            }
            else
            {
                Transform shouldBeHide1 = FindInChildren(transform, "ZOMBIE_OUTERARM_LOWER");
                Transform shouldBeHide2 = FindInChildren(transform, "ZOMBIE_OUTERARM_HAND");
                Transform shouldBeHide3 = FindInChildren(transform, "ZOMBIE_OUTERARM_HAND2");
                Transform shouldBeExchange = FindInChildren(transform, "ZOMBIE_OUTERARM_UPPER");
                if (shouldBeExchange != null)
                {

                    shouldBeExchange.GetComponent<SpriteRenderer>().sprite = brokenArm;
                    shouldBeHide1.gameObject.SetActive(false);
                    shouldBeHide2.gameObject.SetActive(false);
                    shouldBeHide3.gameObject.SetActive(false);
                    shouldBeHide1.GetComponent<SpriteRenderer>().enabled = false;
                    shouldBeHide2.GetComponent <SpriteRenderer>().enabled = false;
                    shouldBeHide3.GetComponent<SpriteRenderer>().enabled = false;
                    if (!dontHaveDropHead && !GameManagement.isPerformance)
                    {
                        GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombieNormalArmDrop);
                        go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;

                        go.transform.position = shouldBeHide1.transform.position;
                        go.transform.rotation = Quaternion.identity;
                    }
                }
            }
        }

    }
    virtual public void loadArmorStatus()
    {
        switch (基础僵尸类型)
        {
            case 1:
                if (!level1ArmorIsDrop)
                {
                    if (!一类防具半破损已经切换)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 * 2)
                        {
                            一类防具半破损已经切换 = true;
                            loadCone(2);
                        }

                    }
                    if (!一类防具完全破损已经切换)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3)
                        {
                            一类防具完全破损已经切换 = true;
                            loadCone(3);
                        }

                    }
                    if (level1ArmorHealth <= 0)
                    {
                        level1ArmorIsDrop = true;
                        loadCone(0);
                    }


                }
                break;
            case 2:
                if (!level1ArmorIsDrop)
                {
                    if (!一类防具半破损已经切换)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 * 2)
                        {
                            一类防具半破损已经切换 = true;
                            loadBucket(2);
                        }

                    }
                    if (!一类防具完全破损已经切换)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3)
                        {
                            一类防具完全破损已经切换 = true;
                            loadBucket(3);
                        }

                    }

                    if (level1ArmorHealth <= 0)
                    {
                        level1ArmorIsDrop = true;
                        loadBucket(0);
                    }
                }
                break;
            default: break;
        }
        if (铁门类僵尸)
        {
            loadDoorStatus();
        }

        if (血量 <= 最大血量 / 2)//断手
        {
            dropArm();
        }
        if (血量 <= 最大血量 / 3)//断头  
        {
            //隐藏头
            hideHead();
        }
        if (血量 <= 0 && alive == true)
        {
            die();
        }
        加载血量文本();
        
        
    }

    public virtual void die()
    {
        if (!alive) return;

        alive = false;

        切换中毒状态(false);
        切换冰冻状态(false);
        切换狂暴状态(false);

        // ? 动画控制器安全检查
        if (myAnimator != null)
        {
            if (HasParameter(myAnimator, "Walk"))
                myAnimator.SetBool("Walk", false);
            else
                Debug.LogWarning("Animator 缺少参数: Walk");

            if (HasParameter(myAnimator, "Die"))
                myAnimator.SetBool("Die", true);
            else
                Debug.LogWarning("Animator 缺少参数: Die");
        }
        else
        {
            Debug.LogError("myAnimator 未被赋值！");
        }

        // ? 血量处理
        level1ArmorHealth = 0;
        level2ArmorHealth = 0;
        血量 = 0;
        loadArmorStatus();

        // ? 失效碰撞体前确认存在
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
        else
            Debug.LogWarning("未找到 Collider2D 组件");

        // ? 通知 ZombieManagement（带空引用检查）
        if (GameManagement.instance.zombieManagement != null)    
        {
            ZombieManagement zm = GameManagement.instance.zombieManagement.GetComponent<ZombieManagement>();
            if (zm != null)
                zm.minusZombieNumAll(gameObject);
            else
                Debug.LogWarning("zombieManagement 对象上没有 ZombieManagement 组件");
        }
        else
        {
            Debug.LogWarning("GameManagement.zombieManagement 为 null");
        }

        
    }

    // ?? 工具方法：检查 Animator 是否包含某参数
    private bool HasParameter(Animator anim, string paramName)
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }


    public virtual void LawnMowerDie()
    {
        血量 = 0;
        level1ArmorHealth = 0;
        level2ArmorHealth = 0;
        loadArmorStatus();
        Destroy(gameObject);
        alive = false;
        dying = true;
        //全场僵尸数减一
        GameManagement.instance.zombieManagement.GetComponent<ZombieManagement>().minusZombieNumAll(gameObject);
    }


    //public virtual void lawnMowerDie()
    //{
    //    Debug.Log("111");
    //    if (alive)
    //    {
    //        alive = false;

    //        myAnimator.enabled = false;

    //        float animationDuration = 1f; // 60 frames at 60 FPS
    //        float timeElapsed = 0f;

    //        Vector3 initialPosition = transform.position;
    //        Quaternion initialRotation = transform.rotation;
    //        Vector3 initialScale = transform.localScale;

    //        Vector3 positionDelta23Frames = new Vector3(0.2913f, 0f, 0f);
    //        Vector3 positionDelta60Frames = new Vector3(0.41f, -0.3751f, 0f);

    //        Quaternion rotationDelta23Frames = Quaternion.Euler(0f, 0f, -29.536f);
    //        Quaternion rotationDelta60Frames = Quaternion.Euler(0f, 0f, -90f);

    //        Vector3 scaleDelta23Frames = new Vector3(-0.144f, 0f, 0f); // Relative to initial x scale
    //        Vector3 scaleDelta60Frames = new Vector3(-0.29478f, 0f, 0f);

    //        gameObject.GetComponent<Collider2D>().enabled = false;
    //        if (GameManagement.zombieManagement != null)
    //        {
    //            GameManagement.zombieManagement.GetComponent<ZombieManagement>().minusZombieNumAll(gameObject);
    //        }
    //        void UpdateAnimation()
    //        {
    //            timeElapsed += Time.deltaTime;
    //            float normalizedTime = Mathf.Clamp01(timeElapsed / animationDuration);

    //            if (normalizedTime <= 23f / 60f)
    //            {
    //                // Interpolation between initial state and 23 frames
    //                float t = normalizedTime / (23f / 60f);
    //                transform.position = Vector3.Lerp(initialPosition, initialPosition + positionDelta23Frames, t);
    //                transform.rotation = Quaternion.Slerp(initialRotation, initialRotation * rotationDelta23Frames, t);
    //                transform.localScale = Vector3.Lerp(initialScale, initialScale + Vector3.Scale(scaleDelta23Frames, initialScale), t);
    //            }
    //            else
    //            {
    //                // Interpolation between 23 frames and 60 frames
    //                float t = (normalizedTime - 23f / 60f) / (37f / 60f);
    //                transform.position = Vector3.Lerp(initialPosition + positionDelta23Frames, initialPosition + positionDelta60Frames, t);
    //                transform.rotation = Quaternion.Slerp(initialRotation * rotationDelta23Frames, initialRotation * rotationDelta60Frames, t);
    //                transform.localScale = Vector3.Lerp(initialScale + Vector3.Scale(scaleDelta23Frames, initialScale), initialScale + Vector3.Scale(scaleDelta60Frames, initialScale), t);
    //            }
    //        }

    //        StartCoroutine(AnimationCoroutine());

    //        System.Collections.IEnumerator AnimationCoroutine()
    //        {
    //            while (timeElapsed < animationDuration)
    //            {
    //                UpdateAnimation();
    //                yield return null;

    //            }
    //            Destroy(gameObject,1f);

    //        }
    //    }
    //}

    protected IEnumerator 濒死扣血()
    {
        float timer = 0f;
        while (true)
        {
            if (alive)
            {
                beAttacked(最大血量 / 10, 2, -1);
                yield return new WaitForSeconds(1f);
                timer += 1f;

                if (timer >= 10f && alive)
                {
                    die();
                    ZombieManagement.instance.minusZombieNumAll(gameObject);
                    yield break;
                }
            }
            else
            {
                yield break;
            }
        }
    }

    public void fadeDisappear()
    {
        if(hasFaded)
            return;
        hasFaded = true;
        myAnimator.enabled = false;
        StartCoroutine(FadeBlendColor());
    }
    public void disappear()
    {
        Destroy(gameObject);
    }
    #endregion

    #region 设置图层
    public virtual void setPosRow(int pos)
    {
        //设置所在行
        pos_row = pos;

        //设置顺序图层及显示顺序
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        GetComponent<SortingGroup>().sortingLayerName = "Zombie-" + pos_row;
        //GetComponent<SortingGroup>().sortingOrder += orderOffset * 20;
        //orderOffset++;
    }

    public virtual void SetSortingOrder(int 排序)
    {
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        GetComponent<SortingGroup>().sortingOrder += 排序 * 50;
        
    }
    #endregion

    #region 材质
    public virtual void TriggerHighlight(int bulletType)
    {
        if(!GameManagement.isPerformance)
        {
            if(高亮 == null)
            {
                高亮 = StartCoroutine(HighlightCoroutine(bulletType));
            }
            else
            {
                StopCoroutine(高亮);
                高亮 = StartCoroutine(HighlightCoroutine(bulletType));
            }
            
        }
        
    }

    public virtual IEnumerator HighlightCoroutine(int bulletType)
    {
        float duration = 0.15f; // 高亮和恢复的持续时间
        float startTime = Time.time;

        // 获取当前透明度
        float currentAlpha = 0f;

        // 透明度增加：从当前透明度到最大值 1
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(currentAlpha, 0.2f, (Time.time - startTime) / duration); // 从当前透明度到 1
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // 更新透明度
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }
        startTime = Time.time;

        // 透明度减少：从 1 到 0
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(0.2f, 0f, (Time.time - startTime) / duration); // 从 1 到 0
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // 更新透明度
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }

        // 确保透明度最终为 0
        foreach (Renderer renderer in allRenderers)
        {
            if (renderer != null && renderer.gameObject.name != "Shadow")
            {
                Material mat = renderer.material;
                Color currentColor = mat.GetColor("_Color");
                currentColor.a = 0f;  // 最终透明度为 0
                mat.SetColor("_Color", currentColor);
            }
        }
    }

    private IEnumerator FadeBlendColor()//死亡时逐渐消失
    {
        yield return new WaitForSeconds(0.5f);

        float elapsed = 0f;

        // 在开始前，将所有材质的初始颜色缓存下来
        var initialColors = new Color[allRenderers.Length][];
        for (int i = 0; i < allRenderers.Length; i++)
        {
            var mats = allRenderers[i].materials;
            initialColors[i] = new Color[mats.Length];
            for (int j = 0; j < mats.Length; j++)
            {
                if (mats[j].HasProperty("_BlendColor"))
                    initialColors[i][j] = mats[j].GetColor("_BlendColor");
                else
                    initialColors[i][j] = Color.clear; // 占位
            }
        }

        // 渐变过程
        while (elapsed < 1f)
        {
            float t = elapsed / 1f; // 从 0 到 1
            for (int i = 0; i < allRenderers.Length; i++)
            {
                var mats = allRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    if (mats[j].HasProperty("_BlendColor"))
                    {
                        Color c = initialColors[i][j];
                        // 把 alpha 从初始 (通常为1) lerp 到 0
                        c.a = Mathf.Lerp(initialColors[i][j].a, 0f, t);
                        mats[j].SetColor("_BlendColor", c);
                    }
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 确保最终完全透明
        for (int i = 0; i < allRenderers.Length; i++)
        {
            var mats = allRenderers[i].materials;
            for (int j = 0; j < mats.Length; j++)
            {
                if (mats[j].HasProperty("_BlendColor"))
                {
                    Color c = mats[j].GetColor("_BlendColor");
                    c.a = 0f;
                    mats[j].SetColor("_BlendColor", c);
                }
            }
        }

        disappear();
    }
    #endregion

    #region 碰撞箱
    public virtual void 关闭碰撞箱()
    {
    }

    public virtual void 开启碰撞箱()
    {
    }
    #endregion

    #region 铁门代码
    public void loadDoorStatus()
    {
        铁门断臂控制();
        if (!level2ArmorIsDrop)
        {
            if (!二类半破损已经切换)
            {
                if (level2ArmorHealth <= level2ArmorMaxHealth / 3 * 2)
                {
                    二类半破损已经切换 = true;
                    foreach (GameObject gameObject in 铁门)
                    {
                        if (gameObject != null)
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = DoorSprite[1];
                        }
                    }
                }
            }
            if (!二类完全破损已经切换)
            {
                if (level2ArmorHealth <= level2ArmorMaxHealth / 3 * 1)
                {
                    二类完全破损已经切换 = true;
                    foreach (GameObject gameObject in 铁门)
                    {
                        if (gameObject != null)
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = DoorSprite[2];
                        }
                    }
                }
                
            }
            if (level2ArmorHealth <= 0)
            {
                铁门无门行走显示逻辑();
                level2ArmorIsDrop = true;
                myAnimator.SetBool("LostDoor", true);
                if (newZombie)
                {
                    GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombiePendantDrop);
                    go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                    go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(铁门[0].GetComponent<SpriteRenderer>().sprite);
                    go.transform.position = 铁门[0].transform.position;
                    go.transform.rotation = Quaternion.identity;
                }
            }
        }
        
        if(血量 <= 0)
        {
            铁门无门行走显示逻辑();
        }
    }
    public void 铁门行走显示逻辑()
    {
        SetActiveForObjects(铁门全部, false);
        SetActiveForObjects(铁门僵尸行走显示的, true);
        铁门断臂控制();
    }

    public void 铁门啃咬显示逻辑()
    {
        SetActiveForObjects(铁门全部, false);
        SetActiveForObjects(铁门僵尸啃咬显示的, true);
        铁门断臂控制();
    }

    public void 铁门无门行走显示逻辑()
    {
        SetActiveForObjects(铁门全部, false);
        SetActiveForObjects(铁门僵尸无铁门行走时显示的, true);
        SetActiveForObjects(铁门, false);
        铁门断臂控制();
    }
    public void 铁门销毁()
    {
        SetActiveForObjects(铁门, false);
        foreach (GameObject objects in 铁门)
        {
            if (objects != null)
            {
                Destroy(objects);
            }
        }
    }

    public void 铁门断臂控制()
    {
        if (armIsDrop)
        {
            SetActiveForObjects(铁门断臂时强制不显示, false);
            SetActiveForObjects(铁门断臂时强制显示, true);
        }
    }
    private void SetActiveForObjects(GameObject[] objects, bool isActive)
    {
        foreach (GameObject gameObject in objects)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(isActive);
            }
        }
    }

    public void 铁门初始化()
    {
        Transform[] allChildrens = GetComponentsInChildren<Transform>(true);

        if (!newZombie)
        {
            铁门僵尸啃咬显示的 = GetGameObjectsByNames(allChildrens, new string[]
        {
        "Zombie_outerarm_upper", "Zombie_outerarm_lower", "Zombie_outerarm_hand",
        "Zombie_innerarm_upper", "Zombie_innerarm_lower", "Zombie_innerarm_hand",
        "ScreenDoor_Eating"
        });

            铁门僵尸行走显示的 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor", "ScreenDoor_outerarm", "Screen_innerhand", "Screen_innerarm"
            });

            铁门僵尸无铁门行走时显示的 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "Zombie_outerarm_upper", "Zombie_outerarm_lower", "Zombie_outerarm_hand",
        "Zombie_innerarm_upper", "Zombie_innerarm_lower", "Zombie_innerarm_hand",
        "ScreenDoor"
            });

            铁门 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor", "ScreenDoor_Eating"
            });

            铁门断臂时强制不显示 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor_outerarm", "ScreenDoor_outerarm", "Screen_innerarm","Screen_innerhand"
            });

            铁门断臂时强制显示 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "Zombie_outerarm_upper"
            });
        }
        else
        {

            铁门僵尸行走显示的 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_SCREENDOOR1", "ZOMBIE_OUTERARM_SCREENDOOR", "ZOMBIE_INNERARM_SCREENDOOR_HAND", "ZOMBIE_INNERARM_SCREENDOOR"
            });

            铁门僵尸无铁门行走时显示的 = GetGameObjectsByNames(allChildrens, new string[]//也是铁门僵尸啃咬显示
            {
        "ZOMBIE_OUTERARM_UPPER", "ZOMBIE_OUTERARM_LOWER", "ZOMBIE_OUTERARM_HAND",
        "ZOMBIE_INNERARM_UPPER", "ZOMBIE_INNERARM_LOWER", "ZOMBIE_INNERARM_HAND",
        "ZOMBIE_SCREENDOOR1"
            });

            铁门僵尸啃咬显示的 = 铁门僵尸无铁门行走时显示的;

            铁门 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_SCREENDOOR1"
            });

            铁门断臂时强制不显示 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_OUTERARM_SCREENDOOR", "ZOMBIE_INNERARM_SCREENDOOR","ZOMBIE_INNERARM_SCREENDOOR_HAND"
            });

            铁门断臂时强制显示 = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_OUTERARM_UPPER"
            });
        }
        
    }
    void AddUnique(GameObject[] sourceArray, List<GameObject> list)
    {
        if (sourceArray != null)
        {
            foreach (GameObject item in sourceArray)
            {
                if (!list.Contains(item)) // 如果list不包含这个元素
                {
                    list.Add(item);
                }
            }
        }
    }
    private GameObject[] GetGameObjectsByNames(Transform[] allChildrens, string[] names)
    {
        List<GameObject> foundObjects = new List<GameObject>();
        foreach (var name in names)
        {
            foreach (var child in allChildrens)
            {
                var foundObject = FindChildByNameRecursive(child, name);
                if (foundObject != null)
                {
                    foundObjects.Add(foundObject);
                    break;
                }
            }
        }

        return foundObjects.ToArray();
    }
    public GameObject FindChildByNameRecursive(Transform parent, string name)
    {
        if (parent == null) return null;
        if (parent.name == name)
        {
            return parent.gameObject;
        }
        foreach (Transform child in parent)
        {
            var foundObject = FindChildByNameRecursive(child, name);
            if (foundObject != null)
            {
                return foundObject;
            }
        }

        return null;
    }

    private GameObject FindChildByName(Transform[] allChildrens, string name)
    {
        foreach (var child in allChildrens)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }
    #endregion

    #region 血量显示

    private string 血量文本()
    {
        string healthDisplay = "";
        if (血量 > 0)
        {
            healthDisplay += "本体：" + 血量 + "/" + 最大血量 + "\n";
        }
        if (level1ArmorHealth > 0)
        {
            healthDisplay += "一类：" + level1ArmorHealth + "/" + level1ArmorMaxHealth + "\n";
        }
        if (level2ArmorHealth > 0)
        {
            healthDisplay += "二类：" + level2ArmorHealth + "/" + level2ArmorMaxHealth + "\n";
        }
        if (debuff.中毒 > 0)
        {
            healthDisplay += "毒素：" + debuff.中毒;
        }
        return healthDisplay;
    }

    public void 加载血量文本()
    {
        血量显示.text = 血量文本();
    }

    public void 变更血量显示(bool b)
    {
        血量显示.gameObject.SetActive(b);
    }


    #endregion
}

public struct 僵尸debuff
{
    public float 减速;//减速时间
    public int 中毒;//中毒层数
    public bool 魅惑;
    public bool 狂暴;
    public bool 冻结;
    
}

public struct 僵尸buff {
    public bool 隐匿;
    public int 坚韧;
    public bool 免疫秒杀;
    public bool 免疫冰冻;
    public bool 免疫魅惑;
}