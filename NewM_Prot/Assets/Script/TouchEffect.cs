using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem prefab;
    private ParticleSystem effect;

    void Start()
    {
        //  Prefabを生成してParticleコンポーネントを保持
        effect = GameObject.Instantiate(prefab).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //  クリック、あるいはタップした瞬間
        if (Input.GetMouseButtonDown(0))// || IsTap())
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward);

            //  移動させてからParticleを発射
            //  (毎回Instantiateするより負荷が軽い)
            effect.transform.position = pos;
            effect.Emit(1);
        }
    }

    bool IsTap()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).phase == TouchPhase.Began;
        }
        return false;
    }
}