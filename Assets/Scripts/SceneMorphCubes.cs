using UnityEngine;
using System.Collections;

public class SceneMorphCubes : MonoBehaviour {
    public int count = 0;
    private GameObject[] meshes; // list of meshes in the world

    public float cubeSize = 2.0f;
    public float cubeSpacing = 0.05f;

    void Start()
    {
        Debug.Log("Start Generating " + count + " meshes...");
        generateMesh(count);
    }

    void Update()
    {

    }

    void generateMesh(int count)
    {
        meshes = new GameObject[count];

        int tileSize = (int)Mathf.Ceil(Mathf.Sqrt(count));

        Debug.Log("tile size is " + tileSize);

        float sizePlusSpacing = (cubeSize+cubeSpacing);

        Debug.Log("Spacing is " + sizePlusSpacing);

        float startX = Mathf.Floor(tileSize / -2.0f) * sizePlusSpacing;
        float startY = startX;

        Debug.Log("start X: " + startX + "," + "start Y: " + startY);

        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                // position in a grid
                float x = startX + (i % tileSize) * sizePlusSpacing;
                float y = startY + Mathf.Floor(i / (float)tileSize) * sizePlusSpacing;

                Debug.Log("Cube position: " + x + "," + y);

                GameObject go = new GameObject("New Cube");
                go.AddComponent<MeshRenderer>();
                go.AddComponent<MeshFilter>();
                go.AddComponent<buildMesh>();
                go.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);

                // update position of cube
                go.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                go.transform.position = new Vector3(x, 0.5f, y);
                meshes[i] = go;
            }
        }
    }
}
