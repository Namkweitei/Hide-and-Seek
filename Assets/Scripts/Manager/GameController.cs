
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class GameController : Singleton<GameController>
    {
        [SerializeField] bool isPlay;
        [SerializeField] bool isStop;
        [SerializeField] bool isWin;
        [SerializeField] bool isLoose;
        [SerializeField] Button buttonStart;
        [SerializeField] Button buttonReload;
        [SerializeField] Player player;
        [SerializeField] Cat cat;
        [SerializeField] List<Level> levelList;
        public bool IsPlay { get => isPlay; set => isPlay = value; }
        public Player Player { get => player; set => player = value; }
        public Cat Cat { get => cat; set => cat = value; }
        public bool IsWin { get => isWin; set => isWin = value; }
        public bool IsLoose { get => isLoose; set => isLoose = value; }

        private void Start()
        {
            Application.targetFrameRate = 60;
            //buttonStart.onClick.AddListener(StartGame);
            //buttonReload.onClick.AddListener(() =>
            //{
            //   SceneManager.LoadScene(0);
            //});
            isWin = false;
            isLoose = false;
            int rd = Random.Range(0, levelList.Count);
            Level level = Instantiate(levelList[rd]);
            level.transform.position = new Vector3(0, -2.8f, 0);
        }
        private void Update()
        {
            
        }
        public void StartGame()
        {
            player.ChangeAnim("run");
            //buttonStart.gameObject.SetActive(false);
            isPlay = true;
            player.IsStart = true;
            cat.isStart = true;
        }

        public void LooseGame()
        {
            isPlay = false;
            if (isLoose) return;
            cat.Reset();
            cat.ChangeAnim("surprise");
            player.ChangeAnim("catch");
            Debug.Log("Game Over! You lost.");
            isLoose = true;
            CanvasController.Instance.LooseGame();
        }

        public void WinGame()
        {
            Debug.Log("Congratulations! You won the game.");
            isPlay = false;
            buttonReload.gameObject.SetActive(true);
            player.ChangeAnim("win");
            cat.Reset();
            cat.ChangeAnim("idle");
            player.IsStart = false;
            cat.isStart = false;
            CanvasController.Instance.WinGame();
        }
    }
}
