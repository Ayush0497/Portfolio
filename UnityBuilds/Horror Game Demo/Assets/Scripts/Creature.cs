    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
using UnityEngine.SceneManagement;

public class Creature : MonoBehaviour
    {
        [SerializeField] GameObject GameObject;
        [SerializeField] private Animator animator;
        [SerializeField] Light Light1;
        [SerializeField] AudioSource scream;

        private void Start()
        {
            GameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            animator.enabled = false;
        }

        private void spawnAndMove()
        {
            GameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            GameObject.SetActive(true);
            animator.enabled = true;
            scream.Play();
            StartCoroutine("EndGame");
        }

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(1.5f);
            Light1.color = Color.black;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(2);
        }
}
