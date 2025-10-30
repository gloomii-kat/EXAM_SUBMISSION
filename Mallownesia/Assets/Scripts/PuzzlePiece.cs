using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public int pieceID; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PuzzleManager.Instance.CollectPiece(pieceID);
            Destroy(gameObject); 
            
        }
    }
}
