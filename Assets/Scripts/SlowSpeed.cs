using Manager;
using UnityEngine;

public class SlowSpeed : MonoBehaviour
{
    bool isInSlow;
    float timer;
    public bool isSlow;
    public float speedSlow = 1.65f;
    private void Update()
    {
        if (isInSlow)
        {
            timer += Time.deltaTime;
            if (timer > 5) 
            {
                GameController.Instance.CurrentLevel.Speed = 2.2f;
                isInSlow = false;
            }
        }
    }
    public void SetSlowSpeed()
    {
        if (isSlow)
        {
            GameController.Instance.CurrentLevel.Speed = speedSlow;
        }
        else
        {
            GameController.Instance.CurrentLevel.Speed = 3f;
        }
        isInSlow = true;
    }
}
