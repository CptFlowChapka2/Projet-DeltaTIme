using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public enum HexState
{
    None,
    Idle,
    Transforming
}

[Serializable]
public struct HexRule : IEquatable<HexRule>
{
    public IngredientType[] inputs;
    public GameObject replacementHex;
    public int nbOfTicksToReplace;
    
    public bool Equals(HexRule other)
    {
        return Equals(inputs, other.inputs) && Equals(replacementHex, other.replacementHex) && nbOfTicksToReplace == other.nbOfTicksToReplace;
    }

    public override bool Equals(object obj)
    {
        return obj is HexRule other && Equals(other);   
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(inputs, replacementHex, nbOfTicksToReplace);
    }
}

public abstract class Hex : MonoBehaviour
{
    public string hexName;
    public Sprite hexUIInfo;
    public HexRule[] hexRules;
    public Vector2Int relativeCoords;
    public List<Grabbable> grabbablesOnThisHex = new List<Grabbable>();
    //public Vector3 boundsCenter;

    public Grabber[] grabbers;
    public bool isHovered = false;
    public HexState state = HexState.Idle;
    public int maxNbrOfGrabbable = 3;
    [SerializeField] private GameObject hoveringOutline;
    [SerializeField] private MeshRenderer meshRenderer; 
    [SerializeField] private GameObject outlineDanger; 
    [SerializeField] private GameObject feedbackTimerDanger; 
    private Material popupBaseMaterial;
    public GridManager gridManager;
    public LvlInfos lvlInfos;
    
    //Variables liées aux HexRules
    protected HexRule currentRule;
    protected HexRule lastTickRule;
    protected int tickCounter;
    
    protected float timerDanger = 0;
    protected bool outlineIsVisible;

    protected virtual void Start()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        lvlInfos = gridManager.LvlInfos;
        lvlInfos.Tick.AddListener(Tick);
        grabbers = gridManager.grabbers;
        popupBaseMaterial = gridManager.popupBaseMat;

        InitializeRelativeCoords();
    }

    private static Vector2Int[] odd = new Vector2Int[6]
    {
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(1, -1),
        new Vector2Int(0, 1),
        new Vector2Int(1, 1)
    };
    private static Vector2Int[] even = new Vector2Int[6]
    {
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 1)
    };

    private  Vector2Int[] offsetNeighbhor;
    private void InitializeRelativeCoords()
    {
        int relativeX = 0;
        int relativeY = 0;

        float stepsX = transform.position.x;
        float stepsY = transform.position.z;

        for (int i = 0; i < 25; i++)
        {
            if ((stepsX > -0.1f && stepsX < 0.1f) || (stepsX >= 0.866024f - 0.1f && stepsX < 0.866024f + 0.1f)) //approximation car Mathf.Sqrt est pas précise
            {
                relativeX = i;
            }
            
            if (stepsY == 0)
            {
                relativeY = i;
            }
            
            stepsX -= 0.866024f * 2f;
            stepsY -= 1.5f;
        }
        
        relativeCoords.x = relativeX;
        relativeCoords.y = relativeY;

        gridManager.hexes[relativeCoords.x, relativeCoords.y]=this;
        offsetNeighbhor = (relativeCoords.y % 2 == 0) switch
        {
            true => even,
            false => odd
        };
    }

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        if (isHovered)
        {
            hoveringOutline.SetActive(true);
        }
        else
        {
            hoveringOutline.SetActive(false);
        }

        VerifyState();
        if (grabbablesOnThisHex.Count <= 0) return;
        for (int i = 0; i < grabbablesOnThisHex.Count; i++)
        {
            grabbablesOnThisHex[i].gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (0.3f + 0.3f * i), transform.position.z);
        }
        
    }

    public virtual void Tick()
    {
        if (grabbablesOnThisHex.Count == 0)
        {
            state = HexState.Idle;
            return;
        }
        if (hexRules.Length != 0)
        {
            VerifyHexRule();
            UpdateFeedbackTimer();
        }
        else
        {
            state = HexState.Idle;
            tickCounter = 0;
            lastTickRule = new HexRule();
        }
        grabbablesOnThisHex?.First()?.Tick();
    }

    public void VerifyHexRule()
    {
        tickCounter++;
        currentRule = new HexRule();
        
        List<IngredientType> ingredientsTypes = new List<IngredientType>();

        foreach (Grabbable grabbable in grabbablesOnThisHex)
        {
            if (grabbable is Ingredient)
            {
                ingredientsTypes.Add(((Ingredient)grabbable).type);
            }
        }
        
        currentRule = new HexRule();

        foreach (HexRule rule in hexRules)
        {
            var intersection =ingredientsTypes.Where(x => rule.inputs.Contains(x)).ToList();
            //Ca doit être exactement la même, mais pas forcément dans le même ordre
            var intersectionIngredientsTypes = intersection.ToList();
            
            if (intersectionIngredientsTypes.Count() == rule.inputs.Length//verifie que l'on as suffisament pour que la régle fonctionne
                && intersectionIngredientsTypes.Count() == ingredientsTypes.Count //verifie que on as bien que les ingredient qu'on as
                )
            {
                currentRule = rule;
                state = HexState.Transforming;
                break;
            }
        }

        if (currentRule.Equals(new HexRule()) || !currentRule.Equals(lastTickRule))
        {
            state = HexState.Idle;
            tickCounter = 0;
            lastTickRule = currentRule;
            return;
        }

        if (tickCounter >= currentRule.nbOfTicksToReplace)
        {
            foreach (Grabbable grabbable in grabbablesOnThisHex)
            {
                Destroy(grabbable.gameObject);
            }
            Instantiate(currentRule.replacementHex, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    private void VerifyState()
    {
        switch (state)
        {
            case HexState.Idle:
                outlineIsVisible = false;
                outlineDanger.SetActive(false);
                break;
            case HexState.Transforming:
                if (!outlineIsVisible)
                {
                    timerDanger += Time.deltaTime;
                    if (timerDanger >= 1f)
                    {
                        outlineIsVisible = true;
                        outlineDanger.SetActive(true);
                    }
                }
                else
                {
                    timerDanger -= Time.deltaTime;
                    if (timerDanger <= 0)
                    {
                        outlineIsVisible = false;
                        outlineDanger.SetActive(false);
                    }
                }
                break;
            default:
                break;
        }
    }

    protected void UpdateFeedbackTimer()
    {
        if (!currentRule.Equals(new HexRule()))
        {
            feedbackTimerDanger.SetActive(true);
            float currentScale = (float)tickCounter / (float)currentRule.nbOfTicksToReplace;
            feedbackTimerDanger.transform.localScale = new Vector3(currentScale, 1f, currentScale);
        }
        else
        {
            feedbackTimerDanger.SetActive(false);
        }
    }

    public void ChangeMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }

    public virtual void AddGrabbable(Grabbable grabbable)
    {
        int totalGrabbables = OnlyIngredientsNumber();
        if (totalGrabbables == maxNbrOfGrabbable)
        {
            Overflow(grabbable);
            return;
        }
        grabbablesOnThisHex.Add(grabbable);
        grabbablesOnThisHex.TrimExcess();
        grabbable.actualHex = this;
       
    }

    public int OnlyIngredientsNumber()
    {
        int totalgrabables = grabbablesOnThisHex.Count;
        if (grabbablesOnThisHex.Count>0&&grabbablesOnThisHex.First() is Machine or Player) totalgrabables -= 1;
        return totalgrabables;
    }

    public GameObject trashHex;
    public void Overflow(Grabbable grabbable)
    {
        //initalise option
        List<int> availableNeighbor = (new int[6] { 0, 1, 2, 3, 4, 5 }).ToList();
        //while to find the good one//
        while (availableNeighbor.Count>0)
        {
            int random = Random.Range(0, availableNeighbor.Count - 1);
            Vector2Int overflowTo = relativeCoords + offsetNeighbhor[random];
            bool insideX = overflowTo.x >= gridManager.hexes.GetLowerBound(0) &&
                            overflowTo.x <= gridManager.hexes.GetUpperBound(0);
            bool insideY = overflowTo.y >= gridManager.hexes.GetLowerBound(1) &&
                            overflowTo.y <= gridManager.hexes.GetUpperBound(1);
            
            if (insideY && insideX && gridManager.hexes[overflowTo.x, overflowTo.y] is not null)
            {
                Hex targetHex= gridManager.hexes[overflowTo.x, overflowTo.y];
                if (targetHex.OnlyIngredientsNumber() < targetHex.maxNbrOfGrabbable)
                {
                    targetHex.AddGrabbable(grabbable);
                    return;
                }
                
            }
            
            availableNeighbor.RemoveAt(random);
            availableNeighbor.TrimExcess();
        }
        //if no good one//
        Instantiate(trashHex);
        for (int i = 0; i < grabbablesOnThisHex.Count; i++)
        {
            Destroy(grabbablesOnThisHex[i].gameObject);
        }
        Destroy(gameObject);


    }

    public virtual void RemoveGrabbable(Grabbable grabbable)
    {
        grabbablesOnThisHex.Remove(grabbable);
        grabbablesOnThisHex.TrimExcess();
        grabbable.actualHex = null;
        
    }

    public virtual void DestroyGrabbables(Grabbable grabbable)
    {
        grabbablesOnThisHex.Remove(grabbable);
        grabbablesOnThisHex.TrimExcess();
        
        if(grabbable ==null)return;
        Destroy(grabbable.gameObject);
        
    }

    public List<Grabbable> AllNextGrabbables(Grabbable grabbable)
    {
        List<Grabbable> allNextGrabbables = grabbablesOnThisHex.ToList(); 
        allNextGrabbables.RemoveRange(0, grabbablesOnThisHex.IndexOf(grabbable) + 1);
        return allNextGrabbables;
    }

   
}

