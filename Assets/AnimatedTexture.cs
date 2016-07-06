using UnityEngine;
using System.Collections;
using System.Security.Policy;

[RequireComponent(typeof(Renderer))]
public class AnimatedTexture : MonoBehaviour
{
    public int CellsHorizonal, CellsVertical;
    public float uX, uY;
    private int lastX, lastY;
    private Renderer rend;

    void Start()
    {
        rend = this.GetComponent<Renderer>();
        rend.material.SetTextureScale("_MainTex", new Vector2(1.0f / CellsHorizonal, 1.0f / CellsVertical));
        lastX = -1;
        lastY = -1;
    }

    void FixedUpdate ()
    {
        int X = (int) uX;
        int Y = (int) uY;
        if (lastY != Y || lastX != X)
        {
            var height = 1.0f / CellsVertical;
            var offset = new Vector2(1.0f / CellsHorizonal * X, 1.0f - height - height * Y);
            rend.material.SetTextureOffset("_MainTex", offset);
            lastY = Y;
            lastX = X;
        }
    }
}
