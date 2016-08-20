﻿using BlinkStickDotNet.Animations;
using BlinkStickDotNet.Animations.Implementations;
using BlinkStickDotNet.Animations.Processors;
using BlinkStickDotNet.Usb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Threading;

namespace BlinkStickDotNet.IntegrationTests
{
    [TestClass]
    public class AnimationQueueIntegrationTests
    {
        [TestMethod]
        public void AnimationQueue_Morph()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            stick.IgnoreDisconnectErrors = true;

            var finished = new ManualResetEvent(false);

            var q = new Animator();
            q.Color(Color.Red);
            q.Morph(4000, Color.Blue);
            q.Morph(4000, Color.Green);
            q.Morph(4000, Color.Red);
            q.Queue(new ActionAnimation((p) =>
            {
                finished.Set();
            }));

            q.Connect(stick, 8);
            q.Start();

            finished.WaitOne();

            q.Stop(true);
            stick.TurnOff();
        }

        [TestMethod]
        public void AnimationQueue_Pulse()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            stick.IgnoreDisconnectErrors = true;

            var finished = new ManualResetEvent(false);

            var q = new Animator();
            q.Pulse(2000, Color.Blue);
            q.Pulse(2000, Color.Green);
            q.Pulse(2000, Color.Red);
            q.Queue(new ActionAnimation((p) =>
            {
                finished.Set();
            }));

            q.Connect(stick);
            q.Start();

            finished.WaitOne();
            stick.TurnOff();
        }

        [TestMethod]
        public void AnimationQueue_PulseInverted()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            stick.IgnoreDisconnectErrors = true;

            var finished = new ManualResetEvent(false);

            var q = new Animator();
            q.PulseInverted(2000, Color.Blue);
            q.PulseInverted(2000, Color.Green);
            q.PulseInverted(2000, Color.Red);
            q.Queue(new ActionAnimation((p) =>
            {
                finished.Set();
            }));

            q.Connect(stick);
            q.Start();

            finished.WaitOne();
            stick.TurnOff();
        }

        [TestMethod]
        public void AnimationQueue_Dim()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            stick.IgnoreDisconnectErrors = true;

            var finished = new ManualResetEvent(false);

            var q = new Animator();
            q.Color(Color.Red);
            q.Dim(2000);
            q.Color(Color.Green);
            q.Dim(2000);
            q.Color(Color.Blue);
            q.Dim(2000);
            q.Queue(new ActionAnimation((p) =>
            {
                finished.Set();
            }));

            q.Connect(stick);
            q.Start();

            finished.WaitOne();
            stick.TurnOff();
        }

        [TestMethod]
        public void AnimationQueue_Color()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            var finished = new ManualResetEvent(false);

            var q = new Animator();
            q.Color(1000, Color.Red);
            q.Chase(1000, Color.Green);
            q.Chase(1000, Color.Blue);
            q.Queue(new ActionAnimation((p) =>
            {
                finished.Set();
            }));

            q.Connect(stick);
            q.Start();

            finished.WaitOne();
            stick.TurnOff();

        }

        public void AnimationQueue_ChaseMorp()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            stick.IgnoreDisconnectErrors = true;

            var finished = new ManualResetEvent(false);



            var q = new Animator();
            q.Chase(1000, Color.Blue.PadBlack(8));
            q.Repeat(2);
            q.Chase(1000, -1, Color.Blue.PadBlack(8));
            q.Repeat(2);
            q.Queue(new ActionAnimation((p) =>
            {
                finished.Set();
            }));
        }

        [TestMethod]
        public void AnimationQueue_ChainedMorphChaser()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();

            var finished = new ManualResetEvent(false);

            using (var queue = new Animator(stick, 8))
            {
                queue
                    .Off()
                    .Color(Color.Red.PadBlack(8))
                    .Wait(200)
                    .BeginParallel()
                        .BeginSequencial()
                            .Chase(6000, 1, 6)
                            .Chase(6000, -1, 6)
                            .End()
                        .BeginSequencial()
                            .Morph(3000, Color.Blue.PadBlack(8))
                            .Morph(3000, Color.Green.PadBlack(8))
                            .Morph(3000, Color.Red.PadBlack(8))
                            .End()
                        .End()
                    .Off()
                    .Pulse(1000, Color.Blue)
                    .Repeat()
                    .Off()
                    .Set(finished);

                queue.Start();
                finished.WaitOne();
            }
        }

        [TestMethod]
        public void AnimationQueue_Chase()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            stick.IgnoreDisconnectErrors = true;

            var finished = new ManualResetEvent(false);

            var q = new Animator();
            q.Chase(1000, Color.Blue.PadBlack(8));
            q.Repeat(2);
            q.Chase(1000, -1, Color.Blue.PadBlack(8));
            q.Repeat(2);
            q.Queue(new ActionAnimation((p) =>
            {
                finished.Set();
            }));

            q.Connect(stick, 8);
            q.Start();

            finished.WaitOne();
            stick.TurnOff();
        }

        [TestMethod]
        public void AnimationQueue_ChaseDimmer()
        {
            var stick = BlinkStickIntegrationTests.EnsureBlinkStick();
            stick.IgnoreDisconnectErrors = true;

            var finished = new ManualResetEvent(false);

            var q = new Animator(stick, 8);
            q.ChaseDimmer(2500, 5, Color.Blue.PadBlack(8));
            q.Repeat();

            q.Connect(stick, 8);
            q.Start();

            finished.WaitOne();
            stick.TurnOff();
        }
    }
}