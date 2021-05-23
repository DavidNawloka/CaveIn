using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astutos.CCFPS
{
    public class HeadBob : MonoBehaviour
    {
        [SerializeField] CCFPSController FPSController;
        [SerializeField] float bobIntervallWalk = .5f;
        [SerializeField] float bobIntervallSprint = .5f;
        [SerializeField] float bobAmount = .5f;

        bool doBob = false;
        AnimationCurve walkBobCurve = new AnimationCurve();
        AnimationCurve sprintBobCurve = new AnimationCurve();
        AnimationCurve curveToUse = new AnimationCurve();
        private void Start()
        {
            CreateWalkCurve();
            CreateSprintCurve();
            curveToUse = walkBobCurve;
            StartCoroutine(Bob());
        }

        private void CreateWalkCurve()
        {
            walkBobCurve.AddKey(0, 0);
            walkBobCurve.AddKey(bobIntervallWalk / 2, 1);
            walkBobCurve.AddKey(bobIntervallWalk, 0);
        }
        private void CreateSprintCurve()
        {
            sprintBobCurve.AddKey(0, 0);
            sprintBobCurve.AddKey(bobIntervallSprint / 2, 1);
            sprintBobCurve.AddKey(bobIntervallSprint, 0);
        }

        private void Update()
        {
            if (FPSController == null) return;
            if (FPSController.GetIsGrounded() && FPSController.GetVelocity() > FPSController.sprintSpeed - 1f)
            {
                curveToUse = sprintBobCurve;
                doBob = true;
            }
            else if (FPSController.GetIsGrounded() && FPSController.GetVelocity() > 0)
            {
                curveToUse = walkBobCurve;
                doBob = true;
            }


        }

        IEnumerator Bob()
        {
            float lastY = Camera.main.transform.localPosition.y;
            float timer = 0f;
            while (true)
            {
                if (doBob)
                {
                    Camera.main.transform.localPosition = new Vector3(
                                    Camera.main.transform.localPosition.x,
                                    lastY + walkBobCurve.Evaluate(timer) * bobAmount,
                                    Camera.main.transform.localPosition.z);
                    if (timer >= bobIntervallWalk)
                    {
                        timer = 0f;
                        doBob = false;
                    }
                    timer += Time.deltaTime;
                    yield return null;
                }
                yield return null;

            }
        }
    }

}
