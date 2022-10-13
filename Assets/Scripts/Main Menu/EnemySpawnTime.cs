
using System;
using UnityEngine;

public class EnemySpawnTime : MonoBehaviour
{
    public void OnClickNext()
    {
        int enemySpawnTimeValue = Int32.Parse(GetComponentInParent<Options>().enemySpawnTime.text);
        if (enemySpawnTimeValue < 30) GetComponentInParent<Options>().enemySpawnTime.text = (enemySpawnTimeValue + 5).ToString();
    }

    public void OnClickPrevious()
    {
        int enemySpawnTimeValue = Int32.Parse(GetComponentInParent<Options>().enemySpawnTime.text);
        if (enemySpawnTimeValue > 15) GetComponentInParent<Options>().enemySpawnTime.text = (enemySpawnTimeValue - 5).ToString();
    }
}
