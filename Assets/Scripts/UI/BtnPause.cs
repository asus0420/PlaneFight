using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnPause : MonoBehaviour
{
    [SerializeField]
    private Sprite replay;
    private Sprite currentSprite;
    [Tooltip("暂停按钮")]
    private Button btnPause;
    [SerializeField]
    [Tooltip( "重开游戏" )]
    private Button restartGame;
    [SerializeField]
    [Tooltip( "返回主菜单" )]
    private Button backMenu;
    [SerializeField]
    [Tooltip( "暂停弹出框" )]
    private Image popWindow;
    [SerializeField]
    [Tooltip( "透明背景遮挡" )]
    private Image shelter;
    [SerializeField]
    [Tooltip( "背景音乐" )]
    private AudioSource bgm;
    [SerializeField]
    [Tooltip( "战斗失败背景图" )]
    private GameObject defeat;

    private Plane plane;

    void Start()
    {
        plane = Plane.Instance;
        currentSprite = GetComponent<Image>().sprite;
        btnPause = GetComponent<Button>();
        if ( popWindow.enabled == true )
        {
            popWindow.gameObject.SetActive( false );
            shelter.gameObject.SetActive( false );
        }
        defeat.SetActive( false );
        btnPause.onClick.AddListener( OnBtnPauseClick );
        restartGame.onClick.AddListener( OnBtnReStartClick );
        backMenu.onClick.AddListener( OnBtnBackMenuClick );
    }

    private void Update()
    {
        PlaneLifeState();
    }
    /// <summary>
    /// 飞机生命状态
    /// </summary>
    void PlaneLifeState()
    {
        if ( plane.IsDie )
        {
            GamePause();
            defeat.SetActive(true);
        }
    }
    /// <summary>
    /// 游戏暂停逻辑
    /// </summary>
    void GamePause()
    {
        btnPause.image.sprite = replay;
        popWindow.gameObject.SetActive( true );
        shelter.gameObject.SetActive( true );
        bgm.mute = true;
        Time.timeScale = 0;
    }
    /// <summary>
    /// 游戏开始逻辑
    /// </summary>
    void GameStart()
    {
        btnPause.image.sprite = currentSprite;
        Time.timeScale = 1;
        popWindow.gameObject.SetActive( false );
        shelter.gameObject.SetActive( false );
        bgm.mute = false;
    }
    /// <summary>
    /// 暂停按钮被按下
    /// </summary>
    void OnBtnPauseClick()
    {
        if ( btnPause.image.sprite == currentSprite )
        {
            GamePause();
        }
        else
        {
            GameStart();
        }
    }
    /// <summary>
    /// 重开游戏被按下
    /// </summary>
    void OnBtnReStartClick()
    {
        if ( Time.timeScale == 0 ) 
        {
            Time.timeScale = 1;
        }
        if ( plane.IsDie )
        {
            //将飞机生命状态置为真
            plane.IsDie = false;
        }
        SceneManager.LoadScene( 1 );
    }
    /// <summary>
    /// 回到主菜单
    /// </summary>
    void OnBtnBackMenuClick()
    {
        SceneManager.LoadScene( 0 );
    }
}
