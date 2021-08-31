using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 targetPosition;

    float movementVertical;
    float movementHorizontal;
    public float dashBar = 50;
    public float dashBarFull = 50;
    bool isfullyEmptyBar = false;
    bool isEmptyBar = false;
    float timer = 0;
    public float moveSpeed =5f;

    public GameObject macheneGunRight;
    public GameObject macheneGunLeft;
    public GameObject canon;
    public GameObject misilLaucher;

    public GameObject bulletMachineGun;
    public GameObject bulletCanon;
    public GameObject bulletMisil;

    bool isFirtShoot = true; // intercalar disparos
    bool isShootingBullet = false;
    bool isShootingCanon = false; //control: bloquea movimiento y otros disparos mientras lo realiza
    public int stockBulletMachinGun = 200;
    public int stockBulletMisil = 50;
    public int stockBulletCanon = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShootingCanon) { // limito todas las acciones durante el enfriamiento del cañom

            //obtengo ingreso de teclado

            //DASH
            if (dashBar >= 0 && !isfullyEmptyBar) {
                if (Input.GetKey(KeyCode.LeftShift)) {
                    isEmptyBar = false;
                    dashBar -= 0.5f;
                    moveSpeed = 15f;
                }
                if (Input.GetKeyUp(KeyCode.LeftShift)) {
                    moveSpeed = 5f;
                    isEmptyBar = true;
                }
            }
            else {
                isfullyEmptyBar = true;
                moveSpeed = 5f;
            }

            //carga cuando el tanque no esta vacio
            if (isEmptyBar) {
                timer += 1 * Time.deltaTime;
                if (timer >= 1) {
                    timer = 0;
                    dashBar+=5;
                }
                if (dashBar >= dashBarFull) {
                    dashBar = dashBarFull;
                    isEmptyBar = false;
                }
            }

            //carga cuando el tanque esta vacio
            if (isfullyEmptyBar) {
                timer += 1 * Time.deltaTime;
                if(timer >= 1) {
                    timer = 0;
                    dashBar++;
                }
                if (dashBar >= dashBarFull) {
                    dashBar = dashBarFull;
                    isfullyEmptyBar = false;
                }
            }

            //adelante y atras
            movementVertical = Input.GetAxis("Vertical");
            transform.position += transform.up * movementVertical * moveSpeed * Time.deltaTime;

            //derecha e ixquierda
            movementHorizontal = Input.GetAxis("Horizontal");
            if (movementHorizontal > 0) {
                transform.position += transform.right * movementHorizontal * moveSpeed * Time.deltaTime;
            }
            if (movementHorizontal < 0) {
                transform.position -= -transform.right * movementHorizontal * moveSpeed * Time.deltaTime;
            }


            //posicion del mouse en relacion a la pantalla
            targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            //coloco la camara sobre el player
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);


            //control de disparos
            if (Input.GetButton("Fire1") && !isShootingBullet) {
                if (stockBulletMachinGun > 0) {
                    isShootingBullet = true;
                    if (isFirtShoot) {
                        shootMachineGun(bulletMachineGun, macheneGunRight, false);
                    }
                    else {
                        shootMachineGun(bulletMachineGun, macheneGunLeft, true);
                    }
                    stockBulletMachinGun--;
                    Invoke("changeStateBullet", 0.25f);
                }
                else {
                    print("Sin Balas de Arma Principal");
                }
            }

            if (Input.GetButton("Fire3")) {
                if(stockBulletCanon > 0) { 
                    Instantiate(bulletCanon, canon.transform.position, canon.transform.rotation);
                    isShootingCanon = true;
                    stockBulletCanon--;
                    Invoke("changeStateCanon", 2f);
                }
                else {
                    print("Sin Balas de Cañon");
                }
            }

            if (Input.GetButton("Fire2")) {
                if(stockBulletMisil > 0) {

                }
                else {
                    print("Sin Balas de Arma Secundaria");
                }
            }
        }
    }

    void shootMachineGun(GameObject bullet, GameObject point, bool stateShoot) {
        Instantiate(bullet, point.transform.position, point.transform.rotation);
        isFirtShoot = stateShoot;
    }

    void changeStateCanon() {
        isShootingCanon = false;
    }
    void changeStateBullet() {
        isShootingBullet = false;
    }

    //ya que utilizo fisica utilizo el fixedupdate
    private void FixedUpdate() {
        
        //rotacion hacia mouse
        Vector2 LookAt = targetPosition - rb.position;
        float angle = Mathf.Atan2(LookAt.y,LookAt.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
