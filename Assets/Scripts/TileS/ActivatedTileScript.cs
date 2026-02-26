using UnityEngine;

public class ActivatedTileScript : MonoBehaviour
{
   private NewTileScript currentTile;
   private NewTileScript nextTile;
   private BeatClock _beatClock;
   private Vector2Int direction;
   public bool inUsed;

   

   public void Initialise(NewTileScript firstTile,Vector2Int dir,BeatClock beatClock)
   {
      _beatClock = beatClock;
      _beatClock.onBeat.AddListener(ReceiveBeat);
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
         _beatClock.onBeat.RemoveListener(ReceiveBeat);
         currentTile = null;
         nextTile = null;
         inUsed = false;
         return;
         
      }
      CurrentTile = NextTile;
   }
}
