using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    Red,
    Green,
    Blue,
}

public class PortalTrigger : MonoBehaviour
{
    public int startRoom;
    public int endRoom;

    [SerializeField] private Axis axis;

    private void Awake()
    {
        startRoom--;
        endRoom--;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 realPos = transform.position + new Vector3(1,1,0);
            Vector3 direction = (other.transform.position - realPos).normalized;
            Vector3 forwardDirection;
            switch (axis)
            {
                case Axis.Red:
                    forwardDirection = transform.right;
                    break;
                case Axis.Green:
                    forwardDirection = transform.up;
                    break;
                case Axis.Blue:
                    forwardDirection = transform.forward;
                    break;
                default:
                    forwardDirection = transform.right;
                    break;
            }
            bool forward = Vector3.Dot(direction, forwardDirection) < 0;
            int stencil;
            GameObject roomOn, roomOff;
            if (forward)
            {
                stencil = endRoom;
                roomOn = PortalManager.instance.rooms[endRoom];
                roomOff = PortalManager.instance.rooms[startRoom];
            }
            else
            {
                stencil = startRoom;
                roomOn = PortalManager.instance.rooms[startRoom];
                roomOff = PortalManager.instance.rooms[endRoom];
            }
            
            foreach (Transform child in roomOn.transform)
            {
                if (child.CompareTag("Collider"))
                {
                    child.gameObject.GetComponent<Collider>().enabled = true;
                }
            }
            foreach(Transform child in roomOff.transform)
            {
                if (child.CompareTag("Collider"))
                {
                    child.gameObject.GetComponent<Collider>().enabled = false;
                }
            }
            
            PortalManager.instance.SetStencilMask(stencil);
        }
    }
}
