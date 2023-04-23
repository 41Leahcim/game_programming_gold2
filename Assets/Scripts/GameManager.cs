using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    [SerializeField] private Box[] boxPrefabs;
    [SerializeField] private Canvas gameOverMenu;
    [SerializeField] private Score score;
    [SerializeField] private float spawnOffset = 3;
    [SerializeField] private float rotationSpeed;

    private CameraController cameraController;
    private Box currentBox;
    private float movement;
    private float rotation;
    private float spawnHeight = 0;
    private bool gameOver;

    void SpawnBox(){
        // Select a random box
        int prefabIndex = Random.Range(0, boxPrefabs.Length);
        Box prefab = boxPrefabs[prefabIndex];

        // Set the spawn position
        Vector3 spawnPosition = new Vector3(0, spawnHeight + spawnOffset, 0);

        // Create the box
        currentBox = Instantiate(prefab, spawnPosition, prefab.transform.rotation);
        currentBox.manager = this;
    }

    // Start is called before the first frame update
    void Start(){
        // Retrieve the camera controller
        cameraController = Camera.main.GetComponent<CameraController>();

        // Spawn the first box
        SpawnBox();
    }

    // Update is called once per frame
    void Update(){
        // Move and rotate the box dependent on input
        currentBox.Move(movement * Time.deltaTime);
        currentBox.Rotate(rotation * Time.deltaTime * rotationSpeed);

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit(0);
        }
    }

    // Sets the direction the box should move, based on input
    void OnMove(InputValue value) => movement = value.Get<float>();

    // Sets the direction the box should turn, based on input
    void OnRotate(InputValue value) => rotation = value.Get<float>();

    // Drops the box, based on a key press
    void OnDrop(InputValue value) => currentBox.Drop();

    public void BoxLanded(){
        // We don't want to adjust the camera or spawn a new box when the game is over
        if(gameOver){
            return;
        }

        // Add the value of the box to the score
        score.AddPoints(currentBox.value);

        // Adjust the height of the camera, if the box stopped too high
        if(currentBox.transform.position.y >= spawnHeight){
            spawnHeight = currentBox.transform.position.y;
            cameraController.AdjustCameraHeight(currentBox.transform.position.y);
        }

        // Spawn the next box
        SpawnBox();
    }

    public void GameOver(){
        // The game is over, disable gameplay and show the game over menu
        gameOver = true;
        gameOverMenu.enabled = true;
    }

    public void TryAgain(){
        // Reload the current scene to allow the user to try again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
