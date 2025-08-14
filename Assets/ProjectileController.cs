using NoctisDev.Pooling.Runtime.Services;
using UnityEngine;
using Zenject;

public class ProjectileController : MonoBehaviour, NoctisDev.Pooling.Runtime.Pools.IPoolable
{
    [Inject] private ColliderProvider colliderProvider;
    [Inject] private IPoolService poolService;

    [SerializeField] private ProjectileMovementController movementController;
    [SerializeField] private Transform animateObject;
    [SerializeField] private float sizePerFuelPoint = 4;
    [SerializeField] private float explodeRadiusPerFuelCount = 10;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private Transform destroyProjectileFXPrefab;
    [SerializeField] private float fxDespawnDelay = 3;

    private float currentFuel;
    public bool CanFly => currentFuel > 0;

    public event System.Action OnDestory;

    private void Start()
    {
        movementController.OnArrive += Explode;
    }

    private void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explodeRadiusPerFuelCount * currentFuel, obstacles);

        foreach (var collider in colliders)
        {
            if(colliderProvider.TryGetObstacle(collider, out ObstacleController obstacle))
            {
                obstacle.Infect();
            }
        }
        var destroyFX = poolService.Spawn(destroyProjectileFXPrefab);
        poolService.Despawn(destroyFX, fxDespawnDelay);
        destroyFX.localScale = currentFuel * explodeRadiusPerFuelCount * Vector3.one;
        destroyFX.transform.position = animateObject.transform.position;
        OnDestory?.Invoke();
    }

    public void OnDespawn()
    {
        
    }

    public void OnSpawn()
    {
        currentFuel = 0;
    }

    internal void AddFuel(float fuelAmount)
    {
        currentFuel += fuelAmount;
        animateObject.transform.localScale = currentFuel * sizePerFuelPoint * Vector3.one;
    }

    internal void StartFlying()
    {
        movementController.StartFlying(animateObject.transform.localScale.magnitude, animateObject.transform.position);
    }
}
