using WaveSystem.Waves;
using Vector2 = UnityEngine.Vector2;

public class ExampleWave : CustomWave
{
   //example of filling a custom wave through script 
   private void Awake()
   {
      _playAreaIndex = 0;
      _enemyAndSpawnTimer = new[]
      {
         new Vector2(1,5f),
         new Vector2(1,4f)
      };
   }
}
