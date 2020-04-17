using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchStateManager
{
    public class StateManager
    {
        public bool       IsTouch;                    // タッチしているかどうかのフラグ
        public Vector2    TouchPos;                   // タッチしている座標
        public TouchPhase Phase;                      // タッチの状態（開始、最中、終了）

        //==============================
        // 初期化処理（コンストラクタ）
        //==============================
        // @param bool flag タッチ有無
        // @param Vector2 position タッチ座標(引数の省略が行えるようにNull許容型に)
        // @param Touchphase phase タッチ状態
        public StateManager(bool flag = false, Vector2 ? position = null, TouchPhase phase = TouchPhase.Began)
        {
            this.IsTouch = flag;

            if (position == null)
            {
                this.TouchPos = new Vector2(0, 0);
            }
            else
            {
                this.TouchPos = (Vector2)position;
            }
            this.Phase = phase;
        }

        //=========================
        // 更新処理
        //=========================
        public void update()
        {
            this.IsTouch = false;

            // マウス操作（エディタ上）
            if (Application.isEditor)
            {
                // 押した瞬間
                if (Input.GetMouseButtonDown(0))
                {                    
                    this.IsTouch = true;
                    this.Phase = TouchPhase.Began;
                    Debug.Log("押した瞬間");
                }

                // 離した瞬間
                if (Input.GetMouseButtonUp(0))
                {
                    this.IsTouch = true;
                    this.Phase = TouchPhase.Ended;
                    Debug.Log("離した瞬間");
                }

                // 押しっぱなし
                if (Input.GetMouseButton(0))
                {
                    this.IsTouch = true;
                    this.Phase = TouchPhase.Moved;
                    Debug.Log("押しっぱなし");
                }

                // 座標取得
                if (this.IsTouch)
                {
                    this.TouchPos = Input.mousePosition;
                }

            }
            // 実機（スマホ）使用時
            else
            {
                if (Input.touchCount > 0)
                {
                    UnityEngine.Touch touch = Input.GetTouch(0);

                    this.TouchPos = touch.position;
                    this.Phase = touch.phase;
                    this.IsTouch = true;
                }
            }
        }

        //=========================
        // タッチ情報の取得
        //=========================
        public StateManager GetTouch()
        {
            return new StateManager(this.IsTouch, this.TouchPos, this.Phase);
        }
    }
}

