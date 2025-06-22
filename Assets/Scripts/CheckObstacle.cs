using Manager;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    bool isCatch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Home")&&!collision.gameObject.CompareTag("Coin") && !collision.gameObject.CompareTag("Trap") && !isCatch && collision.gameObject.layer != 6)
        {
            isCatch = true;
            GameController.Instance.Cat.ChangeAnim("catch");
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
