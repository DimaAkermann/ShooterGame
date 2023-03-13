using UnityEngine;

public class TouchRecognizer : MonoBehaviour
{
   
   [SerializeField] private float _sensivity;
   [SerializeField] private float _moveInputDeadZone;
   [SerializeField] private Transform _cameraTransform;

   private int _rightFingerId;
   private float _halfScreenWidth;
   private Vector2 _lookinput;
   private float _cameraPitch;

   private void Start()
  {
      _rightFingerId = - 1;
      _halfScreenWidth = Screen.width / 2;
      _moveInputDeadZone = Mathf.Pow(Screen.height / _moveInputDeadZone, 2);
  }
  
  private void Update()
  {
      GetTouchInput();
      if (_rightFingerId != -1)
      {
          CamerRotation();
      }
  }
  
  private void GetTouchInput()
  {
      for (int i = 0; i < Input.touchCount; i++)
      {
          Touch t = Input.GetTouch(i);

          switch (t.phase)
          {
              case TouchPhase.Began:
                  if (t.position.x > _halfScreenWidth && _rightFingerId == -1 )
                  {
                      _rightFingerId = t.fingerId;
                  }
                  break;
              case TouchPhase.Ended :
              case TouchPhase.Canceled:
                  if (t.fingerId == _rightFingerId)
                  {
                      _rightFingerId = -1;
                  }
                  break;
              case TouchPhase.Moved:
                  if (t.fingerId == _rightFingerId)
                  {
                      _lookinput = t.deltaPosition * (_sensivity * Time.deltaTime);
                  }
                  break;
              case TouchPhase.Stationary:
                  if (t.fingerId == _rightFingerId)
                  {
                      _lookinput = Vector2.zero;
                  }
                  break;
          }
      }
  }
  
   public void CamerRotation()
  {
      _cameraPitch = Mathf.Clamp(_cameraPitch - _lookinput.y, -90f, 90f);
      _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch,0,0);
        
      transform.Rotate(transform.up,_lookinput.x);
  }
}
