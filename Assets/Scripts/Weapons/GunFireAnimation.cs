using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireAnimation : MonoBehaviour
{

    public void AnimationComplete()
    {
        Destroy(gameObject);
    }

}
