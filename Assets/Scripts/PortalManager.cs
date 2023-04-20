using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance;
    [SerializeField] private Renderer stencilMask;
    [SerializeField] private GameObject startingRoom;
    [SerializeField] private UniversalRendererData rendererData;
    public Material[] stencils;
    public GameObject[] rooms;
    private RenderObjects depthPass;
    [SerializeField] private LayerMask depthMask;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Transform child in startingRoom.transform)
        {
            if (child.CompareTag("Collider"))
            {
                child.gameObject.GetComponent<Collider>().enabled = true;
            }
        }
        foreach (var feature in rendererData.rendererFeatures)
        {
            if(feature.name == "Depth Pass")
            {
                depthPass = (RenderObjects)feature;
            }
        }
        depthMask = 1 << startingRoom.layer;

        depthPass.settings.filterSettings.LayerMask = depthMask;
        depthPass.Create();
    }

    public void SetStencilMask(int stencil)
    {
        stencilMask.material = stencils[stencil];
        depthMask = 1 << rooms[stencil].layer;
        depthPass.settings.filterSettings.LayerMask = depthMask;
        depthPass.Create();
    }
}
