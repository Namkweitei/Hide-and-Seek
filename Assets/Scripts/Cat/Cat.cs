using System;
using System.Collections;
using DG.Tweening;
using LitMotion;
using Manager;
using Obstacle;
using ScriptAbleObject;
using Sirenix.OdinInspector;
using UnityEngine;


public class Cat : MonoBehaviour
{
    private MotionHandle lightHandle;
    public Exam exam;
    [SerializeField] Animator anim;
    [SerializeField] private string animName = "idle";
    [SerializeField] private FieldOfView fieldOfViewLeft;
    [SerializeField] private FieldOfView fieldOfViewRight;
    [SerializeField] private float viewDistance;
    [SerializeField] private float duration;
    [SerializeField] private bool onCatch;
    [SerializeField] SpriteRenderer cat;
    [SerializeField] private SpriteRenderer catMask;
    [SerializeField] float timeCatch;
    public int playerLayer;
    public bool isStart;
    public bool isStop;
    private void Start()
    {
        fieldOfViewLeft.SetOnHitObject(OnFieldOfViewHit);
        fieldOfViewRight.SetOnHitObject(OnFieldOfViewHit);
        Reset();
    }

    private void OnFieldOfViewHit(Collider2D colliderHit)
    {
        if (onCatch) return;
        if (colliderHit.gameObject.layer == playerLayer)
        {
            onCatch = true;
            Reset();
            GameController.Instance.LooseGame();
        }
        else
        {
            var obstacle = colliderHit.GetComponent<IObstacle>();
            obstacle?.Activate();
        }
    }
    private void Update()
    {
        //if (!isStart) return;
        //timeCatch -= Time.deltaTime;
        //if (timeCatch <= 0 && !isStop)
        //{
        //    isStop = true;
        //    ChangeAnim(AnimType.Catch.ToString().ToLowerInvariant());
            
        //    //transform.DOMoveY(1.5f, 1f).SetEase(DG.Tweening.Ease.InBack).OnComplete(() =>
        //    //{
        //    //    Debug.Log("Play Catch Anim");
        //    //    catMask.sortingOrder = 1;
        //    //    PlayLight();
        //    //    StartCoroutine(EndCatch());
        //    //});
        //}
    }
    IEnumerator EndCatch()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Reset");
        Reset();
        catMask.sortingOrder = -1;
        transform.DOMoveY(-1.5f, 1f).SetEase(DG.Tweening.Ease.InBack);
    }
    [Button]
    private void PlayLight()
    {
        PlayAnim(AnimType.Idle, ShowFieldOfView);
    }

    private void ShowFieldOfView()
    {
        if (lightHandle.IsPlaying())
            lightHandle.TryCancel();
        fieldOfViewLeft.gameObject.SetActive(true);
        fieldOfViewRight.gameObject.SetActive(true);
        lightHandle = LMotion.Create(0f, viewDistance, duration).Bind(x =>
        {
            fieldOfViewLeft.viewDistance = x;
            fieldOfViewRight.viewDistance = x;
        }).AddTo(this);
    }

    [Button]
    public void Reset()
    {
        onCatch = false;
        fieldOfViewLeft.viewDistance = 0;
        fieldOfViewRight.viewDistance = 0;
        fieldOfViewLeft.gameObject.SetActive(false);
        fieldOfViewRight.gameObject.SetActive(false);
    }
    public void ReSetAnim()
    {
        isStop = false;
        ChangeAnim("idle");
        timeCatch = UnityEngine.Random.Range(4f, 6f);

    }
    private void PlayAnim(AnimType animType, Action actionCallBack = null)
    {
        //ChangeAnim(animType.ToString().ToLowerInvariant());
        LMotion.Create(0, 1, 1).WithOnComplete(() =>
        {
           
                actionCallBack?.Invoke();
            
            
        }).RunWithoutBinding().AddTo(this);
        Debug.Log("PlayLight");
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
}

public enum AnimType
{
    Idle,
    Surprise,
    Catch,
    Light
}
