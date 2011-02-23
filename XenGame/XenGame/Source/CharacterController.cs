using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jitter.Dynamics.Constraints;
using Jitter.Dynamics;
using Jitter;
using Jitter.LinearMath;

namespace GameClient
{
    public class CharacterController : Constraint
    {
        private const float JumpVelocity = 0.5f;

        private float feetPosition;

        public CharacterController(World world, RigidBody body)
            : base(body, null)
        {
            this.World = world;

            // determine the position of the feets of the character
            // this can be done by supportmapping in the down direction.
            // (furthest point away in the down direction)
            JVector vec = JVector.Down;
            JVector result = JVector.Zero;

            // Note: the following works just for normal shapes, for multishapes (compound for example)
            // you have to loop through all sub-shapes -> easy.
            body.Shape.SupportMapping(ref vec, out result);

            // feet position is now the distance between body.Position and the feets
            // Note: the following '*' is the dot product.
            feetPosition = result * JVector.Down;
        }

        public World World { private set; get; }
        public JVector TargetVelocity { get; set; }
        public bool TryJump { get; set; }
        public RigidBody BodyWalkingOn { get; set; }

        public override void AddToDebugDrawList(List<JVector> lineList, List<JVector> pointList)
        {
            // nothing to debug draw
        }

        private JVector deltaVelocity = JVector.Zero;
        private bool shouldIJump = false;

        public override void PrepareForIteration(float timestep)
        {
            // send a ray from our feet position down.
            // if we collide with something which is 0.05f units below our feets remember this!

            RigidBody resultingBody = null;
            JVector normal; float frac;

            bool result = World.CollisionSystem.Raycast(Body1.Position + JVector.Down * (feetPosition - 0.1f), JVector.Down, RaycastCallback,
                out resultingBody, out normal, out frac);

            BodyWalkingOn = (result && frac <= 0.2f) ? resultingBody : null;
            shouldIJump = (result && frac <= 0.2f && Body1.LinearVelocity.Y < JumpVelocity && TryJump);
        }

        private bool RaycastCallback(RigidBody body, JVector normal, float fraction)
        {
            // prevent the ray to collide with ourself!
            return (body != this.Body1);
        }

        public override void Iterate()
        {
            deltaVelocity = TargetVelocity - Body1.LinearVelocity;
            deltaVelocity.Y = 0.0f;

            // determine how 'stiff' the character follows the target velocity
            deltaVelocity *= 0.005f;

            if (deltaVelocity.LengthSquared() != 0.0f)
            {
                // activate it, in case it fall asleep :)
                Body1.IsActive = true;
                Body1.ApplyImpulse(deltaVelocity * Body1.Mass);
            }

            if (shouldIJump)
            {
                Body1.IsActive = true;
                Body1.ApplyImpulse(JumpVelocity * JVector.Up * Body1.Mass);
                System.Diagnostics.Debug.WriteLine("JUMP! " + DateTime.Now.Second.ToString());

                if (!BodyWalkingOn.IsStatic)
                {
                    BodyWalkingOn.IsActive = true;
                    // apply the negative impulse to the other body
                    BodyWalkingOn.ApplyImpulse(-1.0f * JumpVelocity * JVector.Up * Body1.Mass);
                }

            }
        }
    }
}

