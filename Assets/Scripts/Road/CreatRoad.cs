using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRoad : MonoBehaviour
{
    [SerializeField] Transform posStart;
    [SerializeField] Road roadPrefab;
    [SerializeField] float timer;
    [SerializeField] float timeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeSpeed)
        {
            Road newRoad = Instantiate(roadPrefab, posStart.position, posStart.rotation);
            timer = 0;
        }
    }
}
