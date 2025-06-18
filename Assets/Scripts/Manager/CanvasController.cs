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
    [SerializeField] CanvasGroup panelLoading;
    [SerializeField] GameObject panelGame;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLoose;
    [SerializeField] TextMeshProUGUI textScore;

    public int Coin 
    { 
        get => PlayerPrefs.GetInt("Coin",0); 
        set => PlayerPrefs.SetInt("Coin", value) ; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panelLoading.DOFade(0, 1f).SetDelay(1.5f).OnComplete(() =>
        {
            panelLoading.gameObject.SetActive(false);
        });
        buttonStart.onClick.AddListener(() =>
        {
            GameController.Instance.StartGame();
            buttonStart.gameObject.SetActive(false);
            panelGame.SetActive(true);
        });
        //buttonReload.onClick.AddListener(() =>
        //{
        //    SceneManager.LoadScene(0);
        //});
        //buttonNext.onClick.AddListener(() =>
        //{
        //    SceneManager.LoadScene(0);
        //});
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
       StartCoroutine(WinCatch());
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
            SceneManager.LoadScene(0);
        });
        
    }
    IEnumerator WinCatch()
    {
        yield return new WaitForSeconds(1f);
        panelGame.SetActive(false);
        panelWin.SetActive(true);
        yield return new WaitForSeconds(2f);
        panelWin.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
