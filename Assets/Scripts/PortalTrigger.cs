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
    [SerializeField] private bool reversed;

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
            var forwardDirection = axis switch
            {
                Axis.Red => transform.right,
                Axis.Green => transform.up,
                Axis.Blue => transform.forward,
                _ => transform.right,
            };
            bool forward = Vector3.Dot(direction, forwardDirection) < 0;
            forward = reversed ? !forward : forward;
            int stencil;
            GameObject roomOn, roomOff;
            if (forward && PortalManager.instance.roomIndex == startRoom)
            {
                stencil = endRoom % Constants.NUM_STENCILS;
                roomOn = PortalManager.instance.rooms[endRoom];
                roomOff = PortalManager.instance.rooms[startRoom];
            }
            else if(PortalManager.instance.roomIndex == endRoom)
            {
                stencil = startRoom % Constants.NUM_STENCILS;
                roomOn = PortalManager.instance.rooms[startRoom];
                roomOff = PortalManager.instance.rooms[endRoom];
            }
            else
            {
                return;
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
            PortalManager.instance.roomIndex = forward ? endRoom : startRoom;
        }
    }
}
