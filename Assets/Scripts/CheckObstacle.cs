using Manager;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    bool isCatch;
    int countCatch;
    [SerializeField] Nai nai;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countCatch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Home")&&!collision.gameObject.CompareTag("Coin") && !collision.gameObject.CompareTag("Trap") && !isCatch && collision.gameObject.layer != 6 && collision.gameObject.layer != 8)
        {
            isCatch = true;
            GameController.Instance.Cat.ChangeAnim("catch");
            countCatch++;
            if (countCatch == 2 || countCatch == 4 || countCatch == 6 || countCatch ==8)
            {
                if(nai != null)
                {
                    nai.ChangeAnim("run");
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Home") && !collision.gameObject.CompareTag("Coin") && !collision.gameObject.CompareTag("Trap") && isCatch)
        {
            isCatch = false;
        }
        
    }
}
