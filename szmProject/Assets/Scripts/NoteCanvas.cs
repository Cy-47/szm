using UnityEngine;

public class NoteCanvas : MonoBehaviour
{
    public GameObject linePrefab, zeroLine;
    private float _zeroLineWidth;

    void Start()
    {
        _zeroLineWidth = zeroLine.transform.localScale.x;
    }

    public void SetZeroLineWidth(float width)
    {
        _zeroLineWidth = width;
        Vector3 localScale = zeroLine.transform.localScale;
        localScale.x = width;
        zeroLine.transform.localScale = localScale;
    }
    
    public float GetZeroLineWidth()
    {
        return _zeroLineWidth;
    }
    
    public GameObject GenLine(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        //GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject line = Instantiate(linePrefab, transform);
        line.transform.position = position;
        line.transform.rotation = rotation;
        line.transform.localScale = localScale;
        return line;
    }
}
