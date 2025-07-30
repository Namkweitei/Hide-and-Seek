using DG.Tweening;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private string animName = "idle";
    public Camera mainCamera;
    private bool isHoldingMouse = false;
    bool isStart;
    bool isZoom = false;
    public bool IsStart { get => isStart; set => isStart = value; }

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart || GameController.Instance.IsLoose) return;
        if (Input.GetMouseButtonDown(0) && !isHoldingMouse)
        {
            isHoldingMouse = true;
            GameController.Instance.IsPlay = false;
            ChangeAnim("idle");
        }
        else if (Input.GetMouseButtonUp(0) && isHoldingMouse)
        {
            isHoldingMouse = false;
            GameController.Instance.IsPlay = true;
            ChangeAnim("run");
        }
        //if (isZoom)
        //{
        //    mainCamera.orthographicSize -= Time.deltaTime;
        //    if (mainCamera.orthographicSize <= 2.4f)
        //    {
        //        mainCamera.orthographicSize = 2.4f;
        //        isZoom = false;
        //    }
        //}
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Home"))
    //    {
    //        GameController.Instance.WinGame();
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            GameController.Instance.WinGame();
            
            //ChangeAnim("idle");
        }
        if(collision.gameObject.CompareTag("Coin"))
        {
            CanvasController.Instance.UpCoin();
            Destroy(collision.gameObject);
            //ChangeAnim("catch");
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            collision.gameObject.GetComponent<SlowSpeed>().SetSlowSpeed();
        }
    }
    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);

        }
    }
    public void SetZoom()
    {
        isZoom = true;
        mainCamera.DOOrthoSize(2.4f, 1f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            isZoom = false;
        });
    }
}
