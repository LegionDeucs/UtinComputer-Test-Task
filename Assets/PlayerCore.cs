using NoctisDev.Pooling.Runtime.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCore : MonoBehaviour
{
    [Inject] private InputProvider inputProvider;
    [Inject] private IPoolService poolService;

    [SerializeField] private ProjectileController projectilePrefab;

    [SerializeField] private float fuelAmount = 1;
    [SerializeField] private float scalePerFuelAmount = 4;
    [SerializeField] private float loadingSpeedPercent = .3f;
    [SerializeField] private Transform animateObject;

    [SerializeField] private Transform projectileSpawnPoint;

    private ProjectileController spawnedProjectile;
    private float currentFuel;
    private Coroutine startLoadingRoutine;

    //If will be added more mechanics to this object move current code into new PlayerShootController script and use this script as facade
    private void Start()
    {
        inputProvider.OnAttackLoadingStart += StartLoading;
        inputProvider.OnAttackLoadingFinished += Attack;

        currentFuel = fuelAmount;
    }

    private void SpawnProjectile()
    {
        if (spawnedProjectile != null)
            return;

        spawnedProjectile = poolService.Spawn(projectilePrefab, parent: projectileSpawnPoint);
        spawnedProjectile.transform.localPosition = Vector3.zero;

        spawnedProjectile.OnDestory += SpawnedProjectile_OnDestory;
    }

    private void SpawnedProjectile_OnDestory()
    {
        poolService.Despawn(spawnedProjectile);
        spawnedProjectile.OnDestory -= SpawnedProjectile_OnDestory;
        spawnedProjectile = null;

        inputProvider.OnAttackLoadingStart += StartLoading;
        inputProvider.OnAttackLoadingFinished += Attack;
    }

    private void Attack()
    {
        if (startLoadingRoutine != null)
            StopCoroutine(startLoadingRoutine);

        inputProvider.OnAttackLoadingStart -= StartLoading;
        inputProvider.OnAttackLoadingFinished -= Attack;

        startLoadingRoutine = null;

        if (spawnedProjectile != null && spawnedProjectile.CanFly)
            spawnedProjectile.StartFlying();
    }

    private void StartLoading()
    {
        if (startLoadingRoutine != null || spawnedProjectile != null)
            return;

        SpawnProjectile();
        startLoadingRoutine = StartCoroutine(StartLoadingCoroutine());
    }

    private IEnumerator StartLoadingCoroutine()
    {
        while(currentFuel > 0)
        {
            float amountToSpend = Mathf.Clamp(fuelAmount * loadingSpeedPercent * Time.deltaTime, 0, currentFuel);
            spawnedProjectile.AddFuel(amountToSpend);
            currentFuel -= amountToSpend;
            UpdateAnimateObject();
            yield return null;
        }
        startLoadingRoutine = null;
    }

    public void UpdateAnimateObject()
    {
        animateObject.localScale = currentFuel * scalePerFuelAmount * Vector3.one;
    }
}
