using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int dificulty;
    [SerializeField]
    private Collider level;
    public GameObject[] obstacles;

    private void Start()
    {
        for (int i = 0; i < dificulty; i++)
        {
            GenerateObstacles();
        }

    }

    private void GenerateObstacles()
    {

        GameObject prefab = obstacles[Random.Range(0, obstacles.Length)];

        Vector3 position = new Vector3
        {
            x = Random.Range(level.bounds.min.x, level.bounds.max.x),
            y = Random.Range(level.bounds.min.y, level.bounds.max.y),
            z = Random.Range(level.bounds.min.z, level.bounds.max.z)
        };

        Quaternion rotation = Random.rotation;

        GameObject obstacle = Instantiate(prefab, position, rotation);

        var scale = Random.Range(0.5f, 1f);
        obstacle.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }


}
