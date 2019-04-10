using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 克里夫移动控制
    /// </summary>
    public class CliffMovement : Movement
    {
        /// <summary>
        /// 是否正在跟随
        /// </summary>
        public bool IsFollowing = false;

        /// <summary>
        /// 跟随目标
        /// </summary>
        public RebanaMovement FollowTarget;

        protected override void Start()
        {
            base.Start();
            FollowTarget = Rebana.GetPlayer().GetComponent<RebanaMovement>();
        }

        protected override void Move()
        {
            base.Move();

            // 是否正在跟随
            if (IsFollowing)
            {
                Follow();
            }
        }

        /// <summary>
        /// 跟随目标
        /// </summary>
        private void Follow()
        {
            var distance = Vector2.Distance(transform.position, TargetPos);

            if (Inputting() && distance < 0.01f)
            {
                if (Vector2.Distance(TargetPos, LastTargetPos) >= 1f)
                {
                    LastTargetPos = TargetPos;
                }

                if (Vector2.Distance(TargetPos, FollowTarget.LastTargetPos) >= 1f)
                {
                    TargetPos = FollowTarget.LastTargetPos;
                    var h = TargetPos.x - transform.position.x;
                    var v = TargetPos.y - transform.position.y;
                    if (h > 0)
                    {
                        Direction = DirectionType.Right;
                    }
                    else if (h < 0)
                    {
                        Direction = DirectionType.Left;
                    }
                    else if (v > 0)
                    {
                        Direction = DirectionType.Up;
                    }
                    else if (v < 0)
                    {
                        Direction = DirectionType.Down;
                    }
                }
            }

            if (distance > 0.01f && Vector2.Distance(TargetPos, LastTargetPos) >= 1f)
            {
                PlayRun();
                m_Rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime));
            }
            else
            {
                PlayIdle();
            }
        }
    }
}