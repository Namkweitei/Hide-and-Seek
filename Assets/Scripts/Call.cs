using UnityEngine;

public class Call : MonoBehaviour
{
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
        if(collision.gameObject.layer == 8)
        {
            Nai nai = Object.FindFirstObjectByType<Nai>();
            nai.ChangeAnim("run");
        }
        
    }
}
