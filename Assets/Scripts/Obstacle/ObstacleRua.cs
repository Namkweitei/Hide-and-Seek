using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleRua : MonoBehaviour, IObstacle
    {
        public float speed = 1f;
        [SerializeField] private bool isActive = false;
        
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
            transform.localPosition = vectorTrs + Vector2.left * (speed * Time.deltaTime);
        }
        
        public void RestObstacle()
        {
            isActive = false;
            transform.localPosition = Vector2.zero;
        }
    }
}
