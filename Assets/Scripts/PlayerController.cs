using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake() {
        instance = this;
    }
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;
    public Camera viewCam;
    private float verticalRot;
    public GameObject bulletImpact;
    public int currentAmmo;
    public Animator gunAnim, playerAnim;
    public int currentHealth;
    public int maxHealth = 100;
    public GameObject deadScreen;
    private bool hasDied;
    public Text healthText, ammoText;
    public GameObject gameManager;
    public GameObject escapeScreen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        deadScreen.SetActive(false);
        healthText.text = currentHealth.ToString() + "%";
        ammoText.text = currentAmmo.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.SetActive(false);
            escapeScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(!hasDied)
        {
            // keyboard movement
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 moveHorizontal = transform.up * -moveInput.x;
            Vector3 moveVertical = transform.right * moveInput.y;

            rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

            // mouse control
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 
                                    transform.rotation.eulerAngles.z - mouseInput.x );

            //verticalRot += mouseInput.y;
            //verticalRot = Mathf.Clamp(verticalRot, -60f, 60f);
            //viewCam.localRotation = Quaternion.Euler(viewCam.rotation.eulerAngles.x, verticalRot, viewCam.rotation.eulerAngles.z);

            viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));
            
            
            // shooting
            if(Input.GetMouseButtonDown(0))
            {
                if(currentAmmo > 0)
                {
                    Ray ray = viewCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("Hit at " + hit.transform.name);
                        Instantiate(bulletImpact, hit.point, transform.rotation);

                        if(hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                        }

                        AudioController.instance.PlayGunShot();
                    }

                    gunAnim.SetTrigger("Shoot");
                    currentAmmo--;
                    UpdateAmmoUI();
                }
            }

            if(moveInput != Vector2.zero)
            {
                playerAnim.SetBool("isMoving", true);
            }
            else{
                playerAnim.SetBool("isMoving", false);
            }

        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            deadScreen.SetActive(true);
            hasDied = true;
            currentHealth = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        healthText.text = currentHealth.ToString() + "%";

        AudioController.instance.PlayPlayerHurt();
    }
    public void AddHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthText.text = currentHealth.ToString() + "%";
    }
    public void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo.ToString();
    }
    public void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString();
    }

}
