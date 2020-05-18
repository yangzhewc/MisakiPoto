using UnityEngine;
using TouchStateManager;

public class TouchEffect : MonoBehaviour
{
    [SerializeField] GameObject CLICK_PARTICLE = default; // PS_TouchStarを割り当てる
    [SerializeField] GameObject DRAG_PARTICLE = default;  // PS_DragStarを割り当てる

    private GameObject m_ClickParticle;
    private GameObject m_DragParticle;
    private ParticleSystem m_ClickParticleSystem;
    private ParticleSystem m_DragParticleSystem;

    private StateManager m_TouchManager;

    private bool DragFlag;     // ドラッグしはじめのときにtrueにする（連続でParticle.Play()させないため）

    // Use this for initialization
    void Start()
    {
        // フラグの初期化
        DragFlag = false;

        // タッチ管理マネージャ生成
        this.m_TouchManager = new StateManager();

        // パーティクルを生成
        m_ClickParticle = (GameObject)Instantiate(CLICK_PARTICLE);
        m_DragParticle = (GameObject)Instantiate(DRAG_PARTICLE);

        // パーティクルの再生停止を制御するためコンポーネントを取得
        m_ClickParticleSystem = m_ClickParticle.GetComponent<ParticleSystem>();
        m_DragParticleSystem = m_DragParticle.GetComponent<ParticleSystem>();
        m_ClickParticleSystem.Stop();
        m_DragParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // タッチ状態更新
        this.m_TouchManager.update();

        // タッチ状態の取得
        StateManager TouchState = this.m_TouchManager.GetTouch();

        // パーティクルをマウスカーソルに追従させる
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 30.0f;  // Canvasより手前に移動させる
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        m_DragParticle.transform.position = mousePosition;


        //タッチされていたら
        if (TouchState.IsTouch)
        {
            // タッチ開始時
            if (TouchState.Phase == TouchPhase.Began)
            {
                Debug.Log("★を出すよ");
                m_ClickParticle.transform.position = mousePosition;
                m_ClickParticleSystem.Play();   // １回再生(ParticleSystemのLoopingにチェックを入れていないため)
            }
            // タッチ終了
            else if (TouchState.Phase == TouchPhase.Ended)
            {
                Debug.Log("タッチエフェクト停止");
                // Particleの放出を停止する
                m_ClickParticleSystem.Stop();
                m_DragParticleSystem.Stop();

                DragFlag = false;
            }
            // タッチ中
            else if(TouchState.Phase == TouchPhase.Moved)
            {
                if (!DragFlag)
                {
                    Debug.Log("キラキラを出すよ");
                    m_DragParticleSystem.Play();    // ループ再生(Loopingにチェックが入っている)

                    DragFlag = true;
                }

            }
        }

    }
}