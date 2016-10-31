using UnityEngine;
using System.Collections;

public class ProceduralMesh : MonoBehaviour {
    public int cubeCount = 0;

    void Start () {
        Debug.Log("Generating meshes...");
        generateMesh();
	}

	void Update () {
	
	}

    void generateMesh()
    {
        if (cubeCount > 0)
        {
            for (int i = 0; i < cubeCount; i++)
            {
                
            }
        }
    }
}
