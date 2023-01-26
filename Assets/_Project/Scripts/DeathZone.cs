using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out Player player))
            {
                if(!player.IsDead)
                    player.Death();
            }

            StartCoroutine(WaitToReload(3));
        }
    }
    
    private IEnumerator WaitToReload(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneLoader.instance.ReloadScene();
    }
}
