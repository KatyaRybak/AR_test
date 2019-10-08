using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfLogic : MonoBehaviour
{
    Animator wolfAnimator;
    Coroutine isWolfRun;
    private void Start()
    {
        wolfAnimator = GetComponentInParent<Animator>();
    }
    private void OnMouseDown()
    {
        GameManager.instance.WolfMouseClic(wolfAnimator,true);
        if (isWolfRun != null)
            return;
        isWolfRun = StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(15.0f);
        GameManager.instance.WolfMouseClic(wolfAnimator, false);
        isWolfRun = null;

    }
}
