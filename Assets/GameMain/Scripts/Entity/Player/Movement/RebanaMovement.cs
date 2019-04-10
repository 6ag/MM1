using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 雷班纳移动控制
    /// </summary>
    public class RebanaMovement : Movement
    {
        /// <summary>
        /// 移动控制
        /// </summary>
        protected override void Move()
        {
            // 目标位置和当前位置的间距
            var distance = Vector2.Distance(transform.position, TargetPos);

            m_Timer += Time.deltaTime;
            // 是否能够移动
            if (m_Timer > MoveSpeed * Time.deltaTime && Inputting() && distance < 0.01f)
            {
                m_Timer = 0f;

                var h = GetXAxis();
                var v = GetYAxis();
                // 计算4方向
                var v2 = Calculate4Direction(h, v);
                h = v2.x;
                v = v2.y;

                // 计算下一个移动目标点位置
                Vector2 targetPos = transform.position + new Vector3(h, v);
                var hit = Physics2D.Linecast(targetPos, targetPos);
                if (hit.collider == null || hit.collider.CompareTag("Player") || hit.collider.CompareTag("Teammate")) // 没有碰撞体，或者是队友随便走
                {
                    if (Vector2.Distance(TargetPos, LastTargetPos) >= 1f)
                    {
                        LastTargetPos = TargetPos;
                    }

                    TargetPos = targetPos;
                }
                else if (hit.collider.CompareTag("VehicleBarrier")) // 检测到前方存在战车障碍物
                {
                    if (Vector2.Distance(TargetPos, LastTargetPos) >= 1f)
                    {
                        LastTargetPos = TargetPos;
                    }

                    // 判断当前队伍是否有乘坐战车，有则不能前进
                    TargetPos = targetPos;
                }
                else if (hit.collider.CompareTag("NormalBarrier")) // 检测到前方存在默认通用障碍物
                {
                    PlayIdle();
                }
                else if (hit.collider.isTrigger)
                {
                    if (Vector2.Distance(TargetPos, LastTargetPos) >= 1f)
                    {
                        LastTargetPos = TargetPos;
                    }

                    TargetPos = targetPos;
                }
            }

            //是否到达目的地
            if (distance > 0.01f)
            {
                PlayRun();
                // 移动角色
                m_Rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime));
            }
            else if (!Inputting()) // 没有按下方向键并且已经到目的地
            {
                PlayIdle();
            }
        }
    }
}