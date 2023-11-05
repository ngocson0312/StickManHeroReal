using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SuperFight
{
    public class CreeperChaseState : State
    {
        private Creeper enemy;
        private int currentDirection;
        public CreeperChaseState(Creeper controller, string stateName) : base(controller, stateName)
        {
            this.enemy = controller;
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {

        }

        public override void UpdateLogic()
        {

        }


        public override void UpdatePhysic()
        {
            Controller target = enemy.GetTargetInView();
            if (controller.core.collisionSenses.IsOnGround())
            {
                if (!controller.core.collisionSenses.GroundAhead() || controller.core.collisionSenses.IsTouchWall())
                {

                    enemy.core.movement.Flip();
                    controller.core.movement.SetVelocityX(0);
                    return;
                }
            }
            if (target == null)
            {
                enemy.SwitchState(enemy.creeperPatrol);
            }
            else
            {
                Vector3 direction = (target.transform.position - controller.transform.position);
                if (direction.x < -0.1f)
                {
                    currentDirection = -1;
                }
                else if (direction.x > 0.1f)
                {
                    currentDirection = 1;
                }

                if (currentDirection != controller.core.movement.facingDirection && !controller.isInteracting)
                {
                    controller.core.movement.Flip();
                }

                controller.core.movement.SetVelocityX(controller.core.movement.facingDirection * enemy.speed * 2);


                if (Vector2.SqrMagnitude(target.transform.position - enemy.transform.position) <= enemy.attackRange)
                {
                    enemy.SwitchState(enemy.creeperExplodeState);
                }
            }
        }

    }
}
