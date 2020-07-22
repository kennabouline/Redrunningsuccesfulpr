using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehaviour : MonoBehaviour
{
    // The list of tags that this effect will interact with when triggered
    public List<String> TagsToInteractWith;
    // The pickup effect to play when this game object is triggered
    public GameObject PickupEffect;
    // If the effect created is attached to the colliding object
    public bool AttachToCollidedObject = true;
    // How long to wait before destroying this object when its not attached
    public float DestroyTimeAfterTrigger = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        if (TagsToInteractWith.Contains(other.gameObject.tag))
        {
            // The effect we create when the pickup is triggered
            GameObject pickupEffect = (GameObject)Instantiate(PickupEffect, AttachToCollidedObject ? other.gameObject.transform.position : gameObject.transform.position, Quaternion.identity);
            if (AttachToCollidedObject)
            {
                pickupEffect.transform.parent = other.gameObject.transform;
                Destroy(gameObject);
                return;
            }
            Destroy(gameObject, DestroyTimeAfterTrigger);

        }
    }
}