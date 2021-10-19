using UnityEngine;

public class NoteCanvas : MonoBehaviour
{
    public GameObject linePrefab, zeroLine;
    private float _zeroLineWidth;
    
    
    void Awake()
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
        _zeroLineWidth = zeroLine.transform.localScale.x;
        return _zeroLineWidth;
    }
    
    public GameObject GenLine(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject line = Instantiate(linePrefab, transform.Find("Lines"));
        line.transform.position = position;
        line.transform.rotation = rotation;
        line.transform.localScale = localScale;
        return line;
    }

    public void AddNoteObject(GameObject noteObject)
    {
        noteObject.transform.parent = transform.Find("Notes");
    }

    public void ClearNoteObjects()
    {
        foreach (Transform child in GameObject.Find("Notes").transform)
        {
            Destroy(child.gameObject);
        }
    }
}
