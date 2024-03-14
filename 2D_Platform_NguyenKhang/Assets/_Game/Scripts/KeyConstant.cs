using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyConstant
{
    public const string KEY_COIN = "coin";
}
public class PathConstant
{
    public const string MAP_PATH = "Map/Map_";
}
public enum StateAmin
{   
    idle,
    hit,
    run,
    attack,
    fall,
    die,
    throww,
    jump
}
public enum Tag
{
    EnemyWall,
    Coin,
    DeadZone
}