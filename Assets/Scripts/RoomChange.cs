using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField] private GameObject shownRoom;
    [SerializeField] private bool show;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shownRoom.SetActive(show);
        }
    }
}
