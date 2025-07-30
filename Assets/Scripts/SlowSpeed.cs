using Manager;
using UnityEngine;

public class SlowSpeed : MonoBehaviour
{
    bool isInSlow;
    float timer;
    public bool isSlow;
    public float speedSlow = 1.65f;
    public float speedNormal = 3f;
    public bool isBongBong;
    public bool isHide = false;
    private void Update()
    {
        if (isInSlow)
        {
            timer += Time.deltaTime;
            if (isBongBong)
            {
                transform.position = GameController.Instance.Player.transform.position;
            }
            if (timer > 5) 
            {
                GameController.Instance.CurrentLevel.Speed = 2.2f;
                isInSlow = false;
            }
        }
    }
    public void SetSlowSpeed()
    {
        if (isInSlow) return;
        if (isSlow)
        {
            GameController.Instance.CurrentLevel.Speed = speedSlow;
        }
        else
        {
            GameController.Instance.CurrentLevel.Speed = speedNormal;
        }
        isInSlow = true;
        if (isHide)
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
}
