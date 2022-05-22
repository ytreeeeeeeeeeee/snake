using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFood : MonoBehaviour
{
    [SerializeField] float _offset;
    [SerializeField] List<GameObject> _food = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(_food[Random.Range(0, _food.Count)], new Vector3(Random.Range(-_offset, _offset), 1.2f, Random.Range(-_offset, _offset)), Quaternion.identity);
        }
    }
    void Update()
    {
        
    }
    public void SpawnNew()
    {
        Instantiate(_food[Random.Range(0, _food.Count)], new Vector3(Random.Range(-_offset, _offset), 1.2f, Random.Range(-_offset, _offset)), Quaternion.identity);
    }
}
