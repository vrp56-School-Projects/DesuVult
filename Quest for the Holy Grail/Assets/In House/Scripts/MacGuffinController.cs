using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacGuffinController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.EnemyDamaged += OnDamaged;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,rotateSpeed * Time.deltaTime,0f));
    }

    void OnDamaged(float dummy, GameObject GO){
        if (GO == gameObject)
        {
            EventManager.CallStaggerBoss();
            EventManager.EnemyDamaged -= OnDamaged;
            Destroy(gameObject);

        }
    }
}
