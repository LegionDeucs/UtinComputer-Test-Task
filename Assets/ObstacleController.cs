using NoctisDev.Pooling.Runtime.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObstacleController : MonoBehaviour
{
    [Inject] private ColliderProvider colliderProvider;
    [Inject] private IPoolService poolService;

    [SerializeField] private Material hitMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Collider interactionCollider;
    [SerializeField] private Transform destroyFXPrefab;
    [SerializeField] private float despawnFxDelay = 3;

    private void Start()
    {
        colliderProvider.RegisterObstacle(interactionCollider, this);
    }

    internal void Infect()
    {
        meshRenderer.material = hitMaterial;
        interactionCollider.enabled = false;

        this.WaitAndDoCoroutine(1, OnExplode);
    }

    private void OnExplode()
    {
        var destroyFX = poolService.Spawn(destroyFXPrefab);
        destroyFX.transform.position = transform.position;
        poolService.Despawn(destroyFX, despawnFxDelay);
        transform.SetInactive();
    }
}
