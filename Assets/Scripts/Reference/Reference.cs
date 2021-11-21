using UnityEngine;

public class Reference : MonoBehaviour
{
    public Transform FolderActiveElements => _folderActiveElements;
    [SerializeField] private Transform _folderActiveElements;

    public Transform Canvas => _canvas;
    [SerializeField] private Transform _canvas;

    public Transform FolderInfo => _folderInfo;
    [SerializeField] private Transform _folderInfo;

    public Camera MainCamera => _mainCamera;
    private Camera _mainCamera;

    public static Reference inst;
    private void Awake()
    {
        if (inst != null) Destroy(this);
        inst = this;
        DontDestroyOnLoad(this.gameObject);
        _mainCamera = Camera.main;
    }

}
