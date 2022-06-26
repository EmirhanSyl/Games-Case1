using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int slimeMonsterCount;
    public int turtleMonsterCount;

    [HideInInspector] public float activeTurtleCount;
    [HideInInspector] public float activeSlimeCount;

    [SerializeField] private float spawnDuration;

    [SerializeField] private int firstTurtleInitiationCount = 2;
    [SerializeField] private int firstSlimeInitiationCount = 3;

    [SerializeField] private GameObject slimeMonster;
    [SerializeField] private GameObject turtleMonster;

    private float turtleSpawnTimer;
    private float slimeSpawnTimer;

    private List<Transform> spawns = new List<Transform>(); 


    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawns.Add(transform.GetChild(i));
        }

        for (int i = 0; i < firstSlimeInitiationCount; i++)
        {
            var slime = Instantiate(slimeMonster, spawns[Random.Range(0, spawns.Count)].position, Quaternion.identity);
            activeSlimeCount++;
        }
        for (int i = 0; i < firstTurtleInitiationCount; i++)
        {
            var turtle = Instantiate(turtleMonster, spawns[Random.Range(0, spawns.Count)].position, Quaternion.identity);
            activeTurtleCount++;
        }
    }

    
    void Update()
    {
        SpawnSlime();
        SpawnTurtle();
    }

    void SpawnTurtle()
    {
        turtleSpawnTimer += Time.deltaTime;
        if (turtleSpawnTimer < spawnDuration)
        {
            return;
        }

        if (activeTurtleCount < turtleMonsterCount)
        {
            Instantiate(turtleMonster, spawns[Random.Range(0, spawns.Count)].position, Quaternion.identity);
            activeTurtleCount++;
        }
        turtleSpawnTimer = 0;
    }

    void SpawnSlime()
    {
        slimeSpawnTimer += Time.deltaTime;
        if (slimeSpawnTimer < spawnDuration - 4f)
        {
            return;
        }

        if (activeSlimeCount < slimeMonsterCount)
        {
            Instantiate(slimeMonster, spawns[Random.Range(0, spawns.Count)].position, Quaternion.identity);
            activeSlimeCount++;
        }
        slimeSpawnTimer = 0;
    }
}
