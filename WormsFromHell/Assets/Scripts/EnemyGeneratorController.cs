using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    // En teoría debeía estar ordeando (?)
    public Enemy[] _enemyArray;
    public float _generatorTimer =3f;
    public int _actualLevelSpawn = 1;
    public int levelSpawnRange = 4;

    private List<GameObject> _enemyPrefab;

    void Start()
    {
        _enemyPrefab = new List<GameObject>();
        StartCoroutine(GenerateEnemy());
    }

    IEnumerator GenerateEnemy() {
        while (true) {
            yield return new WaitForSeconds(_generatorTimer);


            Instantiate(_enemyArray[Random.Range(0, _enemyArray.Length)].gameObject, transform.position, Quaternion.identity);
        }
    }

    private void MakeEnemyList() {
        _enemyPrefab.Clear();
        foreach (Enemy enemy in _enemyArray) {
            int dif = (int)Mathf.Abs(enemy._level -_actualLevelSpawn);
            if (dif < levelSpawnRange) {
                _enemyPrefab.Add(enemy.gameObject);
            }
        }
    }
}
