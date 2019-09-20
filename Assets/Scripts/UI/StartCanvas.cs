using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartCanvas : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup bottomBtnSeting;
    [SerializeField]
    [Tooltip( "弹出框" )]
    private Image popWindow;
    [SerializeField]
    [Tooltip( "历史最高分排行" )]
    private Text highScoreTxt;
    void Start()
    {
        if ( popWindow.enabled )
        {
            popWindow.gameObject.SetActive( false );
        }
        FileReadWirte.Instance.CheckScore();
    }
    void Update()
    {
        
    }
    public void OnStartClick()
    {
        if ( Time.timeScale ==0 )
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene( 1 );
    }
    public void OnRankClick()
    {
        popWindow.gameObject.SetActive( true );
        highScoreTxt.text = FileReadWirte.Instance.ReadFileOfScore().ToString();
    }
    public void OnEndlessClick()
    {

    }
    public void OnSettingClick()
    {

    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
    public void OnCancelClick()
    {
        popWindow.gameObject.SetActive( false );
    }
}
