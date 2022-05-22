using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moveSnake : MonoBehaviour
{
    public float speed;
    [SerializeField] float _rotSpeed;
    [SerializeField] GameObject _cam;
    [SerializeField] Text _score;
    [SerializeField] Text _nick;
    [SerializeField] GameObject _zopa;
    [SerializeField] float _timeNow;
    [SerializeField] GameObject scripts;
    [SerializeField] GameObject _gameUI;
    [SerializeField] GameObject _deadUI;
    [SerializeField] int _plusScore;
    bool _lowSpeedBool;
    bool _highSpeedBool;
    public List<GameObject> allLength = new List<GameObject>();
    void Start()
    {
        Time.timeScale = 1;
        string name = PlayerPrefs.GetString("Name");
        _nick.text = name;
        allLength.Add(this.gameObject);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * _rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * _rotSpeed * Time.deltaTime);
        }
        if (_lowSpeedBool)
        {
            if (_timeNow >= 7)
            {
                speed = 10;
                _lowSpeedBool = false;
            }
            else
            {
                _timeNow += Time.deltaTime;
            }
        }
        if (_highSpeedBool)
        {
            if (_timeNow >= 7)
            {
                speed = 10;
                _highSpeedBool = false;
            }
            else
            {
                _timeNow += Time.deltaTime;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "food")
        {
            _score.text = (int.Parse(_score.text) + _plusScore).ToString();
            Destroy(other.gameObject);
            _cam.GetComponent<spawnFood>().SpawnNew();
            Vector3 _newTailPos = allLength[allLength.Count - 1].transform.position;
            _newTailPos.z -= 3;
            allLength.Add(Instantiate(_zopa, _newTailPos, Quaternion.identity));
        }
        if (other.tag == "min")
        {
            if (allLength.Count == 1)
            {
                Dead();
            }
            else
            {
                Destroy(allLength[allLength.Count - 1].gameObject);
                allLength.RemoveAt(allLength.Count - 1);
                Destroy(other.gameObject);
                _cam.GetComponent<spawnFood>().SpawnNew();
                _score.text = (int.Parse(_score.text) - _plusScore).ToString();
            }
            
        }
        if (other.tag == "low")
        {
            _lowSpeedBool = true;
            Destroy(other.gameObject);
            _timeNow = 0;
            speed /= 1.5f;
            _cam.GetComponent<spawnFood>().SpawnNew();
        }
        if (other.tag == "high")
        {
            _highSpeedBool = true;
            Destroy(other.gameObject);
            _timeNow = 0;
            speed *= 1.5f;
            _cam.GetComponent<spawnFood>().SpawnNew();
        }
        if (other.tag == "wall")
        {
            Dead();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "wall")
        {
            Dead();
        }
    }
    void Dead()
    {
        scripts.GetComponent<DBManager>().addScore(_nick.text, int.Parse(_score.text));
        _gameUI.SetActive(false);
        _deadUI.SetActive(true);
        Time.timeScale = 0;
    }
}
