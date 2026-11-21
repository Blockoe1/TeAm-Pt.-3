using MarUtility.ExecutionManagement;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaster : Manager
{
    private static ParticleMaster inst;

    [SerializeField]
    private Dictionary<string, GameObject> _library;

    #region GS
    public static ParticleMaster INST { get => inst; }
    #endregion


    public override void Initialize()
    {
        //Set instance.
        if (inst == null)
            inst = this;
        else
        {
            Debug.LogError("Multiple copies of ColorMaster exists, please delete one.");
            Destroy(this);
        }

        base.Initialize();
    }
    
    //Plays a particle under the parent.
    public GameObject Play(string ID, Transform parent)
    {
        if (!ExistsInLibrary(ID)) return null;

        GameObject particle = Instantiate(_library[ID], parent);
        return particle;
    }

    //Plays a particle at the position.
    public GameObject Play(string ID, Vector2 position)
        => Play(ID, new Vector3(position.x, position.y, 0));

    public GameObject Play(string ID, Vector3 position)
    {
        if (!ExistsInLibrary(ID)) return null;

        GameObject particle = Instantiate(_library[ID], position, Quaternion.identity);
        return particle;
    }

    //Returns false if a particle does not exist in the library.
    private bool ExistsInLibrary(string ID)
    {
        if (!_library.ContainsKey(ID))
        {
            Debug.LogError($"Particle {ID} does not exist in the ParticleMaster library.");
            return false;
        }

        if (_library[ID] == null)
        {
            Debug.LogError($"Particle {ID} is null.");
            return false;
        }

        return true;
    }
}
