using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private string animName = "idle";
    private bool isHoldingMouse = false;
    bool isStart;

    public bool IsStart { get => isStart; set => isStart = value; }

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            GameController.Instance.WinGame();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            GameController.Instance.WinGame();
            ChangeAnim("idle");
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
}
