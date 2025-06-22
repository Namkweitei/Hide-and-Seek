using System;
using Obstacle;
using Sirenix.OdinInspector;
using UnityEngine;


public class ObstacleRua : MonoBehaviour, IObstacle
{
    public float speed = 1f;
    [SerializeField] private bool isActive = false;

    public bool IsActive { get => isActive; set => isActive = value; }

    [Button]
    public void Activate()
    {
        isActive = true;
    }

    public void InitData()
    {

    }

    private void Update()
    {
        if (!isActive) return;
        Vector2 vectorTrs = transform.localPosition;
        transform.localPosition = vectorTrs + Vector2.right * (speed * Time.deltaTime);
    }

    public void RestObstacle()
    {
        isActive = false;
        transform.localPosition = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            isActive = true;
        }
        if (collision.gameObject.layer == 0)
        {
            isActive = false;
        }
    }
}

