using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationTerrain : MonoBehaviour
{
    [Range(1,50)]
    public int size = 2;
    public Material material;
    public float speedGeneration = 0;
    public Vector2 perlyNoize;

    private Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    private MeshFilter mf;
    private MeshRenderer mr;

    private void Start()
    {
        mesh = new Mesh();
        mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;

        StartCoroutine(Generation());
    }

    IEnumerator Generation()
    {
        vertices = new Vector3[(size+1) * (size+1)];
        triangles = new int[size * size * 6];

        for (int i = 0, z = 0; z <= size; z++)
        {
            for (int x = 0; x <= size; x++)
            {
                float y = Mathf.PerlinNoise(perlyNoize.x * x, perlyNoize.y * z) * 2f;
                vertices[i] = new Vector3(x, y, z) + transform.position;
                i++;
            }
        }

        int trianStep = 0;
        int vertStep = 0;

        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                triangles[0 + trianStep] = vertStep;
                triangles[1 + trianStep] = vertStep + size + 1;
                triangles[2 + trianStep] = vertStep + 1;
                triangles[3 + trianStep] = vertStep + 1;
                triangles[4 + trianStep] = vertStep + size + 1;
                triangles[5 + trianStep] = vertStep + size + 2;
                trianStep += 6;
                vertStep++;
                UpdateMesh();
                yield return new WaitForSeconds(speedGeneration);
            }
            vertStep++;
        }
    }
   
    void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
