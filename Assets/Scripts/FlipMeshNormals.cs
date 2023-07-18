using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMeshNormals : MonoBehaviour
{

    private void Start()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            Mesh mesh = meshCollider.sharedMesh;
            if (mesh != null)
            {
                int[] triangles = mesh.triangles;
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int temp = triangles[i];
                    triangles[i] = triangles[i + 2];
                    triangles[i + 2] = temp;
                }

                mesh.triangles = triangles;
                mesh.RecalculateNormals();
                meshCollider.sharedMesh = mesh;
            }
        }
    }
}
