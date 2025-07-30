
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
        [SerializeField] Level currentLevel;
        [SerializeField] Nai nai;
        public bool IsPlay { get => isPlay; set => isPlay = value; }
        public Player Player { get => player; set => player = value; }
        public Cat Cat { get => cat; set => cat = value; }
        public bool IsWin { get => isWin; set => isWin = value; }
        public bool IsLoose { get => isLoose; set => isLoose = value; }
        public Level CurrentLevel { get => currentLevel; set => currentLevel = value; }
        public List<Level> LevelList { get => levelList; set => levelList = value; }

        private void Start()
        {
            Application.targetFrameRate = 60;
            //buttonStart.onClick.AddListener(StartGame);
            //buttonReload.onClick.AddListener(() =>
            //{
            //   SceneManager.LoadScene(0);
            //});
            InitStart();
        }
        public void InitStart()
        {
            isWin = false;
            isLoose = false;
            int rd = Random.Range(0, levelList.Count);
            if(currentLevel != null)
            {
                Destroy(currentLevel.gameObject);
            }
            currentLevel = Instantiate(levelList[rd]);
            currentLevel.transform.position = new Vector3(0, -2.8f, 0);
            cat.ChangeAnim("start1");
            player.ChangeAnim("idle");
            if(nai != null)
            {
                nai.ChangeAnim("idle");
            }
        }
        private void Update()
        {
            
        }
        public void StartGame()
        {
            player.ChangeAnim("run");
            cat.ChangeAnim("start");
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
            player.ChangeAnim("win");
            cat.Reset();
            cat.ChangeAnim("idle");
            player.IsStart = false;
            cat.isStart = false;
            CanvasController.Instance.WinGame();
        }
    }
}
