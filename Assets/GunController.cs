using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   
    [SerializeField] private Animator gunAnim;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1.2f;
    private bool gunFacingRight;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    public int currentBullets;
    public int maxBullets;

    private void Start(){
        ReloadBullets();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        gun.rotation = Quaternion.Euler(new Vector3(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0,0, angle) * new Vector3(gunDistance, 0,0);

        if (Input.GetKeyDown(KeyCode.Mouse0)){
            Shoot(direction);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            ReloadBullets();
        }

        GunFlipController(mousePos);
    }

    private void GunFlipController(Vector3 mousePos){
        if (mousePos.x < gun.position.x && gunFacingRight || mousePos.x > gun.position.x && !gunFacingRight){
            GunFlip();
        }
    }

    public void GunFlip(){
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    public void Shoot(Vector3 direction){

        if (currentBullets <= 0){
            return;    
        }

        gunAnim.SetTrigger("Shoot");

        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);

        currentBullets--;

        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;

        Destroy(newBullet, 1f);
    }

    private void ReloadBullets(){
        currentBullets = maxBullets;
    } 
}
