using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFiller : MonoBehaviour
{
    public GameObject cubePrefab;
    public List<GameObject> cubes;
    public int cubeSize_x = 9;
    public int cubeSize_y = 6;
    public int cubeSize_z = 9;
    public int offset = 1;
    public float scale = 0.4f;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = this.transform.localScale;
        GenerateCubes();
    }

    void Update()
    {
        if (cubes.Count == 0)
        {
            GenerateCubes();
        }
    }

    void GenerateCubes()
    {
        this.transform.localScale = originalScale;

        for (int y = 0; y<cubeSize_y*offset; y += offset)
        {
            for (int z = 0; z<cubeSize_z*offset; z += offset)
            {
                for (int x = 0; x<cubeSize_x*offset; x += offset)
                {
                    Vector3 location = new Vector3(transform.position.x + x,
                                                   transform.position.y + y,
                                                   transform.position.z + z);
                    GameObject cube = Instantiate(cubePrefab, location, Quaternion.identity);
                    cube.transform.SetParent(this.transform);
                    cubes.Add(cube);
                }
            }
        }

        this.transform.localScale = scale * originalScale;   
    }
}
