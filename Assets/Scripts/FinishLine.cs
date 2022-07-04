using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponents<Collider>().ToList().ForEach(x => x.enabled = !x.isTrigger);
            GameController.Instance.LevelCompleted();

            enabled = false;
        }
    }
}
