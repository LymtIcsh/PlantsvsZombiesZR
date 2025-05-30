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
    #region ����

    public bool ���Կ�ҧ = true;

    protected bool armIsDrop = false;
    protected bool headIsDrop = false;
    protected bool level1ArmorIsDrop = false;
    protected bool level2ArmorIsDrop = false;
    
    public bool �����ཀྵʬ = false;

    public int zombieID;
    /// <summary>
    /// 0Ϊ��ͨ��ʬ��1Ϊ·�ϣ�2Ϊ��Ͱ
    /// </summary>
    public int ������ʬ����;

    //[HideInInspector]
    public int pos_row;   //λ�ڵڼ���

    //�������
    [HideInInspector]
    public int Ѫ��;   //Ѫ��

    [HideInInspector]
    public int ���Ѫ��;

    /// <summary>
    /// һ�����
    /// </summary>
    [HideInInspector]
    public int level1ArmorHealth;
    [HideInInspector]
    public int level1ArmorMaxHealth;



    /// <summary>
    /// �������
    /// </summary>
    [HideInInspector]
    public int level2ArmorHealth;
    [HideInInspector]
    public int level2ArmorMaxHealth;

    protected bool һ����߰������Ѿ��л� = false;
    protected bool һ�������ȫ�����Ѿ��л� = false;
    [HideInInspector]
    public bool ����������Ѿ��л� = false;
    [HideInInspector]
    public bool ������ȫ�����Ѿ��л� = false;

    [HideInInspector]
    public bool alive = true;

    //�������
    [HideInInspector]
    public int ������;  //������
    protected Plant attackPlant;   //��ǰ������ֲ���Plant���
    protected Zombie attackZombie;

    protected Animator myAnimator;   //�������
    

    [HideInInspector]
    public int audioIndex = 1;

    //static int orderOffset = 0;

    [HideInInspector]
    public Material highlightMaterial; // ��Inspector��ָ���ĸ߹����

    [HideInInspector]
    public const float highlightDuration = 0.06f; // �߹�Ч���ĳ���ʱ�䣨�룩

    [HideInInspector]
    public Renderer[] allRenderers;    // ���� Renderer ���

    private float ����ʱ�� = 5f; // �����һ�ζ����ʱ��Ϊ5�루���Ը������������
    private Coroutine ����Э��; // �����洢Э�̵�����

    private bool �ж�Ч���� = false;
    private bool ����Ч���� = false;


    public Sprite coneBroken1;//·������
    public Sprite coneBroken2;
    public Sprite coneBroken3;
    public GameObject coneDrop;

    public Sprite bucketBroken1; //��Ͱ����
    public Sprite bucketBroken2;
    public Sprite bucketBroken3;
    public GameObject bucketDrop;

    public Sprite fullHead;

    public Sprite brokenArm;//���˵��ֱ�ͼƬ

    public GameObject zombieHeadDrops;
    public GameObject zombieArmDrops;


    public bool dontHaveDropHead;

    public Coroutine ����;

    public bool isEating;

    public ZombieForestSlider zombieForestSlider;

    public GameObject ���ڿ�ҧĿ��;

    //[HideInInspector]
    public bool dying = false;

    public Sprite[] DoorSprite;//����ͼƬ
    private GameObject[] ���Ž�ʬ��ҧ��ʾ��;
    private GameObject[] ���Ž�ʬ������ʾ��;
    private GameObject[] ���Ž�ʬ����������ʱ��ʾ��;
    private GameObject[] ����;
    private GameObject[] ���Ŷϱ�ʱǿ�Ʋ���ʾ;
    private GameObject[] ���Ŷϱ�ʱǿ����ʾ;
    private GameObject[] ����ȫ��;

    private float ���ٶȳ��� = 1f;
    private float �����ٶȳ��� = 1f;
    private float ����ٶȳ��� = 1f;
    private float �ؿ�������� = 1f;
    [SerializeField]
    private float _�����ٶȳ��� = 1f;

    
    protected virtual float �����ٶȳ���//���ڸ�д
    {
        get => _�����ٶȳ���;
        set => _�����ٶȳ��� = value;
    }

    private TMP_Text Ѫ����ʾ;

    public ��ʬdebuff debuff;
    public ��ʬbuff buff;

    public bool newZombie = false;

    private bool hasFaded = false;

    private bool isEntrance = false;
    #endregion

    #region ��ʼʱִ��
    protected virtual void Awake()
    {
        //highlightMaterial = Resources.Load<Material>("Shader/highLight");
        myAnimator = gameObject.GetComponent<Animator>();
        allRenderers = GetComponentsInChildren<Renderer>(true);

        if (GameManagement.instance != null && GameManagement.instance.��Ϸ���� && ZombieManagement.instance != null)
        {
            ZombieManagement.instance.GetComponent<ZombieManagement>().addZombieNumAll(gameObject);
        }
        else
        {
            չʾ������ʼ��();
        }

        TMP_Text ����Ѫ������ = Resources.Load<TMP_Text>("Prefabs/Effects/Ѫ����ʾ/��ͨ��ʬѪ����ʾ");
        Ѫ����ʾ = Instantiate(����Ѫ������, transform.position, Quaternion.identity, transform);
        Ѫ����ʾ.gameObject.SetActive(false);

        ����ٶȳ��� = Random.Range(0.8f, 1.2f);
        ���ض����ٶ�();
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
        Ѫ����ʾ.gameObject.SetActive(true);
        Vector3 parentScale = transform.localScale;
        float safeX = parentScale.x != 0 ? parentScale.x : 1f;
        float safeY = parentScale.y != 0 ? parentScale.y : 1f;
        float safeZ = parentScale.z != 0 ? parentScale.z : 1f;
        Ѫ����ʾ.transform.localScale = new Vector3(
            0.05f / safeX,
            0.05f / safeY,
            0.05f / safeZ
        );
        Ѫ����ʾ.gameObject.SetActive(false);
        Ѫ����ʾ.gameObject.SetActive(true);
        Ѫ����ʾ.gameObject.SetActive(GameManagement.�Ƿ���ʾѪ��);

        ���ų�ʼ��();

        zombieForestSlider = GameManagement.instance.zombieForestSlider;

        ��Ϸ������ʼ��();

        Ѫ�� = ZombieStructManager.GetZombieStructById(zombieID).BaseHP;
        level1ArmorHealth = ZombieStructManager.GetZombieStructById(zombieID).Armor1HP;
        level2ArmorHealth = ZombieStructManager.GetZombieStructById(zombieID).Armor2HP;
        level1ArmorMaxHealth = level1ArmorHealth;
        level2ArmorMaxHealth = level2ArmorHealth;

        ������ = 50;

        switch (������ʬ����)
        {
            case 1: loadCone(1); break;
            case 2: loadBucket(1); break;
            default: break;
        }
        if(�����ཀྵʬ)
        {
            List<GameObject> combinedList = new List<GameObject>();
            AddUnique(���Ž�ʬ��ҧ��ʾ��, combinedList);
            AddUnique(���Ž�ʬ������ʾ��, combinedList);
            AddUnique(���Ž�ʬ����������ʱ��ʾ��, combinedList);
            AddUnique(����, combinedList);
            AddUnique(���Ŷϱ�ʱǿ�Ʋ���ʾ, combinedList);
            AddUnique(���Ŷϱ�ʱǿ����ʾ, combinedList);
            ����ȫ�� = combinedList.ToArray();
            ����������ʾ�߼�();

        }


        if(GameManagement.levelData.LevelType == levelType.TheDreamOfPotatoMine)
        {
            �ؿ�������� *= 1.5f * GameManagement.GameDifficult;
        }
        //�������ٶ�����
        
        ���ض����ٶ�();


        activate();
        
        
        ���Ѫ�� = Ѫ��;

        InvokeRepeating("���㶾��", 0, 2f);

        ����Ѫ���ı�();
    }


    protected virtual void activate()
    {
        Invoke("doAfterStartSomeTimes", Random.Range(15, 80));
        gameObject.SetActive(true);
    }

    protected virtual void doAfterStartSomeTimes()//����ɭ�ֽ�ʬ10 - 30������ɲݴԲ�ɱ���Լ�
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
            if (ZombieManagement.instance != null && ZombieManagement.instance.isActiveAndEnabled && this != null && gameObject != null && !GameManagement.instance.��Ϸ����) 
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

    #region ��ҧ�빥��
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(���Կ�ҧ && !dying)
        {
            if (!debuff.�Ȼ�
            && collision.tag == "Plant"
            && myAnimator.GetBool("Attack") == false
            )
            {
                attackPlant = collision.GetComponent<Plant>();
                attackZombie = collision.GetComponent<Zombie>();
                if(attackPlant != null && attackPlant.row == pos_row && attackPlant.ֲ������ != PlantType.�ش���ֲ�� && attackPlant.�ɱ�����)
                {

                }
                else if(attackZombie != null && attackZombie.pos_row == pos_row && attackZombie.debuff.�Ȼ� != debuff.�Ȼ�)
                {

                }
                else
                {
                    return;
                }

                ���ڿ�ҧĿ�� = collision.gameObject;
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Attack", true);
                isEating = true;

                if (�����ཀྵʬ)
                {
                    ���ſ�ҧ��ʾ�߼�();
                }
            }
            else if (collision.tag == "Zombie"
            && collision.GetComponent<Zombie>().pos_row == pos_row
            && collision.GetComponent<Zombie>().debuff.�Ȼ� != debuff.�Ȼ�
            && myAnimator.GetBool("Attack") == false
            )
            {
                ���ڿ�ҧĿ�� = collision.gameObject;
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Attack", true);
                isEating = true;
                attackZombie = collision.GetComponent<Zombie>();

                if (�����ཀྵʬ)
                {
                    ���ſ�ҧ��ʾ�߼�();
                }

            }
        }
        
        if (!debuff.�Ȼ� && collision.tag == "GameOverLine" && !dying && alive)
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
        if (collision.gameObject == ���ڿ�ҧĿ��)
        {
            myAnimator.SetBool("Attack", false);
            myAnimator.SetBool("Walk", true);
            isEating = false;
            ���ڿ�ҧĿ�� = null;
            attackPlant = null;
            attackZombie = null;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = true;

            if (�����ཀྵʬ)
            {
                if (!level2ArmorIsDrop)
                {
                    ����������ʾ�߼�();
                }
                else
                {
                    ��������������ʾ�߼�();
                }
            }
        }
        //else if (collision.tag == "��������")
        //{
        //    die();
        //}
    }

    //protected virtual void OnTriggerStay2D(Collider2D collision)
    //{
    //    ////if (���Կ�ҧ &&
    //    ////    !dying &&
    //    ////    !isEating && collision.tag == "Plant" && collision.GetComponent<Plant>().row == pos_row && collision.GetComponent<Plant>().plantType == 0 && collision.GetComponent<Plant>().canBeAttacked)
    //    ////{
    //    ////    myAnimator.SetBool("Walk", false);
    //    ////    myAnimator.SetBool("Attack", true);
    //    ////    isEating = true;
    //    ////    plant = collision.GetComponent<Plant>();
    //    ////    ���ڿ�ҧĿ�� = collision.gameObject;
    //    ////    ��ҧĿ�귽�� = ���ڿ�ҧĿ��.GetComponent<Plant>();
    //    ////    if (isDoorZombie)
    //    ////    {
    //    ////        ���ſ�ҧ��ʾ�߼�();
    //    ////    }
    //    ////}
    //    //if (isEating && plant.row != pos_row)
    //    //{
    //    //    myAnimator.SetBool("Attack", false);
    //    //    myAnimator.SetBool("Walk", true);
    //    //    isEating = false;
    //    //    ���ڿ�ҧĿ�� = null;
    //    //    plant = null;
    //    //    if (isDoorZombie)
    //    //    {
    //    //        if (!level2ArmorIsDrop)
    //    //        {
    //    //            ����������ʾ�߼�();
    //    //        }
    //    //        else
    //    //        {
    //    //            ��������������ʾ�߼�();
    //    //        }
    //    //    }
    //    //}
    //}
    public virtual void attack()
    {
        if (attackPlant != null && !dying)
        {
            attackPlant.beAttacked(������, "beEated", gameObject);
        }
        else if(attackZombie != null && !dying)
        {
            attackZombie.beAttacked(������, 1, -1);
        }
    }
    #endregion

    #region �ܻ�
    public virtual void beAttacked(int hurt, int BulletType ,int AttackedMusicType)//AttackedMusicTypeΪ2ʱ�ǵش̹����������ű���������Ч��Ϊ-1ʱ��������Ч
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

        if (BulletType == 0) // �����˺����۳�����Ѫ��
        {
            HandleBodyDamage(hurt); // ���ñ������˵Ĵ�����
        }
        else if (BulletType == 1 || BulletType==3) // һ���˺�
        {
            if (level2ArmorHealth > 0) // ������������Ѫ��
            {
                // ��������ȿ�Ѫ
                int level2Damage = Mathf.Min(hurt, level2ArmorHealth);
                level2ArmorHealth -= level2Damage;
                hurt -= level2Damage; // ʣ���˺���������
                HandleLevel2ArmorDamage(level2Damage); // ���ö���������˴���

                if (hurt > 0) // ��������˺�δ������
                {
                    // һ����߿�Ѫ
                    int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                    level1ArmorHealth -= level1Damage;
                    hurt -= level1Damage; // ʣ���˺���������
                    HandleLevel1ArmorDamage(level1Damage); // ����һ��������˴���

                    if (hurt > 0) // ��������˺�δ������
                    {
                        HandleBodyDamage(hurt); // �����۳�����Ѫ��
                    }
                }
            }
            else // �������û��Ѫ����ֱ�ӿ�һ����ߺͱ���
            {
                // һ������ȿ�Ѫ
                int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                level1ArmorHealth -= level1Damage;
                hurt -= level1Damage; // ʣ���˺���������
                HandleLevel1ArmorDamage(level1Damage); // ����һ��������˴���

                if (hurt > 0) // ��������˺�δ������
                {
                    HandleBodyDamage(hurt); // �����۳�����Ѫ��
                }
            }
        }
        else if (BulletType == 2) // �����˺�
        {
            if (level1ArmorHealth > 0) // ���һ�������Ѫ��
            {
                // �ȿ۳�һ����ߵ�Ѫ��
                int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                level1ArmorHealth -= level1Damage;
                hurt -= level1Damage; // ʣ���˺���������
                HandleLevel1ArmorDamage(level1Damage); // ����һ��������˴���
            }

            if (hurt > 0) // ���һ������Ѿ�û��Ѫ���������۳�����Ѫ��
            {
                HandleBodyDamage(hurt); // �����۳�����Ѫ��
            }
        }
        loadArmorStatus();
    }

    // һ������ܵ��˺�ʱ���õĴ�����
    protected virtual void HandleLevel1ArmorDamage(int hurt)
    {
        // ����������һ��������˺���߼������粥��һ����ߵ��𻵶�������Ч��
        //Debug.Log($"һ������ܵ� {hurt} ���˺�");
    }

    // ��������ܵ��˺�ʱ���õĴ�����
    protected virtual void HandleLevel2ArmorDamage(int hurt)
    {
        // ���������Ӷ���������˺���߼������粥�Ŷ�����ߵ��𻵶�������Ч��
        //Debug.Log($"��������ܵ� {hurt} ���˺�");
    }

    // �����ܵ��˺�ʱ���õĴ�����
    protected virtual void HandleBodyDamage(int hurt)
    {
        // ������Դ��������˺���߼�������۳�Ѫ��������������Ч��������
        Ѫ�� -= hurt;
        //Debug.Log($"�����ܵ� {hurt} ���˺���ʣ��Ѫ��: {bloodVolume}");
        // ������������ӱ������˺�������߼������粥�����˶�������Ч��
    }
    //�����ˣ������湥��ʱ����
    public virtual void beBurned(int damage)
    {
        beAttacked(damage, 1, 1);
        �л�����״̬(false);
    }
    public virtual void beSquashed()
    {
        if (buff.����) return;
        beAttacked(1800, 1, -1);
        if (Ѫ�� <= 0 && !alive)
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

    public void �������(){
        level1ArmorHealth += buff.���� * 50;
    }

    public void beAshAttacked()
    {
        if(Ѫ�� <= 1800)
        {
            Vector3 offset = new Vector3(0.137f, -0.088f, 0f); // z�ᱣ�ֲ���
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

    #region ��Ч����
    // ���ű�������Ч
    public virtual void playAudioOfBeingAttacked(int bulletType)
    {
        if (�����ཀྵʬ && !level2ArmorIsDrop && bulletType == 1)
        {
            // ʹ�� AudioManager ������Ч
            AudioManager.Instance.PlaySoundEffect(11); // ��Ӧ shieldhit ��Ч
        }
        else
        {
            if (!level1ArmorIsDrop)
            {
                switch (������ʬ����)
                {
                    case 0:
                        // ʹ�� AudioManager ���Ų�ͬ����Ч
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // ��Ӧ bodyhit1 �� bodyhit2
                        break;
                    case 1:
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 9 : 10); // ��Ӧ conehit1 �� conehit2
                        break;
                    case 2:
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 11 : 12); // ��Ӧ shieldhit1 �� shieldhit2
                        break;
                }
            }
            else
            {
                // ʹ�� AudioManager ����Ĭ�ϵ���Ч
                AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // ��Ӧ bodyhit1 �� bodyhit2
            }
        }

        // �л���Ч����
        audioIndex = (audioIndex == 1) ? 2 : 1;
    }

    // ��������������Ч
    public virtual void playAudioOfBodyBeingAttacked() // �ش̹���ʹ�ã�������������
    {
        // ʹ�� AudioManager ������Ч
        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // ��Ӧ bodyhit1 �� bodyhit2

        // �л���Ч����
        audioIndex = (audioIndex == 1) ? 2 : 1;
    }

    // ���Ž�ʬ��ҧ����Ч
    public virtual void PlayEatAudio()
    {
        // ��������Ŀ�ҧ��Ч (chomp1 �� chomp2)
        AudioManager.Instance.PlaySoundEffect(Random.Range(13, 15)); // ��Ӧ chomp1 �� chomp2
    }

    public virtual void fallDown()
    {
        AudioManager.Instance.PlaySoundEffect(20);
    }
    #endregion

    #region �ж���������״̬�л�
    private void �л�����״̬(int ״̬����)
    {
        switch(״̬����)
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

    private void �л�����״̬(bool ��ʼ)//0Ϊ�����1Ϊ��ʼ
    {
        if (����Ч���� == ��ʼ) return; // �����ǰ״̬�Ѿ���Ŀ��״̬����ֱ�ӷ���
        if(��ʼ)
        {
            �л��ж�״̬(false);
        }
        ����Ч���� = ��ʼ;
        �����ٶȳ��� = ��ʼ ? 0.5f : 1f;  // ʹ����Ԫ��������򻯸�ֵ
        �л�����״̬(��ʼ ? 1 : 0);
        ���ض����ٶ�();
    }
    public virtual void ���Ӽ���() // ���ڼ���
    {
        if (����Э�� == null)
        {
            debuff.���� = ����ʱ��;
            ����Э�� = StartCoroutine(����Ч��Э��(debuff.����));
        }
        else
        {
            debuff.���� = ����ʱ��;
            StopCoroutine(����Э��);
            ����Э�� = StartCoroutine(����Ч��Э��(debuff.����));
        }
    }
    private IEnumerator ����Ч��Э��(float ����ʱ��)
    {
        �л�����״̬(true);
        float elapsedTime = 0f;
        while (elapsedTime < ����ʱ��)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        �������״̬();
    }
    protected void �������״̬()
    {
        �л�����״̬(false);
    }

    private void �л��ж�״̬(bool ��ʼ)//0Ϊ�����1Ϊ��ʼ
    {
        if (�ж�Ч���� == ��ʼ) return;
        if(��ʼ)
        {
            �л�����״̬(false);
            �л���״̬(false);
        }
        if(!��ʼ)
        {
            debuff.�ж� = 0;
        }
        �ж�Ч���� = ��ʼ;

        
        �л�����״̬(��ʼ ? 3 : 0);
      
        ���ض����ٶ�();

        ����Ѫ���ı�();
    }

    public void �����ж�(int �����ж�����)
    {
        debuff.�ж� += �����ж�����;

        �л���״̬(false);


       
        for(int i = 0;i < �����ж�����;i++)
        {
            Invoke("DecreasePoisonLayer", 6f);
        }
        if(debuff.�ж� > 60)
        {
            SetAchievement.SetAchievementCompleted("�Ҳ��Ƕ���");
        }
        if (debuff.�ж� > 0 && !�ж�Ч����)
        {
            �л��ж�״̬(true);
            �ж�Ч���� = true;
        }

        ����Ѫ���ı�();
    }
    public void ��������(int ��������)
    {
        if (debuff.�ж� > 0 && �������� > 0)
        {
            beAttacked(debuff.�ж� * ���Ѫ�� / 100 * ��������, 3, -1);
        }
        else
        {
            return;
        }
    }
    public void DecreasePoisonLayer()
    {
        debuff.�ж� -= 1;
        if (debuff.�ж� < 0)
            debuff.�ж� = 0;

        if (debuff.�ж� == 0 && �ж�Ч����)
        {
            �л��ж�״̬(false);
        }

        ����Ѫ���ı�();
    }
    public void DecreasePoisonLayer(int ���ٵĲ���)
    {
        debuff.�ж� -= ���ٵĲ���;
        if (debuff.�ж� < 0)
            debuff.�ж� = 0;

        if (debuff.�ж� == 0 && �ж�Ч����)
        {
            �л��ж�״̬(false);
        }

        ����Ѫ���ı�();
    }
    public void ���㶾��()
    {
        if(debuff.�ж� > 0)
        {
            beAttacked(debuff.�ж� * 3, 1,-1);
        }
        else
        {
            return;
        }
    }

    public void �л���״̬(bool ��)//0Ϊ�����1Ϊ��ʼ
    {
        
        if(debuff.�� == ��)
        {
            return;
        }
        else
        {
            if(��)
            {
                if (��)
                {
                    �л�����״̬(false);
                    �л��ж�״̬(false);
                }
                �л�����״̬(5);
                debuff.�� = true;
                ���ٶȳ��� = 3f;
            }
            else
            {
                �л�����״̬(0);
                debuff.�� = false;
                ���ٶȳ��� = 1f;
            }
            
        }
        ���ض����ٶ�();

    }

    public void �л��Ȼ�״̬()//�Ȼ�ֻ�ܽ��룬�޷��˳�
    {
        if (debuff.�Ȼ�)
            return;
        debuff.�Ȼ� = true;
        transform.Rotate(0f, 180f, 0f);
        Ѫ����ʾ.transform.Rotate(0f, 180f, 0f);
        �л��ж�״̬(false);
        �л�����״̬(false);
        �л���״̬(false);
        �л�����״̬(4);
        ZombieManagement.instance.minusZombieNumAll(gameObject);
        gameObject.tag = "Plant";
        //myAnimator.SetBool("Attack", false);
        //myAnimator.SetBool("Walk", true);
        //isEating = false;
        //���ڿ�ҧĿ�� = null;
        //attackPlant = null;
        //if (�����ཀྵʬ)
        //{
        //    if (!level2ArmorIsDrop)
        //    {
        //        ����������ʾ�߼�();
        //    }
        //    else
        //    {
        //        ��������������ʾ�߼�();
        //    }
        //}
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }

    public void չʾ������ʼ��()
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
                Debug.LogWarning($"���� '{randomAnim}' �����ڣ��޷�����");
            }
        }
    }

    public void ��Ϸ������ʼ��()
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
                Debug.LogWarning($"���� '{randomAnim}' �����ڣ��޷�����");
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



    private void ���ض����ٶ�()
    {
        if(myAnimator != null)
        {
            myAnimator.speed = ���ٶȳ��� * �����ٶȳ��� * ����ٶȳ��� * �ؿ�������� * �����ٶȳ���;
        }
    }

    #endregion

    #region ���ز�ͬ״̬�����Ŷ��������Ŵ����У�
    protected virtual void hideHead()
    {
        if(!dying)
        {
            dying = true;

            StartCoroutine(������Ѫ());

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
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true)) // �ڶ�����������Ϊtrue���������������壨����δ����ģ�
        {
            if (child.name == beFind)
            {
                return child;
            }
        }
        return null; // û���ҵ���Ϊ������
    }
    public void loadCone(int loadType)//���ݲ�ͬ�˺�����·��
    {
        if(!newZombie)
        {
            // ������Ϊ "Cone" �����������壨����δ����ģ�
            Transform coneTransform = FindInChildren(transform, "Cone");

            if (coneTransform != null)
            {
                // ��ȡ���������SpriteRenderer���
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
            // ������Ϊ "Cone" �����������壨����δ����ģ�
            Transform coneTransform = FindInChildren(transform, "ZOMBIE_CONE1");

            if (coneTransform != null)
            {
                // ��ȡ���������SpriteRenderer���
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
    public virtual void loadBucket(int loadType)//���ݲ�ͬ�˺�����Ͱ
    {
        if(!newZombie)
        {
            // ������Ϊ "Cone" �����������壨����δ����ģ�
            Transform bucketTransform = FindInChildren(transform, "Bucket");

            if (bucketTransform != null)
            {
                // ��ȡ���������SpriteRenderer���
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
            // ������Ϊ "Cone" �����������壨����δ����ģ�
            Transform bucketTransform = FindInChildren(transform, "ZOMBIE_BUCKET1");

            if (bucketTransform != null)
            {
                // ��ȡ���������SpriteRenderer���
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

    protected virtual void dropArm()//�ֱ۵���
    {
        if (!armIsDrop)
        {
            armIsDrop = true;
            AudioManager.Instance.PlaySoundEffect(59);
            if (!newZombie)
            {
                // ������Ϊ "Cone" �����������壨����δ����ģ�
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
        switch (������ʬ����)
        {
            case 1:
                if (!level1ArmorIsDrop)
                {
                    if (!һ����߰������Ѿ��л�)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 * 2)
                        {
                            һ����߰������Ѿ��л� = true;
                            loadCone(2);
                        }

                    }
                    if (!һ�������ȫ�����Ѿ��л�)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3)
                        {
                            һ�������ȫ�����Ѿ��л� = true;
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
                    if (!һ����߰������Ѿ��л�)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 * 2)
                        {
                            һ����߰������Ѿ��л� = true;
                            loadBucket(2);
                        }

                    }
                    if (!һ�������ȫ�����Ѿ��л�)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3)
                        {
                            һ�������ȫ�����Ѿ��л� = true;
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
        if (�����ཀྵʬ)
        {
            loadDoorStatus();
        }

        if (Ѫ�� <= ���Ѫ�� / 2)//����
        {
            dropArm();
        }
        if (Ѫ�� <= ���Ѫ�� / 3)//��ͷ  
        {
            //����ͷ
            hideHead();
        }
        if (Ѫ�� <= 0 && alive == true)
        {
            die();
        }
        ����Ѫ���ı�();
        
        
    }

    public virtual void die()
    {
        if (!alive) return;

        alive = false;

        �л��ж�״̬(false);
        �л�����״̬(false);
        �л���״̬(false);

        // ? ������������ȫ���
        if (myAnimator != null)
        {
            if (HasParameter(myAnimator, "Walk"))
                myAnimator.SetBool("Walk", false);
            else
                Debug.LogWarning("Animator ȱ�ٲ���: Walk");

            if (HasParameter(myAnimator, "Die"))
                myAnimator.SetBool("Die", true);
            else
                Debug.LogWarning("Animator ȱ�ٲ���: Die");
        }
        else
        {
            Debug.LogError("myAnimator δ����ֵ��");
        }

        // ? Ѫ������
        level1ArmorHealth = 0;
        level2ArmorHealth = 0;
        Ѫ�� = 0;
        loadArmorStatus();

        // ? ʧЧ��ײ��ǰȷ�ϴ���
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
        else
            Debug.LogWarning("δ�ҵ� Collider2D ���");

        // ? ֪ͨ ZombieManagement���������ü�飩
        if (GameManagement.instance.zombieManagement != null)    
        {
            ZombieManagement zm = GameManagement.instance.zombieManagement.GetComponent<ZombieManagement>();
            if (zm != null)
                zm.minusZombieNumAll(gameObject);
            else
                Debug.LogWarning("zombieManagement ������û�� ZombieManagement ���");
        }
        else
        {
            Debug.LogWarning("GameManagement.zombieManagement Ϊ null");
        }

        
    }

    // ?? ���߷�������� Animator �Ƿ����ĳ����
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
        Ѫ�� = 0;
        level1ArmorHealth = 0;
        level2ArmorHealth = 0;
        loadArmorStatus();
        Destroy(gameObject);
        alive = false;
        dying = true;
        //ȫ����ʬ����һ
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

    protected IEnumerator ������Ѫ()
    {
        float timer = 0f;
        while (true)
        {
            if (alive)
            {
                beAttacked(���Ѫ�� / 10, 2, -1);
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

    #region ����ͼ��
    public virtual void setPosRow(int pos)
    {
        //����������
        pos_row = pos;

        //����˳��ͼ�㼰��ʾ˳��
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        GetComponent<SortingGroup>().sortingLayerName = "Zombie-" + pos_row;
        //GetComponent<SortingGroup>().sortingOrder += orderOffset * 20;
        //orderOffset++;
    }

    public virtual void SetSortingOrder(int ����)
    {
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        GetComponent<SortingGroup>().sortingOrder += ���� * 50;
        
    }
    #endregion

    #region ����
    public virtual void TriggerHighlight(int bulletType)
    {
        if(!GameManagement.isPerformance)
        {
            if(���� == null)
            {
                ���� = StartCoroutine(HighlightCoroutine(bulletType));
            }
            else
            {
                StopCoroutine(����);
                ���� = StartCoroutine(HighlightCoroutine(bulletType));
            }
            
        }
        
    }

    public virtual IEnumerator HighlightCoroutine(int bulletType)
    {
        float duration = 0.15f; // �����ͻָ��ĳ���ʱ��
        float startTime = Time.time;

        // ��ȡ��ǰ͸����
        float currentAlpha = 0f;

        // ͸�������ӣ��ӵ�ǰ͸���ȵ����ֵ 1
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(currentAlpha, 0.2f, (Time.time - startTime) / duration); // �ӵ�ǰ͸���ȵ� 1
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // ����͸����
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }
        startTime = Time.time;

        // ͸���ȼ��٣��� 1 �� 0
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(0.2f, 0f, (Time.time - startTime) / duration); // �� 1 �� 0
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // ����͸����
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }

        // ȷ��͸��������Ϊ 0
        foreach (Renderer renderer in allRenderers)
        {
            if (renderer != null && renderer.gameObject.name != "Shadow")
            {
                Material mat = renderer.material;
                Color currentColor = mat.GetColor("_Color");
                currentColor.a = 0f;  // ����͸����Ϊ 0
                mat.SetColor("_Color", currentColor);
            }
        }
    }

    private IEnumerator FadeBlendColor()//����ʱ����ʧ
    {
        yield return new WaitForSeconds(0.5f);

        float elapsed = 0f;

        // �ڿ�ʼǰ�������в��ʵĳ�ʼ��ɫ��������
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
                    initialColors[i][j] = Color.clear; // ռλ
            }
        }

        // �������
        while (elapsed < 1f)
        {
            float t = elapsed / 1f; // �� 0 �� 1
            for (int i = 0; i < allRenderers.Length; i++)
            {
                var mats = allRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    if (mats[j].HasProperty("_BlendColor"))
                    {
                        Color c = initialColors[i][j];
                        // �� alpha �ӳ�ʼ (ͨ��Ϊ1) lerp �� 0
                        c.a = Mathf.Lerp(initialColors[i][j].a, 0f, t);
                        mats[j].SetColor("_BlendColor", c);
                    }
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ȷ��������ȫ͸��
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

    #region ��ײ��
    public virtual void �ر���ײ��()
    {
    }

    public virtual void ������ײ��()
    {
    }
    #endregion

    #region ���Ŵ���
    public void loadDoorStatus()
    {
        ���Ŷϱۿ���();
        if (!level2ArmorIsDrop)
        {
            if (!����������Ѿ��л�)
            {
                if (level2ArmorHealth <= level2ArmorMaxHealth / 3 * 2)
                {
                    ����������Ѿ��л� = true;
                    foreach (GameObject gameObject in ����)
                    {
                        if (gameObject != null)
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = DoorSprite[1];
                        }
                    }
                }
            }
            if (!������ȫ�����Ѿ��л�)
            {
                if (level2ArmorHealth <= level2ArmorMaxHealth / 3 * 1)
                {
                    ������ȫ�����Ѿ��л� = true;
                    foreach (GameObject gameObject in ����)
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
                ��������������ʾ�߼�();
                level2ArmorIsDrop = true;
                myAnimator.SetBool("LostDoor", true);
                if (newZombie)
                {
                    GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombiePendantDrop);
                    go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                    go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(����[0].GetComponent<SpriteRenderer>().sprite);
                    go.transform.position = ����[0].transform.position;
                    go.transform.rotation = Quaternion.identity;
                }
            }
        }
        
        if(Ѫ�� <= 0)
        {
            ��������������ʾ�߼�();
        }
    }
    public void ����������ʾ�߼�()
    {
        SetActiveForObjects(����ȫ��, false);
        SetActiveForObjects(���Ž�ʬ������ʾ��, true);
        ���Ŷϱۿ���();
    }

    public void ���ſ�ҧ��ʾ�߼�()
    {
        SetActiveForObjects(����ȫ��, false);
        SetActiveForObjects(���Ž�ʬ��ҧ��ʾ��, true);
        ���Ŷϱۿ���();
    }

    public void ��������������ʾ�߼�()
    {
        SetActiveForObjects(����ȫ��, false);
        SetActiveForObjects(���Ž�ʬ����������ʱ��ʾ��, true);
        SetActiveForObjects(����, false);
        ���Ŷϱۿ���();
    }
    public void ��������()
    {
        SetActiveForObjects(����, false);
        foreach (GameObject objects in ����)
        {
            if (objects != null)
            {
                Destroy(objects);
            }
        }
    }

    public void ���Ŷϱۿ���()
    {
        if (armIsDrop)
        {
            SetActiveForObjects(���Ŷϱ�ʱǿ�Ʋ���ʾ, false);
            SetActiveForObjects(���Ŷϱ�ʱǿ����ʾ, true);
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

    public void ���ų�ʼ��()
    {
        Transform[] allChildrens = GetComponentsInChildren<Transform>(true);

        if (!newZombie)
        {
            ���Ž�ʬ��ҧ��ʾ�� = GetGameObjectsByNames(allChildrens, new string[]
        {
        "Zombie_outerarm_upper", "Zombie_outerarm_lower", "Zombie_outerarm_hand",
        "Zombie_innerarm_upper", "Zombie_innerarm_lower", "Zombie_innerarm_hand",
        "ScreenDoor_Eating"
        });

            ���Ž�ʬ������ʾ�� = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor", "ScreenDoor_outerarm", "Screen_innerhand", "Screen_innerarm"
            });

            ���Ž�ʬ����������ʱ��ʾ�� = GetGameObjectsByNames(allChildrens, new string[]
            {
        "Zombie_outerarm_upper", "Zombie_outerarm_lower", "Zombie_outerarm_hand",
        "Zombie_innerarm_upper", "Zombie_innerarm_lower", "Zombie_innerarm_hand",
        "ScreenDoor"
            });

            ���� = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor", "ScreenDoor_Eating"
            });

            ���Ŷϱ�ʱǿ�Ʋ���ʾ = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor_outerarm", "ScreenDoor_outerarm", "Screen_innerarm","Screen_innerhand"
            });

            ���Ŷϱ�ʱǿ����ʾ = GetGameObjectsByNames(allChildrens, new string[]
            {
        "Zombie_outerarm_upper"
            });
        }
        else
        {

            ���Ž�ʬ������ʾ�� = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_SCREENDOOR1", "ZOMBIE_OUTERARM_SCREENDOOR", "ZOMBIE_INNERARM_SCREENDOOR_HAND", "ZOMBIE_INNERARM_SCREENDOOR"
            });

            ���Ž�ʬ����������ʱ��ʾ�� = GetGameObjectsByNames(allChildrens, new string[]//Ҳ�����Ž�ʬ��ҧ��ʾ
            {
        "ZOMBIE_OUTERARM_UPPER", "ZOMBIE_OUTERARM_LOWER", "ZOMBIE_OUTERARM_HAND",
        "ZOMBIE_INNERARM_UPPER", "ZOMBIE_INNERARM_LOWER", "ZOMBIE_INNERARM_HAND",
        "ZOMBIE_SCREENDOOR1"
            });

            ���Ž�ʬ��ҧ��ʾ�� = ���Ž�ʬ����������ʱ��ʾ��;

            ���� = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_SCREENDOOR1"
            });

            ���Ŷϱ�ʱǿ�Ʋ���ʾ = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_OUTERARM_SCREENDOOR", "ZOMBIE_INNERARM_SCREENDOOR","ZOMBIE_INNERARM_SCREENDOOR_HAND"
            });

            ���Ŷϱ�ʱǿ����ʾ = GetGameObjectsByNames(allChildrens, new string[]
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
                if (!list.Contains(item)) // ���list���������Ԫ��
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

    #region Ѫ����ʾ

    private string Ѫ���ı�()
    {
        string healthDisplay = "";
        if (Ѫ�� > 0)
        {
            healthDisplay += "���壺" + Ѫ�� + "/" + ���Ѫ�� + "\n";
        }
        if (level1ArmorHealth > 0)
        {
            healthDisplay += "һ�ࣺ" + level1ArmorHealth + "/" + level1ArmorMaxHealth + "\n";
        }
        if (level2ArmorHealth > 0)
        {
            healthDisplay += "���ࣺ" + level2ArmorHealth + "/" + level2ArmorMaxHealth + "\n";
        }
        if (debuff.�ж� > 0)
        {
            healthDisplay += "���أ�" + debuff.�ж�;
        }
        return healthDisplay;
    }

    public void ����Ѫ���ı�()
    {
        Ѫ����ʾ.text = Ѫ���ı�();
    }

    public void ���Ѫ����ʾ(bool b)
    {
        Ѫ����ʾ.gameObject.SetActive(b);
    }


    #endregion
}

public struct ��ʬdebuff
{
    public float ����;//����ʱ��
    public int �ж�;//�ж�����
    public bool �Ȼ�;
    public bool ��;
    public bool ����;
    
}

public struct ��ʬbuff {
    public bool ����;
    public int ����;
    public bool ������ɱ;
    public bool ���߱���;
    public bool �����Ȼ�;
}