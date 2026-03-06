using UnityEngine;

public class ActivatedTileScript : MonoBehaviour
{
   private NewTileScript currentTile;
   private NewTileScript nextTile;
   private OldBeatClock _oldBeatClock;
   public Vector2Int direction;
   public ActivatedTileManager activatedTileManager;
   public bool inUsed;

   

   public void Initialise(NewTileScript firstTile,Vector2Int dir,OldBeatClock oldBeatClock)
   {
      _oldBeatClock = oldBeatClock;
      _oldBeatClock.onBeat.AddListener(ReceiveBeat);
      activatedTileManager.RemoveInUsedFromList(this);
      direction = dir;
      CurrentTile = firstTile;
      inUsed = true;

   }

   public NewTileScript CurrentTile
   {
      get => currentTile;
      set
      {
         value.thisPatterneList.Add(this);
         currentTile?.thisPatterneList.Remove(this);
         currentTile = value;
         currentTile.CheckForPattern();
         NextTile = currentTile.NextTileScript(direction);
      } 
   }

   public NewTileScript NextTile
   {
      get => nextTile;
      set
      {
         value?.thisPatternSignList.Add(this);
         nextTile?.thisPatternSignList.Remove(this);

         nextTile = value;
         nextTile?.CheckForPattern();
      }
   }
   private void ReceiveBeat()
   {
      Move();
   }
   
   public void Move()
   {
      
      if (NextTile is null)
      {
         _oldBeatClock.onBeat.RemoveListener(ReceiveBeat);
         currentTile = null;
         nextTile = null;
         inUsed = false;
         activatedTileManager.AddNotInUsedFromList(this);
         return;
      }
      CurrentTile = NextTile;
      transform.position = currentTile.transform.position + Vector3.up;
   }
}
