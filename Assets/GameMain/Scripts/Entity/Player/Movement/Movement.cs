using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 移动控制基类
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class Movement : MonoBehaviour
    {
        /// <summary>
        /// 角色朝向
        /// </summary>
        public DirectionType Direction = DirectionType.Down;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float MoveSpeed = 3f;

        /// <summary>
        /// 将要移动的目标位置
        /// </summary>
        public Vector2 TargetPos = Vector2.one;

        /// <summary>
        /// 上一次的移动目标（作为跟随坐标）
        /// </summary>
        public Vector2 LastTargetPos = Vector2.one;

        /// <summary>
        /// 动画控制器
        /// </summary>
        protected Animator m_Animator;

        /// <summary>
        /// 刚体组件
        /// </summary>
        protected Rigidbody2D m_Rigidbody2D;

        /// <summary>
        /// 计时器
        /// </summary>
        protected float m_Timer = 0;

        protected virtual void Start()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// 设置角色出生位置
        /// </summary>
        /// <param name="position"></param>
        public virtual void Born(Vector3 position)
        {
            transform.position = position;
            TargetPos = transform.position;
            LastTargetPos = TargetPos;
        }

        /// <summary>
        /// 角色是否正在移动中
        /// </summary>
        /// <returns></returns>
        public virtual bool Moving()
        {
            return Vector2.Distance(transform.position, TargetPos) > 0.01f;
        }

        /// <summary>
        /// 虚拟摇杆
        /// </summary>
        private UIjoystick m_UIjoystick;

        private GameObject m_JoystickGo;

        /// <summary>
        /// 获取输入的x轴
        /// </summary>
        /// <returns></returns>
        protected float GetXAxis()
        {
            if (m_JoystickGo == null)
            {
                m_JoystickGo = GameObject.FindGameObjectWithTag("Joystick");
            }

            if (m_JoystickGo != null && m_UIjoystick == null)
            {
                m_UIjoystick = m_JoystickGo.GetComponent<UIjoystick>();
            }

            if (m_UIjoystick != null)
            {
                return m_UIjoystick.Horizontal;
            }

            return 0;
        }

        /// <summary>
        /// 获取输入的y轴
        /// </summary>
        /// <returns></returns>
        protected float GetYAxis()
        {
            if (m_JoystickGo == null)
            {
                m_JoystickGo = GameObject.FindGameObjectWithTag("Joystick");
            }

            if (m_JoystickGo != null && m_UIjoystick == null)
            {
                m_UIjoystick = m_JoystickGo.GetComponent<UIjoystick>();
            }

            if (m_UIjoystick != null)
            {
                return m_UIjoystick.Vertical;
            }

            return 0;
        }

        /// <summary>
        /// 是否正在输入
        /// </summary>
        /// <returns></returns>
        protected bool Inputting()
        {
            return Math.Abs(GetXAxis()) > 0 || Math.Abs(GetYAxis()) > 0;
        }

        /// <summary>
        /// 移动控制
        /// </summary>
        protected virtual void Move()
        {
            
        }

        #region 计算移动坐标

        /// <summary>
        /// 计算8方向
        /// </summary>
        /// <param name="h"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        protected Vector2 Calculate8Direction(float h, float v)
        {
            if (Math.Abs(v) < 0.01f && Math.Abs(h) < 0.01f)
            {
                return new Vector2(0, 0);
            }

            var rad = Math.Atan2(v, h);
            if (rad >= -Math.PI / 8 && rad < 0 || rad >= 0 && rad < Math.PI / 8)
            {
                Direction = DirectionType.Right;
                return new Vector2(1, 0); // 右
            }
            else if (rad >= Math.PI / 8 && rad < 3 * Math.PI / 8)
            {
                Direction = DirectionType.RightUp;
                return new Vector2(1, 1); // 右上
            }
            else if (rad >= 3 * Math.PI / 8 && rad < 5 * Math.PI / 8)
            {
                Direction = DirectionType.Up;
                return new Vector2(0, 1); // 上
            }
            else if (rad >= 5 * Math.PI / 8 && rad < 7 * Math.PI / 8)
            {
                Direction = DirectionType.LeftUp;
                return new Vector2(-1, 1); // 左上
            }
            else if (rad >= 7 * Math.PI / 8 && rad < Math.PI || rad >= -Math.PI && rad < -7 * Math.PI / 8)
            {
                Direction = DirectionType.Left;
                return new Vector2(-1, 0); // 左
            }
            else if (rad >= -7 * Math.PI / 8 && rad < -5 * Math.PI / 8)
            {
                Direction = DirectionType.LeftDown;
                return new Vector2(-1, -1); // 左下
            }
            else if (rad >= -5 * Math.PI / 8 && rad < -3 * Math.PI / 8)
            {
                Direction = DirectionType.Down;
                return new Vector2(0, -1); // 下
            }
            else
            {
                Direction = DirectionType.RightDown;
                return new Vector2(1, -1); // 右下
            }
        }

        /// <summary>
        /// 计算4方向
        /// </summary>
        /// <param name="h"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        protected Vector2 Calculate4Direction(float h, float v)
        {
            if (Math.Abs(v) < 0.01f && Math.Abs(h) < 0.01f)
            {
                return new Vector2(0, 0);
            }

            var rad = Math.Atan2(v, h);
            if (rad >= -2 * Math.PI / 8 && rad < 0 || rad >= 0 && rad < 2 * Math.PI / 8)
            {
                Direction = DirectionType.Right;
                return new Vector2(1, 0); // 右
            }
            else if (rad >= 2 * Math.PI / 8 && rad < 6 * Math.PI / 8)
            {
                Direction = DirectionType.Up;
                return new Vector2(0, 1); // 上
            }
            else if (rad >= 6 * Math.PI / 8 && rad < Math.PI || rad >= -Math.PI && rad < -6 * Math.PI / 8)
            {
                Direction = DirectionType.Left;
                return new Vector2(-1, 0); // 左
            }
            else
            {
                Direction = DirectionType.Down;
                return new Vector2(0, -1); // 下
            }
        }

        #endregion

        #region 播放动画

        /// <summary>
        /// 播放移动动画
        /// </summary>
        protected void PlayRun()
        {
            if (Direction == DirectionType.Right && !PlayingOrTransAnima("WalkingRight"))
            {
                m_Animator.SetTrigger("WalkingRight");
            }
            else if (Direction == DirectionType.Left && !PlayingOrTransAnima("WalkingLeft"))
            {
                m_Animator.SetTrigger("WalkingLeft");
            }
            else if (Direction == DirectionType.Up && !PlayingOrTransAnima("WalkingUp"))
            {
                m_Animator.SetTrigger("WalkingUp");
            }
            else if (Direction == DirectionType.Down && !PlayingOrTransAnima("WalkingDown"))
            {
                m_Animator.SetTrigger("WalkingDown");
            }
        }

        /// <summary>
        /// 角色闲置移动
        /// </summary>
        protected void PlayIdle()
        {
            if (Direction == DirectionType.Right && !PlayingOrTransAnima("IdleRight"))
            {
                m_Animator.SetTrigger("IdleRight");
            }
            else if (Direction == DirectionType.Left && !PlayingOrTransAnima("IdleLeft"))
            {
                m_Animator.SetTrigger("IdleLeft");
            }
            else if (Direction == DirectionType.Up && !PlayingOrTransAnima("IdleUp"))
            {
                m_Animator.SetTrigger("IdleUp");
            }
            else if (Direction == DirectionType.Down && !PlayingOrTransAnima("IdleDown"))
            {
                m_Animator.SetTrigger("IdleDown");
            }
        }

        /// <summary>
        /// 指定动画是否正在播放或者即将播放
        /// </summary>
        /// <param name="animName"></param>
        /// <returns></returns>
        protected bool PlayingOrTransAnima(string animName)
        {
            var stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            var transInfo = m_Animator.GetAnimatorTransitionInfo(0);
            return stateInfo.IsName(animName) || transInfo.IsName(animName);
        }

        #endregion
    }
}