using UnityEngine;

public class PlatformerCamera2D : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _cameraSmooth;

    private float _lastXposition = -11111100000000;
       
    void Update()
    {
        var position = transform.position;
        position.x = Mathf.Lerp(transform.position.x, _target.position.x, _cameraSmooth);
        position.x = Mathf.Max(position.x, _lastXposition);
        transform.position = position - Vector3.forward;

        if (position.x > _lastXposition)
            _lastXposition = position.x;
        //transform.position = Vector3.Lerp(transform.position, _target.position, _cameraSmooth) - Vector3.forward;
    }
}
