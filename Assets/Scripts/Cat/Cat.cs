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
    [SerializeField] public Animator anim;
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
    private void Start()
    {
        fieldOfViewLeft.SetOnHitObject(OnFieldOfViewHit);
        fieldOfViewRight.SetOnHitObject(OnFieldOfViewHit);
    }

    private void OnFieldOfViewHit(Collider2D colliderHit)
    {
        if (onCatch) return;
        if (colliderHit.gameObject.layer == playerLayer)
        {
            onCatch = true;
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
        if (!isStart) return;
        timeCatch -= Time.deltaTime;
        if (timeCatch <= 0)
        {
            Debug.Log("Catch");
            timeCatch = UnityEngine.Random.Range(6f, 8f);
            transform.DOMoveY(1.5f, 1f).SetEase(DG.Tweening.Ease.InBack).OnComplete(() =>
            {
                Debug.Log("Play Catch Anim");
                catMask.sortingOrder = 1;
                PlayLight();
                StartCoroutine(EndCatch());
            });
        }
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
    }

    private void PlayAnim(AnimType animType, Action actionCallBack = null)
    {
        anim.Play(animType.ToString());
        //if (AnimTimeGlobalConfig.Instance == null || exam == null)
        //{
        //    Debug.LogWarning("Missing anim config or exam data");
        //    return;
        //}

        //float animTime;
        //try
        //{
        //    animTime = AnimTimeGlobalConfig.Instance.GetAnimTime(animType, exam);
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError("Failed to get anim time: " + e.Message);
        //    return;
        //}
        //var animTime = AnimTimeGlobalConfig.Instance.GetAnimTime(animType, exam);
        LMotion.Create(0, 1, 1).WithOnComplete(() =>
        {
           
                actionCallBack?.Invoke();
            
            
        }).RunWithoutBinding().AddTo(this);
        Debug.Log("PlayLight");
    }
}

public enum AnimType
{
    Idle,
    Surprise,
    Catch,
    Light
}
