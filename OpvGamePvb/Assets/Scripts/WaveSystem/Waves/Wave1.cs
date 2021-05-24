using WaveSystem.Waves;
using Vector2 = UnityEngine.Vector2;

public class Wave1 : CustomWave
{
   //example of making customclass file
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
