using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TeamRegion : MonoBehaviour
{
    public abstract GameObject SpawnObstacle(E_Direction dir, Timer timer, float timeForArrival, float locY);
    public virtual void SpawnMonster(E_Direction dir, GameObject player, GameObject spawner ,Timer timer, float timeForArrival, float locY) { }
    
}
