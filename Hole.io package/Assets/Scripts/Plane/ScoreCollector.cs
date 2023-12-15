
using Holeio.Interact;
using UnityEngine;

namespace Holeio.score
{
    public class ScoreCollector : MonoBehaviour
    {
        [SerializeField]
        private int currentScore;
        public void AddScore(int score)
        {
            currentScore += score;
            PlayerService.Instance.CheckForUpgrade(currentScore);
            UIService.Instance.UpdateScore(currentScore);
        }

        public int GetScore()
        {
            return currentScore;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Interactable>() != null)
            {
                
                int score = collision.gameObject.GetComponent<Interactable>().GetScore();
                AddScore(score);
                collision.gameObject.SetActive(false);
                Debug.Log("gameobject destroyed");
            }
        }
    }
}

