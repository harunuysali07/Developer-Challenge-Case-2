using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public Vector3 targetStackPosition;

    [SerializeField] private float runSpeed = 1f;

    private Animator animator;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GameController.Instance.OnLevelStart += OnLevelStart;
        GameController.Instance.OnLevelEnd += OnLevelEnd;
    }

    private void OnLevelStart()
    {
        targetStackPosition = transform.position;
        animator.Rebind();
    }

    private void OnLevelEnd(bool isWin)
    {
        animator.SetTrigger(isWin ? "Success" : "Fail");
    }

    private void Update()
    {
        UpdateBlendSpeed();

        if (!GameController.GameState)
            return;

        if (Vector3.Distance(transform.position, targetStackPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetStackPosition, runSpeed * Time.deltaTime);
        }

        if (!Physics.SphereCast(transform.position + Vector3.up, .1f, Vector3.down, out RaycastHit hit, 1.1f, LayerMask.GetMask("Stack")))
        {
            Debug.Log("Player failed");

            GetComponent<Rigidbody>().isKinematic = false;
            animator.SetTrigger("Fall");

            GameController.Instance.LevelFailed();
        }
    }

    private Vector3 lastPosition;
    private float blendTreeSpeed = 0;
    private void UpdateBlendSpeed()
    {
        blendTreeSpeed = Mathf.Lerp(blendTreeSpeed, Mathf.Clamp01((lastPosition - transform.position).magnitude * 100f), 10 * Time.deltaTime);
        animator.SetFloat("Movement Speed", blendTreeSpeed);
        lastPosition = transform.position;
    }
}
