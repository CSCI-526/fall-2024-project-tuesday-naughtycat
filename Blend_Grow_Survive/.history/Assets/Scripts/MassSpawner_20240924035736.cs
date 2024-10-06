using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSpawner : MonoBehaviour
{

    #region instance 
    public static MassSpawner ins;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
    }
    #endregion

    public GameObject Mass;
    public List<GameObject> Players = new List<GameObject>();
    public List<GameObject> CreatedMasses = new List<GameObject>();
    public int MaxMass = 50;
    public float Time_To_Instantiate = 0.5f;
    public Vector2 pos;
    public GameObject Enemy;
    public int MaxEnemies = 10;
    public List<GameObject> CreatedEnemies = new List<GameObject>();
    public Vector2 enemySizeRange;


    private void Start()
    {
        StartCoroutine(CreateMass(CreatedMasses, MaxMass, Mass));
        StartCoroutine(CreateMass(CreatedEnemies, MaxEnemies, Enemy));
    }

    public IEnumerator CreateMass(List<GameObject> CreatedMasses, int MaxMass, GameObject Mass)
    {
        // wait for seconds
        yield return new WaitForSecondsRealtime(Time_To_Instantiate);

        if (CreatedMasses.Count <= MaxMass)
        {
            //Vector2 Position = new Vector2(Random.Range(-pos.x, pos.x), Random.Range(-pos.y, pos.y));
            Vector2 Position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            Position /= 2;

            GameObject m = Instantiate(Mass, Position, Quaternion.identity);

            AddMass(m, CreatedMasses);

        }

        StartCoroutine(CreateMass(CreatedMasses, MaxMass, Mass));


    }

    public void AddMass(GameObject m, List<GameObject> CreatedMasses)
    {
        if (CreatedMasses.Contains(m) == false)
        {
            CreatedMasses.Add(m);
            for (int i = 0; i < Players.Count; i++)
            {
                PlayerEatMass pp = Players[i].GetComponent<PlayerEatMass>();
                pp.AddMass(m);
            }
        }
    }
    public void RemoveMass(GameObject m, List<GameObject> CreatedMasses)
    {
        if (CreatedMasses.Contains(m) == true)
        {
            CreatedMasses.Remove(m);
            for (int i = 0; i < Players.Count; i++)
            {
                PlayerEatMass pp = Players[i].GetComponent<PlayerEatMass>();
                pp.RemoveMass(m);
            }
        }
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, pos);
    }
}