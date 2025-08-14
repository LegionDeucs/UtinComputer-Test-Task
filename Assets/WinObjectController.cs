using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WinObjectController : MonoBehaviour
{
    [Inject] private GameplayContext gameplayContext;

    [SerializeField] private PhysicsTrigger physicsTrigger;
    void Start()
    {
        physicsTrigger.OnInteractionEnter += PhysicsTrigger_OnInteractionEnter;
    }

    private void PhysicsTrigger_OnInteractionEnter(Collider obj)
    {
        gameplayContext.Win();
    }
}
