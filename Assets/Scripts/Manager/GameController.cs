using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class GameController : Singleton<GameController>
    {
        [SerializeField] bool isPlay;
        [SerializeField] bool isStop;
        [SerializeField] Button buttonStart;
        [SerializeField] Button buttonReload;
        [SerializeField] Player player;
        [SerializeField] Cat cat;
        public bool IsPlay { get => isPlay; set => isPlay = value; }
        public Player Player { get => player; set => player = value; }
        public Cat Cat { get => cat; set => cat = value; }

        private void Start()
        {
            Application.targetFrameRate = 60;
            buttonStart.onClick.AddListener(StartGame);
            buttonReload.onClick.AddListener(() =>
            {
               SceneManager.LoadScene(0);
            });

        }
        private void Update()
        {
            
        }
        private void StartGame()
        {
            player.ChangeAnim("run");
            buttonStart.gameObject.SetActive(false);
            isPlay = true;
            player.IsStart = true;
            cat.isStart = true;
        }

        public void LooseGame()
        {
            Debug.Log("Game Over! You lost.");
        }

        public void WinGame()
        {
            Debug.Log("Congratulations! You won the game.");
            isPlay = false;
            buttonReload.gameObject.SetActive(true);
            player.ChangeAnim("idle");
            player.IsStart = false;
            cat.isStart = false;
        }
    }
}
