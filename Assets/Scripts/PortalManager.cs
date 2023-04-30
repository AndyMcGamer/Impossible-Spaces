using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance;
    [SerializeField] private Renderer stencilMask;
    [SerializeField] private UniversalRendererData rendererData;
    public Material[] stencils;
    public GameObject[] rooms;
    private RenderObjects depthPass;
    [SerializeField] private LayerMask depthMask;
    [Tooltip("Set the value of this variable to be the starting room")]
    public int roomIndex;

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
        roomIndex--;
        foreach (Transform child in rooms[roomIndex].transform)
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
        depthMask = 1 << rooms[roomIndex].layer;

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

    private void OnApplicationQuit()
    {
        depthMask = 0;
        depthPass.settings.filterSettings.LayerMask = depthMask;
        depthPass.Create();
    }
}
