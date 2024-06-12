using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Spawner : MonoBehaviour
{

    public float _delay = 0.4f;
    public GameObject _cube;

    private void Start()
    {
        InvokeRepeating("Spawn", _delay, _delay);
    }

    void Spawn()
    {
        Instantiate(_cube, new Vector3(UnityEngine.Random.Range(-6,6),10,0), Quaternion.identity);
    }

    private void Update()
    {
        
    }

}