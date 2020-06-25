using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bullet;
    public GameObject gun;

    void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        gun.SetActive(true);
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        StartCoroutine(ExecuteAfterTime(0.25f));
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gun.SetActive(false);
    }
}
