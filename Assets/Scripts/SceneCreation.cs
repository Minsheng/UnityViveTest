using UnityEngine;
using System.Collections;
using ProceduralToolkit.Examples.Primitives;
using ProceduralToolkit;

public class SceneCreation : MonoBehaviour {

    public int count = 0;
    private float colorRandomizer = 0.2f;
    public Color colorPicker;
    private Color newColor;
    private GameObject[] cubes; // list of cubes in the world

    public float cubeSize = 10.0f;
    public float cubeSpacing = 1.0f;

    void Start () {
        Debug.Log("Start Generating " + count + " meshes...");
        generateMesh(count);
        Debug.Log("Meshes successfully generated!");
    }

    void Update()
    {

    }

    void changeColor()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            float randomColor = Random.Range(-colorRandomizer, colorRandomizer);
            newColor.r = colorPicker.r + randomColor;
            newColor.g = colorPicker.g + randomColor;
            newColor.b = colorPicker.b + randomColor;

            Debug.Log("The new color has values: " + newColor.r + ":" + newColor.g + ":" + newColor.b);

            cubes[i].GetComponent<Renderer>().material.color = newColor;
        }
    }

    void generateMesh(int cubeCount)
    {
        cubes = new GameObject[cubeCount];

        int tileSize = (int) Mathf.Ceil(Mathf.Sqrt(cubeCount));

        float sizePlusSpacing = (cubeSize + cubeSpacing);

        float startX = Mathf.Floor(tileSize / -2.0f)* sizePlusSpacing;
        float startY = startX;

        if (cubeCount > 0)
        {
            for (int i = 0; i < cubeCount; i++)
            {
                // position in a grid
                float x = startX + (i % tileSize) * sizePlusSpacing;
                float y = startY + Mathf.Floor(i / (float)tileSize) * sizePlusSpacing;

                GameObject go = new GameObject("New Cube");
                go.AddComponent<MeshRenderer>();
                go.AddComponent<MeshFilter>();
                if (i % 2 == 0)
                {
                    go.GetComponent<MeshFilter>().mesh =  MeshE.Octahedron(1f+i/10);
                } else
                {
                    go.GetComponent<MeshFilter>().mesh = MeshE.Hexahedron(1f, 1f, 1f+i);
                }
                
                go.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);

                // update position of cube
                go.transform.position = new Vector3(x, 0, y);
                cubes[i] = go;
            }
        }
    }

    //private float[][] buildPositionMap(int cubeCount)
    //{
    //    float[][] map = new float[cubeCount][];

    //    for (int x = 0; x < map.Length; x++)
    //    {
    //        for (int z = 0; z < map.Length; z++)
    //        {
    //            map[x][1] = z;
    //        }
    //    }

    //    return map;
    //}
}
