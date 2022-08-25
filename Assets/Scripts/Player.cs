using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15f;
    [SerializeField]
    private GameObject _respawnPosition;
    private float _yVelocity = 0;
    private bool _doubleJump = false;
    private int _coins = 0;
    private UIManager _uiManager;
    private int _lives = 3;
    private CharacterController _controller;

    void Start() {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (!_uiManager) {
            Debug.LogError("The UI Manager is NULL.\n");
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    void Update() {
        MovePlayer();
        CheckDeathPoint();
    }

    void MovePlayer() {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = _speed * direction;

        if (_controller.isGrounded) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                _yVelocity = _jumpHeight;
                _doubleJump = true;
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Space) && _doubleJump) {
                _yVelocity += _jumpHeight;
                _doubleJump = false;
            }

            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void CheckDeathPoint() {
        Vector3 _checkDeathHeight = this.transform.position;

        if (_checkDeathHeight.y <= - 3.52) {
            this.transform.position = _respawnPosition.transform.position;
            Damage();
        }
        
    }

    public void AddCoins() {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage() {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1) {
            SceneManager.LoadScene(0);
        }
    }
}
