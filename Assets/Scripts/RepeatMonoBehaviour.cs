using UnityEngine;

public class RepeatMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }
    protected virtual void Awake()
    {
        this.LoadComponents();
        this.ResetValue();
    }
    
    protected virtual void ResetValue()
    {
        // For override
    }

    protected virtual void LoadComponents()
    {
        // For override
    }
}
