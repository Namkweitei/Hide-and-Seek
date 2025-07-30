using DG.Tweening;
using Manager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : Singleton<CanvasController>
{
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonReload;
    [SerializeField] Button buttonNext;
    [SerializeField] Slider sliderProgress;
    [SerializeField] Slider sliderLoading;
    [SerializeField] CanvasGroup panelLoading;
    [SerializeField] GameObject panelGame;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLoose;
    [SerializeField] GameObject panelNext;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] int sceneLoad;

    [SerializeField] bool isAilient;
    public int Coin 
    { 
        get => PlayerPrefs.GetInt("Coin",0); 
        set => PlayerPrefs.SetInt("Coin", value) ; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sliderLoading != null) 
        {
            sliderLoading.DOValue(1, 2f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                panelLoading.DOFade(0, 1f).SetDelay(1.5f).OnComplete(() =>
                {
                    panelLoading.gameObject.SetActive(false);
                });
            });
        }
        else
        {
            panelLoading.DOFade(0, 1f).SetDelay(1.5f).OnComplete(() =>
            {
                panelLoading.gameObject.SetActive(false);
            });
        }
       
       
        buttonStart.onClick.AddListener(() =>
        {
            GameController.Instance.StartGame();
            panelGame.SetActive(true);
            buttonStart.gameObject.SetActive(false);
        });
        if(buttonReload != null)
        {
            buttonReload.onClick.AddListener(() =>
            {
                GameController.Instance.InitStart();
                buttonStart.gameObject.SetActive(true);
                panelNext.SetActive(false);
            });
        }

        buttonNext.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneLoad == 0 ? 1 : 0);
        });
        textScore.text = Coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpCoin()
    {
        int score = Coin;
        score++;
        textScore.text = score.ToString();
        Coin = score;
    }
    public void SetSlider(float value)
    {
        sliderProgress.value = value;
    }
    public void WinGame()
    {
        if (isAilient)
        {
            GameController.Instance.CurrentLevel.GetComponent<Animator>().enabled = true;
            GameController.Instance.CurrentLevel.GetComponent<Animator>().Play("Tenlua");
        }
        else
        {
            StartCoroutine(WinCatch());
        }
    }
    public void LooseGame()
    {
        StartCoroutine(EndCatch());
    }
    IEnumerator EndCatch()
    {
        yield return new WaitForSeconds(1f);
        panelGame.SetActive(false);
        panelLoose.SetActive(true);
        yield return new WaitForSeconds(1f);
        panelLoose.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() =>
        {

            GameController.Instance.InitStart();
            buttonStart.gameObject.SetActive(true);
            panelLoose.SetActive(false);


        });
        
    }
    IEnumerator WinCatch()
    {
        yield return new WaitForSeconds(5f);
        panelGame.SetActive(false);
        panelWin.SetActive(true);
        yield return new WaitForSeconds(2f);
        if(panelNext != null)
        {
            panelNext.SetActive(true);
        }
        else
        {
            panelWin.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() =>
            {
                if(sceneLoad == 0)
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
                
            });
        }
        
    }
}
