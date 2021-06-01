using UnityEngine;

public class SimpleExitGame : MonoBehaviour
{
   private const float HoldTime = 1f;
   private float _timer = HoldTime;
   private const KeyCode ButtonToQuit = KeyCode.Escape;
   private void Update()
   {
     CheckEscapeToQuit();
   }

   private void CheckEscapeToQuit()
   {
      if (Input.GetKey(ButtonToQuit))
      {
         _timer -= Time.deltaTime;
      }
      if (Input.GetKeyUp(ButtonToQuit))
      {
         _timer = HoldTime;
      }

      if (_timer < 0)
      {
         Application.Quit();
      }
   }
}
