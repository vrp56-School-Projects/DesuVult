using System;
using UnityEngine;

public class EventManager
{

    //Item Pickups
    public static event Action<float> Pickup;
    public static void CallPickup (float value) {
        Pickup?.Invoke(value);
    }

    public static event Action<float> HealthPickup;
    public static void CallHealthPickup (float value) {
        HealthPickup?.Invoke(value);
    }

    public static event Action<float> StaminaPickup;
    public static void CallStaminaPickup (float value) {
        StaminaPickup?.Invoke(value);
    }

    public static event Action<float> DeusVultPickup;
    public static void CallDeusVultPickup (float value) {
        DeusVultPickup?.Invoke(value);
    }
    //End Item Pickups

    public static event Action EnemyDied;
    public static void CallEnemyDied() {
        EnemyDied?.Invoke();
    }

    public static event Action EnemyNinjaDied;
    public static void CallEnemyNinjaDied() {
        EnemyNinjaDied?.Invoke();
    }

    public static event Action EnemySamuraiDied;
    public static void CallEnemySamuraiDied() {
        EnemySamuraiDied?.Invoke();
    }

    public static event Action PlayerDied;
    public static void CallPlayerDied() {
        PlayerDied?.Invoke();
    }

    public delegate void Damaged(float damage);
    public delegate void enemyDamaged(float damage, GameObject gameObject);
    public static event Damaged PlayerDamaged;
    public static void CallPlayerDamaged(float damage) {
        PlayerDamaged?.Invoke(damage);
    }

    public static event enemyDamaged EnemyDamaged;
    public static void CallEnemyDamaged(float damage, GameObject gameObject){
        EnemyDamaged?.Invoke(damage, gameObject);
    }



    public static event Action Explore;
    public static void CallExplored() {
        Explore?.Invoke();
    }
    public static event Action StaggerBoss;
    public static void CallStaggerBoss() {
        StaggerBoss?.Invoke();
    }

    public static event Action ChallengeCompleted;
    public static void CallChallengeCompleted() {
        ChallengeCompleted?.Invoke();
    }

    public delegate void Looked(RaycastHit hit);
    public static event Looked PlayerLooked;
    public static void CallPlayerLooked(RaycastHit hit) {
        PlayerLooked?.Invoke(hit);
    }
}
