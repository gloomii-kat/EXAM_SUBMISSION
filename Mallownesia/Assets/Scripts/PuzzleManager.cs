using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public int totalPieces = 5;
    private bool[] collectedPieces;
    private int collectedCount = 0;

    public Text puzzleCounterText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collectedPieces = new bool[totalPieces];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdatePuzzleUI(); // Initialize the UI
    }

    public void CollectPiece(int id)
    {
        if (id >= 0 && id < totalPieces && !collectedPieces[id])
        {
            collectedPieces[id] = true;
            collectedCount++;
            Debug.Log($"Collected piece {id}. Total: {collectedCount}/{totalPieces}");

            UpdatePuzzleUI(); // Update UI when a piece is collected
        }
    }

    public bool HasAllPieces()
    {
        return collectedCount == totalPieces;
    }

    private void UpdatePuzzleUI()
    {
        if (puzzleCounterText != null)
        {
            puzzleCounterText.text = $"Puzzles: {collectedCount}/{totalPieces}";
        }
    }

}
