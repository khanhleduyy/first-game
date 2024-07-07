using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObject/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public TypeOfEnemy typeOfEnemy;
    public int maxHealthAmmount;
    public int maxSpeedAmmount;
}

public enum TypeOfEnemy
{
    Suicide, 
    Shoot,
    Fly,
}
