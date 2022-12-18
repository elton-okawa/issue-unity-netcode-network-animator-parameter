using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AnimatorController : NetworkBehaviour
{
    [SerializeField] string logName;
    [SerializeField] bool logEnabled = false;

    Animator animator;
    readonly int IS_RED = Animator.StringToHash("isRed");

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FastChangeTimes(int times)
    {
        StartCoroutine(ToggleCoroutine(animator.GetBool(IS_RED), times));
    }

    IEnumerator ToggleCoroutine(bool initialValue, int times)
    {
        bool lastValue = initialValue;
        for (int i = 0; i < times; i++)
        {
            yield return new WaitForEndOfFrame();
            lastValue = !lastValue;
            SetIsRed(lastValue);
        }
    }

    public void Toggle()
    {
        SetIsRed(!animator.GetBool(IS_RED));
    }

    void SetIsRed(bool isRed)
    {
        if (logEnabled) Debug.Log($"[{logName}] isRed: {isRed}");
        animator.SetBool(IS_RED, isRed);
    }
}
