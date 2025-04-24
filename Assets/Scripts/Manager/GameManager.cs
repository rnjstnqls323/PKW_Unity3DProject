using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private GameObject _player;
    public GameObject Player
    { get { return _player; } }

    private GameObject _minotaurs;
    public GameObject Minotaurs
    { get { return _minotaurs; } }

    void Awake()
    {
        _instance = this;
        SpawnPlayer();
        SpawnMinotaurs();
    }

    private void SpawnPlayer()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Player");
        
        _player = Instantiate(prefab);
    }

    private void SpawnMinotaurs()
    {
        GameObject minotaursPrefab = Resources.Load<GameObject>("Prefabs/Minotaurs");

        _minotaurs = Instantiate(minotaursPrefab);
    }
}
