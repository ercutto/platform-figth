using UnityEngine;

public class ObjectBool : MonoBehaviour
{
    public bool isTaken;
    public bool onUse;
    public void Awake()
    {
        isTaken = false;
        onUse = false;
    }
    private void Update()
    {
        
    }

}
