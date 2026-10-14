using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip bombClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private AudioSource source;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
    }

    private void OnEnable()
    {
        FallingItem.OnItemCaught += HandleItemCaught;
        GameStateManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        FallingItem.OnItemCaught -= HandleItemCaught;
        GameStateManager.OnGameOver -= HandleGameOver;
    }

    private void HandleItemCaught(ItemData data)
    {
        Play(data.isBomb ? bombClip : coinClip);
    }

    private void HandleGameOver()
    {
        Play(loseClip);
    }

    private void Play(AudioClip clip)
    {
        if (clip != null)
            source.PlayOneShot(clip, volume);
    }
}