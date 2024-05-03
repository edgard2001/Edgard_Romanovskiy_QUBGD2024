using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //#if UNITY_EDITOR
        //    // Application.Quit() does not work in the editor so
        //    // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        //    UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //    Application.Quit();
        //#endif

        //Explodable explode = other.gameObject.transform.parent.GetComponentInParent<Explodable>();
        //if (explode != null)
        //   explode.Explode();
    }
}
