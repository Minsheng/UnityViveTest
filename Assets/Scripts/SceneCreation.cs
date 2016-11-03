using UnityEngine;
using System.Collections.Generic;
using ProceduralToolkit;

public class SceneCreation : MonoBehaviour {

    //public int count = 0;
    //private float colorRandomizer = 0.2f;
    //public Color colorPicker;
    private Color newColor;
    public GameObject[] meshes; // list of cubes in the world

    public float cubeSize = 10.0f;
    public float cubeSpacing = 1.0f;

    public bool isGravityOn = true;

    /* Load csv data */
    public TextAsset file;

    public class Row
    {
        public string WardNumber;
        public string HouseholdNum;
        public string UniverstiyDegree;
        public string Employed;
    }

    List<Row> rowList = new List<Row>();
    bool isLoaded = false;

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.WardNumber = grid[i][0];
            row.HouseholdNum = grid[i][1];
            row.UniverstiyDegree = grid[i][2];
            row.Employed = grid[i][3];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_WardNumber(string find)
    {
        return rowList.Find(x => x.WardNumber == find);
    }
    public List<Row> FindAll_WardNumber(string find)
    {
        return rowList.FindAll(x => x.WardNumber == find);
    }
    public Row Find_HouseholdNum(string find)
    {
        return rowList.Find(x => x.HouseholdNum == find);
    }
    public List<Row> FindAll_HouseholdNum(string find)
    {
        return rowList.FindAll(x => x.HouseholdNum == find);
    }
    public Row Find_UniverstiyDegree(string find)
    {
        return rowList.Find(x => x.UniverstiyDegree == find);
    }
    public List<Row> FindAll_UniverstiyDegree(string find)
    {
        return rowList.FindAll(x => x.UniverstiyDegree == find);
    }
    public Row Find_Employed(string find)
    {
        return rowList.Find(x => x.Employed == find);
    }
    public List<Row> FindAll_Employed(string find)
    {
        return rowList.FindAll(x => x.Employed == find);
    }

    void Start () {
        Load(file);
        if (file)
        {
            int count = NumRows();
            Debug.Log("Start Generating " + count + " meshes...");
            generateMesh(count);
        }
        Debug.Log("Meshes successfully generated!");
    }

    void generateMesh(int meshCount)
    {
        meshes = new GameObject[meshCount];

        int tileSize = (int) Mathf.Ceil(Mathf.Sqrt(meshCount));

        float sizePlusSpacing = (cubeSize + cubeSpacing);

        float startX = Mathf.Floor(tileSize / -2.0f)* sizePlusSpacing;
        float startY = startX;

        if (meshCount > 0)
        {
            for (int i = 0; i < meshCount; i++)
            {
                // position in a grid
                float x = startX + (i % tileSize) * sizePlusSpacing;
                float y = startY + Mathf.Floor(i / (float)tileSize) * sizePlusSpacing;

                GameObject go = new GameObject("New Object");
                go.AddComponent<MeshRenderer>();
                go.AddComponent<MeshFilter>();

                // conditionally generate meshes and their colliders
                //if (i % 2 == 0)
                //{
                //    float r = 1f + i / 10;
                //    go.GetComponent<MeshFilter>().mesh =  MeshE.Octahedron(r);
                //    go.AddComponent<SphereCollider>();
                //    go.GetComponent<SphereCollider>().radius = r;
                //} else
                //{
                //    go.GetComponent<MeshFilter>().mesh = MeshE.Hexahedron(1f, 1f, 1f+i); // width, length, height
                //    go.AddComponent<BoxCollider>();
                //    go.GetComponent<BoxCollider>().size = new Vector3(1f, 1f + i, 1f); // x, y (height), z
                //}

                float householdNum = float.Parse(GetAt(i).HouseholdNum);
                float universityDegree = float.Parse(GetAt(i).UniverstiyDegree);
                float employed = float.Parse(GetAt(i).Employed);

                float scaledHeight = superLerp(0.0f, 2.0f, 16.0f, 46.0f, householdNum);

                go.GetComponent<MeshFilter>().mesh = MeshE.Hexahedron(cubeSize, cubeSize, scaledHeight); // width, length, height
                go.AddComponent<BoxCollider>();
                go.GetComponent<BoxCollider>().size = new Vector3(cubeSize, scaledHeight, cubeSize); // x, y (height), z
                go.AddComponent<Rigidbody>();
                go.GetComponent<Rigidbody>().useGravity = isGravityOn;

                //go.GetComponent<BoxCollider>().isTrigger = true;

                float colorFrom = 0.0f;
                float colorTo = 1.0f;
                float red = superLerp(colorFrom, colorTo, 16.0f, 46.0f, householdNum);
                float green = superLerp(colorFrom, colorTo, 1.0F, 18.0F, universityDegree);
                float blue = superLerp(colorFrom, colorTo, 19.0F, 48.0F, employed);

                //Debug.Log("Red color value is " + red + "," + green + "," + blue);
                //go.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                go.GetComponent<Renderer>().material.color = new Color(red, green, blue,1);
                
                // update position of cube
                go.transform.position = new Vector3(x, 0, y);
                meshes[i] = go;
            }
        }
    }

    // Originally written by Eric5h5
    // https://forum.unity3d.com/threads/mapping-or-scaling-values-to-a-new-range.180090/
    float superLerp(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        } else if (value >= to2)
        {
            return to;
        }
        return (to - from) * ((value - from2) / (to2 - from2) + from);
    }

    //void changeColor()
    //{
    //    for (int i = 0; i < meshes.Length; i++)
    //    {
    //        float randomColor = Random.Range(-colorRandomizer, colorRandomizer);
    //        newColor.r = colorPicker.r + randomColor;
    //        newColor.g = colorPicker.g + randomColor;
    //        newColor.b = colorPicker.b + randomColor;

    //        Debug.Log("The new color has values: " + newColor.r + ":" + newColor.g + ":" + newColor.b);

    //        meshes[i].GetComponent<Renderer>().material.color = newColor;
    //    }
    //}
}
