using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask interactionMask;
    public event System.Action<Collider> OnInteractionEnter;

    private void OnTriggerEnter(Collider collider)
    {

        if (interactionMask.Includes(collider.gameObject.layer))
            OnInteractionEnter?.Invoke(collider);
    }
}
