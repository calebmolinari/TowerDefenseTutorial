using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyPooler : MonoBehaviour
{
    [System.Serializable] public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public EnemyData enemyData;
    }

    #region Singleton
    public static EnemyPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    EnemyData tempData;
    public EnemyData enemyDataReference;
    public EnemyData toughEnemyDataReference;
    EnemyController enemyController;
    int searchPool = 0;


    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                enemyController = obj.GetComponent<EnemyController>();
                enemyController.enemyData = ScriptableObject.CreateInstance<EnemyData>();
                SpawnSetStats(enemyController, pool.tag, pool.enemyData);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        EnemyController controller = objectToSpawn.GetComponent<EnemyController>();
        EnemyMovement movement = objectToSpawn.GetComponent<EnemyMovement>();
        movement.ResetWaypoints();

        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].tag == tag)
                searchPool = i;
        }
        SpawnSetStats(controller, tag, pools[searchPool].enemyData);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    void SpawnSetStats(EnemyController controller, string tag, EnemyData enemyData)
    {
        /*
        if (tag == "Enemy")
        {
            controller.enemyData.health = enemyDataReference.health;
            controller.enemyData.startHealth = enemyDataReference.startHealth;
            controller.enemyData.speed = enemyDataReference.speed;
            controller.enemyData.value = enemyDataReference.value;
        }
        if (tag == "EnemyTough")
        {
            controller.enemyData.health = toughEnemyDataReference.health;
            controller.enemyData.startHealth = toughEnemyDataReference.startHealth;
            controller.enemyData.speed = toughEnemyDataReference.speed;
            controller.enemyData.value = toughEnemyDataReference.value;
        }
        */
        controller.enemyData.health = enemyData.health;
        controller.enemyData.startHealth = enemyData.startHealth;
        controller.enemyData.speed = enemyData.speed;
        controller.enemyData.value = enemyData.value;
    }

}
