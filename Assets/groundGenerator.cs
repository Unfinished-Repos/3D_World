using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class groundGenerator : MonoBehaviour {
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize, zSize;

    void Start() {
      mesh = new Mesh();
      GetComponent<MeshFilter>().mesh = mesh;

      CreateShape();
      UpdateMesh();
    }

    void CreateShape () {
      vertices = new Vector3[(xSize + 1) * (zSize + 1)];

      // Setting up initial points
      for (int i = 0, x = 0; x <= xSize; x++) {
        for (int z = 0; z <= zSize; z++) {
          float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
          vertices[i++] = new Vector3(x, y, z);
        }
      }
      mesh.vertices = vertices;

      // Creating triangles
      triangles = new int[xSize * zSize * 6];
      int vert = 0;
      int tris = 0;

      for (int z = 0; z < zSize; z++) {
        for (int x = 0; x < xSize; x++) {
          triangles[tris++] = vert + 0;
          triangles[tris++] = vert + 1;
          triangles[tris++] = vert + xSize + 1;
          triangles[tris++] = vert + 1;
          triangles[tris++] = vert + xSize + 2;
          triangles[tris++] = vert + xSize + 1;

          vert++;
        }
        vert++;
      }
      mesh.triangles = triangles;
    }

    void UpdateMesh () {
      mesh.Clear();

      mesh.vertices = vertices;
      mesh.triangles = triangles;

      mesh.RecalculateNormals();
    }

    private void OnDrawGizmos () {
      if (vertices == null)
        return;

      for (int i = 0; i < vertices.Length; i++) {
        Gizmos.DrawSphere(vertices[i], .1f);
      }
    }
}
