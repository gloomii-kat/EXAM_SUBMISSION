using UnityEngine;

public class PuzzleAssemblyPoint : MonoBehaviour
{
    public GameObject completedPuzzlePrefab;
    public DoorController door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PuzzleManager.Instance.HasAllPieces())
            {
                AssemblePuzzle();
            }
            else
            {
                Debug.Log("You don’t have all the pieces yet!");
            }
        }
    }

    private void AssemblePuzzle()
    {
        Instantiate(completedPuzzlePrefab, transform.position, transform.rotation);
        Debug.Log("Puzzle Completed!");
        /*
        Timer timer = Object.FindFirstObjectByType<Timer>();
        if (timer != null && timer.CurrentTime > 0)  // We'll make CurrentTime public
        {
            SceneLoader sceneLoader = Object.FindFirstObjectByType<SceneLoader>();
            if (sceneLoader != null)
            {
                sceneLoader.LoadYouWinScene();
            }
            else
            {
                Debug.LogWarning("SceneLoader not found in scene.");
            }
        }
        */
    }
}
