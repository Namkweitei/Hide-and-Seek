using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] float speed;
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

}
