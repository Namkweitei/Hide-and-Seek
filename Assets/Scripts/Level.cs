using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    [SerializeField] Animator luaAnim;
    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(GameController.Instance.IsPlay == false) return;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        //if (transform.position.x <= -10)
        //{
        //    gameObject.SetActive(false);
        //}
    }
    private void FixedUpdate()
    {
        float distanceStartToEnd = Vector3.Distance(startPoint.position, endPoint.position);
        float distance = Vector3.Distance(GameController.Instance.Player.transform.position, endPoint.position);
        CanvasController.Instance.SetSlider(1 - distance / distanceStartToEnd);
        
    }
    public void SetTenLua()
    {
        GameController.Instance.Player.gameObject.SetActive(false);
        luaAnim.gameObject.SetActive(true);
        luaAnim.Play("Lua");
    }
    public void SetLoad()
    {
        SceneManager.LoadScene(1);
    }
}
