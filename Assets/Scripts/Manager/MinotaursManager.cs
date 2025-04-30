using System.Collections.Generic;
using UnityEngine;

public class MinotaursManager : MonoBehaviour
{
    public static MinotaursManager Instance;

    [SerializeField]
    private GameObject minotaurPrefab;

    [SerializeField]
    private Vector3[] spawnPositions;

    private List<GameObject> minotaurPool = new List<GameObject>();
    private int poolSize = 7;

    private Transform monsterParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GameObject monsterObj = GameObject.Find("Monster");
        if (monsterObj != null)
        {
            monsterParent = monsterObj.transform;
        }
        else
        {
            Debug.LogWarning("하이라키에 'Monster' 오브젝트가 없습니다. 미노타우르스가 월드 루트에 생성됩니다.");
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject minotaur = Instantiate(minotaurPrefab);

            if (monsterParent != null)
                minotaur.transform.SetParent(monsterParent);

            minotaur.SetActive(false);
            minotaurPool.Add(minotaur);
        }
    }

    public void SpawnAllMinotaurs()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject minotaur = minotaurPool[i];
            minotaur.transform.position = spawnPositions[i];
            minotaur.SetActive(true);
        }
    }

    public void DeactivateAllMinotaurs()
    {
        foreach (GameObject minotaur in minotaurPool)
        {
            minotaur.SetActive(false);
        }
    }
}
