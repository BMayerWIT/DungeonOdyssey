using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RenderLoading : MonoBehaviour
{

    public GameObject[] dynamicObjects;
    public float cullingDistance = 10f;

    private void Update()
    {
        CullObjects();
    }

    void CullObjects()
    {
        dynamicObjects = GameObject.FindGameObjectsWithTag("Tile");

        foreach (var obj in dynamicObjects)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            if (distance > cullingDistance)
            {
                // Disable or unload renderer component
                Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
                if (renderers != null)
                {
                    for (int i = 0; i < renderers.Length; i++)
                    {
                        renderers[i].enabled = false;


                    }
                }
            }
            else
            {
                // Enable renderer for nearby objects
                Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
                if (renderers != null)
                {
                    for (int i = 0; i < renderers.Length; i++)
                    {
                        renderers[i].enabled = true;


                    }

                }
            }
        }
    }
}

