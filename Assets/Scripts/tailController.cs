using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tailController : MonoBehaviour
{
    float _speed;
    GameObject _head;
    List<GameObject> all;
    [SerializeField] GameObject _target;
    void Start()
    {
        _head = GameObject.Find("head_zmea");
        all = _head.GetComponent<moveSnake>().allLength;
        _speed = 10;
        _target = all[all.Count - 2];
        if (all.Count < 10)
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
    void Update()
    {
        Vector3 _targetPos = _target.transform.position;
        transform.LookAt(_target.transform, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, _targetPos, (_speed - 5) * Time.deltaTime);
    }
}
