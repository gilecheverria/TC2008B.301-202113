using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Create a mesh object to define the shape of the model
        Mesh mesh = new Mesh();
        // Assign the mesh to the game object
        GetComponent<MeshFilter>().mesh = mesh;

        //// GEOMETRY ////
        // An array to store the vertex coordinates
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(6, 0, 0);
        verts[2] = new Vector3(3, 6, 0);
        verts[3] = new Vector3(3, -6, 0);

        //// TOPOLOGY ////
        // An array to describe the connections between vertices
        int[] faces = new int[9];
        faces[0] = 0;
        faces[1] = 2;
        faces[2] = 1;
        faces[3] = 0;
        faces[4] = 1;
        faces[5] = 2;
        faces[6] = 0;
        faces[7] = 3;
        faces[8] = 1;

        // Store the data in the mesh
        mesh.vertices = verts;
        mesh.triangles = faces;
        mesh.RecalculateNormals();
    }
}
