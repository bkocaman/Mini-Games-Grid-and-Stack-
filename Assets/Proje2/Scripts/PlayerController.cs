using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject[] collectiblesPrefab;
    [SerializeField] private GameObject model;

    private bool controlsEnabled = false;
    private bool isStarted = false;

    private Animator animator;


    private void Start()
    {
       
        animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        if (controlsEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //platform manager add
            }
       
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isStarted)
            {
                Start2Run();
            }
        }
    }

    public void StartNextLevel()
    { 

        Start2Run();
    }

    private void Start2Run()
    {
        isStarted = true;
        controlsEnabled = true;
       
    }


    public void StartToRun(float timeFor2unit)
    {
        animator.applyRootMotion = false;
        animator.SetTrigger("Run");
        model.transform.DOLocalRotate(Vector3.zero, 0.3f);
        transform.DOMoveZ(2, timeFor2unit).SetRelative(true).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartToRun(timeFor2unit);
        });
    }

    public void Fall()
    {
        transform.DOKill();
      
    }
    
}
