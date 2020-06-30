using UnityEngine;

public class Shield : MonoBehaviour // This file holds logic for the shield itself, while shielding is logic for how to spawn/accelerate the shield
{
    public float shieldDuration = 2f;

    void Start() 
    {
        Destroy(gameObject, shieldDuration);
    }
}
