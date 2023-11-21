using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public Vector2 size = Vector2.one * 4;
    public bool isConnected;
    private DungeonGenerator dungeonGenerator;

    private void Start()
    {
        dungeonGenerator = GameObject.Find("Generator").GetComponent<DungeonGenerator>();
    }

    private void OnDrawGizmos()
    {
        //if (dungeonGenerator.drawGizmos && dungeonGenerator != null)
        //{
            Gizmos.color = Color.cyan;
            Vector2 halfSize = size * 0.5f;
            Vector3 offset = transform.position + transform.up * halfSize.y;

            // Define top and side vectors
            Vector3 top = transform.up * size.y;
            Vector3 side = transform.right * halfSize.x;
            // Define corner vectors
            Vector3 topRight = transform.position + top + side;
            Vector3 topLeft = transform.position + top - side;
            Vector3 bottomRight = transform.position + side;
            Vector3 bottomLeft = transform.position - side;

            // Draw Lines
            Gizmos.DrawLine(offset, offset + transform.forward);
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, bottomLeft);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomLeft, bottomRight);

            // Draw diagonal lines
            Gizmos.color *= 0.7f;
            Gizmos.DrawLine(topRight, offset);
            Gizmos.DrawLine(topLeft, offset);
            Gizmos.DrawLine(bottomRight, offset);
            Gizmos.DrawLine(bottomLeft, offset);
        //}
        //else { return; }
    }

    
}
