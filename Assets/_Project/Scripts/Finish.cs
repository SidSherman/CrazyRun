using System.Collections;
using UnityEngine;


public class Finish : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _nextIndex;
    [SerializeField] private GameObject _finishVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.Finish();
            Instantiate(_finishVFX, other.gameObject.transform.position, other.gameObject.transform.rotation);
            StartCoroutine(WaitToNewLevel(3));
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.DeactivateMovement();
            }
        }
    }

    private IEnumerator WaitToNewLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _sceneLoader.LoadSceneByIndex(_nextIndex);
    }

}
