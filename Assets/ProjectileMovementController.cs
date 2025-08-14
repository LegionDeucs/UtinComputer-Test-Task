using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementController : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AnimationCurve moveCurve;

    public event Action OnArrive;

    private Tween moveTween;

    public void StartFlying(float projectileRadius, Vector3 visualPosition)
    {
        if (Physics.SphereCast(visualPosition, projectileRadius, Vector3.forward, out RaycastHit raycastHitInfo, 1000, targetLayer))
        {
            Vector3 targetPosition = raycastHitInfo.point.WithY(0) + Vector3.back * projectileRadius;
            float moveDuration = (targetPosition - transform.position).magnitude/moveSpeed;
            moveTween = transform.DOMove(targetPosition, moveDuration).SetEase(moveCurve).OnComplete(EndFly);
        }
    }

    private void EndFly()
    {
        OnArrive?.Invoke();
        moveTween = null;
    }
}
