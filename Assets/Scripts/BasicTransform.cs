using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTransform : MonoBehaviour
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

        //// TRANSFORMATIONS ////
        // Create a transformation matrix
        Matrix4x4 translate = MakeTranslate(7, 5, 3);
        Matrix4x4 scale = MakeScale(2, 0.5f, 1);
        Matrix4x4 combined = scale * translate;
        // Apply the transformation to each vertex
        for (int i=0; i<verts.Length; i++) {
            Vector4 temp = new Vector4(verts[i].x, verts[i].y, verts[i].z, 1);
            verts[i] = combined * temp;
        }

        // Store the data in the mesh
        mesh.vertices = verts;
        mesh.triangles = faces;
        mesh.RecalculateNormals();
    }

    Matrix4x4 MakeScale(float sx, float sy, float sz)
    {
        Matrix4x4 mat = Matrix4x4.identity;
        mat[0, 0] = sx;
        mat[1, 1] = sy;
        mat[2, 2] = sz;
        return mat;
    }

    Matrix4x4 MakeTranslate(float tx, float ty, float tz)
    {
        Matrix4x4 mat = Matrix4x4.identity;
        mat[0, 3] = tx;
        mat[1, 3] = ty;
        mat[2, 3] = tz;
        return mat;
    }
}
