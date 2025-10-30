using UnityEngine;

public class TestGlow : MonoBehaviour
{
    private Material mat;
    public Color glowColor = Color.yellow * 10f;
    private Color baseColor = Color.black;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", baseColor);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mat.SetColor("_EmissionColor", glowColor);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mat.SetColor("_EmissionColor", baseColor);
        }
    }
}
