using UnityEngine;
public class OutlineShader: MonoBehaviour
{
    [SerializeField] private Material _outlineMaterial;
    [SerializeField] private float _outlineScaleFactor;
    [SerializeField] private Color _outlineColor;

    private Renderer _outlineRenderer;
    void Start()
    {
        _outlineRenderer = CreateOutline(_outlineMaterial, _outlineScaleFactor, _outlineColor);
        _outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();
        for(int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i] = outlineMat;
            rend.materials[i].SetColor("_OutlineColor", color);
            rend.materials[i].SetFloat("_OutlineScale", scaleFactor);
        }
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        outlineObject.GetComponent<OutlineShader>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        rend.enabled = false;
        return rend;
    }
}