using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRooms : MonoBehaviour
{
    [SerializeField] private Axis axis;
    [SerializeField] private bool reversed;
    [SerializeField] private GameObject[] turnOff;
    [SerializeField] private GameObject[] turnOn;

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Vector3 realPos = transform.position + new Vector3(1, 1, 0);
            Vector3 direction = (other.transform.position - realPos).normalized;
            var forwardDirection = axis switch
            {
                Axis.Red => transform.right,
                Axis.Green => transform.up,
                Axis.Blue => transform.forward,
                _ => transform.right,
            };
            bool forward = Vector3.Dot(direction, forwardDirection) < 0;
            forward = reversed ? !forward : forward;
            if (forward)
            {
                foreach (GameObject room in turnOff)
                {
                    room.SetActive(false);
                }
                foreach (GameObject room in turnOn)
                {
                    room.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject room in turnOn)
                {
                    room.SetActive(false);
                }
                foreach (GameObject room in turnOff)
                {
                    room.SetActive(true);
                }
            }
        }
    }
}
