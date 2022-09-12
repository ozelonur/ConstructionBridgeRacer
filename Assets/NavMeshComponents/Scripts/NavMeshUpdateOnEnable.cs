using UnityEngine;
using UnityEngine.AI;


//Leveli scene e koy
//nav mesh bake yap
//nav mesh datayı UpdateOnEnable componentindeki fielda set et
//leveldeki değişikleri override et
//leveli sceneden sil

public class NavMeshUpdateOnEnable : MonoBehaviour
{
    public NavMeshData m_NavMeshData;

    private NavMeshDataInstance m_NavMeshInstance;

    void OnEnable()
    {
        m_NavMeshData.position = transform.position;
        m_NavMeshData.rotation = transform.rotation;
        m_NavMeshInstance = NavMesh.AddNavMeshData(m_NavMeshData);
    }

    void OnDisable()
    {
        NavMesh.RemoveNavMeshData(m_NavMeshInstance);
    }
}