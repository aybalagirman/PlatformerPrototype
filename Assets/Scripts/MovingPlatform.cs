using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    [SerializeField]
    private Transform _targetA, _targetB;
    private float _speed = 4f;
    private bool _switch = false;
    // Update is called once per frame
    void FixedUpdate() {
        if (!_switch) {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        } else if (_switch) {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);

        }

        if (transform.position == _targetA.position) {
            _switch = false;
        } else if (transform.position == _targetB.position) {
            _switch = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = this.transform;
        }
    } 

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = null;
        }
    }
}
