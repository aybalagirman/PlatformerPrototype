using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
    [SerializeField]
    private GameObject _respawnPosition;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();

            if (player) {
                CharacterController cc = other.GetComponent<CharacterController>();

                if (cc) {
                    cc.enabled = false;
                }
                
                other.transform.position = _respawnPosition.transform.position;
                StartCoroutine(CCEnableRoutine(cc));
                player.Damage();
            }

        }
    }

    IEnumerator CCEnableRoutine(CharacterController controller) {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
