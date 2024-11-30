using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    Rigidbody rb;
    public int ballspeed = 0;
    public int jumpspeed = 0;
    private bool istouching = false;

    public TMPro.TextMeshProUGUI textUI;
    public TMPro.TextMeshProUGUI textUITimer;
    private int currentScore = 0;

    public GameObject coinPrefab;
    public int sizeOfCoins = 10;
    public GameObject[] coin;

    public float currentTime = 0f;
    public float maxTime = 60f;

    public AudioClip Coin;
    private AudioSource audioSource;
    
    void Start()
    {
        currentTime = maxTime;
        rb = GetComponent<Rigidbody>();
        coin = new GameObject[sizeOfCoins];
        for (int i = 0; i < sizeOfCoins; i++) {
        coin[i] = Instantiate(coinPrefab, new Vector3(Random.Range(-3f,3f), Random.Range(5f,5f), 0f),coinPrefab.transform.rotation);
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = "Score: " + currentScore;
        textUITimer.text = "Time: " + currentTime;

        currentTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(new Vector3(-1*ballspeed, 0f, 0f),ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(new Vector3(1f*ballspeed, 0f, 0f),ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(new Vector3(0f, 0f, -1*ballspeed),ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(new Vector3(0f, 0f, 1*ballspeed),ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space) && istouching) {
            rb.AddForce(new Vector3(0f, 1*jumpspeed, 0f),ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
        {

        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag=="Floor") {
                istouching = false;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag=="Floor") {
                istouching = true;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Collectable")) {
                CollectCoin(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            
        }
        private void OnTriggerStay(Collider other)
        {
            
        }
        private void CollectCoin(GameObject coin)
        {
            currentScore++;
            //Destroy(coin);
            coin.SetActive(false);
            StartCoroutine(RespawCoin(coin));
            audioSource.PlayOneShot(Coin);
        }

        private IEnumerator RespawCoin(GameObject coin) {
            yield return new WaitForSeconds(3f);

        //Debug.Log("Respawn");
        //Instantiate(coinPrefab, new Vector3(Random.Range(-5f,5), Random.Range(1f,5f), Random.Range(-5f,5)),coinPrefab.transform.rotation);
        coin.transform.position = new Vector3(Random.Range(-3f,3f), Random.Range(5f,5f), 0f);
        coin.SetActive(true);
        }
}
