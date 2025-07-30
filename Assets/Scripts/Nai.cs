using UnityEngine;

public class Nai : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private string animName = "idle";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);

        }
    }
    public void ResetAnim()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = new Vector3(5.39f, transform.position.y, transform.position.z);
        ChangeAnim("idle");
    }
}
