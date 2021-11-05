using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTransform : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verts;
    int[] faces;
    Vector3[] transformed;

    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Create a mesh object to define the shape of the model
        mesh = new Mesh();
        // Assign the mesh to the game object
        GetComponent<MeshFilter>().mesh = mesh;

        //// GEOMETRY ////
        // An array to store the vertex coordinates
        verts = new Vector3[4];
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(6, 0, 0);
        verts[2] = new Vector3(3, 6, 0);
        verts[3] = new Vector3(3, -6, 0);

        //// TOPOLOGY ////
        // An array to describe the connections between vertices
        faces = new int[9];
        faces[0] = 0;
        faces[1] = 2;
        faces[2] = 1;
        faces[3] = 0;
        faces[4] = 1;
        faces[5] = 2;
        faces[6] = 0;
        faces[7] = 3;
        faces[8] = 1;

        transformed = new Vector3[verts.Length];
    }

    void Update()
    {
        angle += 1;

        //// TRANSFORMATIONS ////
        // Create a transformation matrix
        Matrix4x4 translate = Transforms.MakeTranslate(7, 5, 3);
        //Matrix4x4 scale = MakeScale(2, 0.5f, 1);
        Matrix4x4 transOrigin = Transforms.MakeTranslate(-3, -6, 0);
        Matrix4x4 transObject = Transforms.MakeTranslate(3, 6, 0);
        Matrix4x4 rotZ = Transforms.MakeRotateY(angle);
        Matrix4x4 combined = transObject * rotZ * transOrigin;
        // Apply the transformation to each vertex
        for (int i=0; i<verts.Length; i++) {
            Vector4 temp = new Vector4(verts[i].x, verts[i].y, verts[i].z, 1);
            transformed[i] = combined * temp;
        }

        // Store the data in the mesh
        mesh.vertices = transformed;
        mesh.triangles = faces;
        mesh.RecalculateNormals();
    }
}
