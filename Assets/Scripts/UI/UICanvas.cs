using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UICanvas : MonoBehaviour
{
    public bool isCanShoot = false;
    private static UICanvas instance;
    public Image hpImg;
    public Image mpImg;
    public Button btn_shoot;
    public Text skillCd;
    public Text scoreTxt;
    private RectTransform hpRect;
    private RectTransform mpRect;
    private Image formButtonImg;
    //总分数
     [HideInInspector]
    public float scoreTotal = 0;
    //当前分数
   
    private float currentScore = 0;
    private float overTime;
    public static UICanvas Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        hpRect = hpImg.GetComponent<RectTransform>();
        mpRect = mpImg.GetComponent<RectTransform>();
        formButtonImg = btn_shoot.GetComponent<Image>();
    }
    void Update()
    {
        UpdatePlaneHp();
        UpdatePlaneMp();
        UpdateSkillTime();
        CheckScore( scoreTotal );
        ShootBtnIsCanUse();
        UpdateScore();
    }
    /// <summary>
    /// 点击按钮
    /// </summary>
    public void IsCanShoot()
    {
        isCanShoot = true;
        Plane.Instance.Mp = 0;
        Plane.Instance.TimeColck = 0;
    }
    /// <summary>
    /// 更新血量
    /// </summary>
    private void UpdatePlaneHp()
    {
        float hp =  Plane.Instance.Hp;
        hpRect.localScale = new Vector3( hp / 10.0f , 1 , 1 );
    }
    /// <summary>
    /// 更新MP
    /// </summary>
    private void UpdatePlaneMp()
    {
        float mp = Plane.Instance.Mp;
        mpRect.localScale = new Vector3( mp / Plane.Instance.maxMp , 1 , 1 );
        formButtonImg.fillAmount = mp / Plane.Instance.maxMp;
    }
    /// <summary>
    /// 更新技能Cd时间显示
    /// </summary>
    private void UpdateSkillTime()
    {
        overTime = Plane.Instance.TimeColck;
        if ( overTime <= Plane.Instance.SkillTime )
        {
            skillCd.text = Math.Round( Plane.Instance.SkillTime - overTime , 1 ).ToString();
        }
    }
    /// <summary>
    /// 攻击按钮能否使用
    /// </summary>
    private void ShootBtnIsCanUse()
    {
        if ( Plane.Instance.Mp >= Plane.Instance.maxMp )
        {
            btn_shoot.interactable = true;
            skillCd.enabled = false;
        }
        else
        {
            btn_shoot.interactable = false;
            skillCd.enabled = true;
        }
    }
    private IEnumerator UpdateScore( int totalScore )
    {
        for ( float i = currentScore ; i <= totalScore ; i++ )
        {
            scoreTxt.text = i.ToString();
            yield return new WaitForSeconds( 0.05f );
            currentScore = int.Parse( scoreTxt.text );
            if ( currentScore > totalScore )
            {
                StopCoroutine( "UpdateScore" );
            }
        }
    }
    /// <summary>
    /// 分数增加的协程
    /// </summary>
    /// <param name="number"></param>
    private void CheckScore( float number )
    {
        if ( number > currentScore )
        {
            StartCoroutine( "UpdateScore" , number );
        }
    }
    /// <summary>
    /// 向文件夹中更新数据
    /// </summary>
    void UpdateScore()
    {
        float _currentScore = FileReadWirte.Instance.ReadFileOfScore();
        if ( _currentScore < scoreTotal )
        {
            FileReadWirte.Instance.WriteFileOfScore( scoreTotal );
        }
    }
}
