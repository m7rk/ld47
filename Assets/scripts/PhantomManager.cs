using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomManager : MonoBehaviour
{
    PhantomMoveCache pmc;
    List<Phantom> phantoms;
    int timeStep = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        phantoms = new List<Phantom>();
        pmc = new PhantomMoveCache();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i != pmc.phantomCount(); ++i)
        {
            if (pmc.getMaxMovesForPhantom(i) > timeStep)
            {
                phantoms[i].GetComponent<Phantom>().Input(pmc.getPhantomMove(i, timeStep));
            } else
            {
                phantoms[i].gameObject.SetActive(false);
            }
        }

        timeStep++;
    }

    public void Reset(List<AbstractCharacter.CharacterMove> moves, Vector2 startLoc, Vector2 spawnOffset)
    {
        timeStep = 0;
        pmc.addNewPhantom(moves);
        foreach(var v in phantoms)
        {
            Destroy(v.gameObject);
        }
        phantoms.Clear();

        for(int i = 0; i != pmc.phantomCount(); ++i)
        {
            var p = Instantiate(Resources.Load<GameObject>("Prefab/Phantom"));
            p.transform.parent = this.transform.parent;
            p.transform.position = pmc.getPhantomMove(i,0).location + spawnOffset;
            phantoms.Add(p.GetComponent<Phantom>());
        }
    }
}
