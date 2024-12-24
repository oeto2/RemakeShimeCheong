using System;
using Constants;
using UnityEngine;

public class MadangTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == ObjectLayer.PlayerLayer)
        {
            DialogueManager.Instance.StartTalk(7040);
            gameObject.SetActive(false);
        }
    }
}
