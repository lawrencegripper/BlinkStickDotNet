﻿using BlinkStickDotNet;
using BlinkStickDotNet.Animations;
using BlinkStickDotNet.Animations.Implementations;
using BlinkStickDotNet.Animations.Processors;
using System;
using System.Drawing;

namespace BlinkstickDotNet.Examples.Animation
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var colors = new Color[] { Color.Red, Color.Black, Color.Green, Color.Black, Color.Blue, Color.Black, Color.White, Color.Black };
            var processor = new ColorProcessor(BlinkStick.FindFirst(), 8);
            processor.ProcessColors(colors);

            var processor2 = new ColorProcessor(BlinkStick.FindFirst(), 8);
            var colors2 = processor2.GetCurrentColors();


            var blue = Color.Blue.Darken(0.6);
            var orange = Color.FromArgb(255, 75, 0);
            var green = Color.FromArgb(0, 70, 0);
            var darkgreen = Color.Green.Darken(0.5);

            var stick = BlinkStick.FindFirst();
            if (stick.OpenDevice())
            {
                var queue = new Animator();

                queue.Chase(1000, Color.Purple.PadBlack(8));
                queue.Repeat(3);
                queue.ChaseDimmer(2000, 2, Color.Purple.PadBlack(8));
                queue.Morph(1000, Color.Black);

                queue.Morph(1, Color.Black);
                queue.Morph(1500, darkgreen);
                queue.Wait(2500);
                queue.Morph(1500, Color.Black);
                queue.Wait(2500);
                queue.RepeatAll();

                queue.Queue(new Feedback("Morph to red 5s"));
                queue.Morph(5000, Color.Red);

                queue.Queue(new Feedback("Morph to green 5s"));
                queue.Morph(5000, Color.Green);

                queue.Queue(new Feedback("Morph to blue 5s"));
                queue.Morph(5000, Color.Blue);

                queue.Queue(new Feedback("Morph to red 5s"));
                queue.Morph(5000, Color.Red);
                
                queue.Queue(new Feedback("Pulse red 1s"));
                queue.Pulse(1000, Color.Red);

                queue.Queue(new Feedback("Pulse green 1s"));
                queue.Pulse(1000, green);

                queue.Queue(new Feedback("Pulse blue 1s"));
                queue.Pulse(1000, Color.Blue);

                queue.Queue(new Feedback("Chase 1 orange led for 1000 for 8 times"));
                queue.Chase(1000, orange.PadBlack(8));
                queue.Repeat(8);

                queue.Queue(new Feedback("Off for 2000"));
                queue.Off(2000);

                queue.Queue(new Feedback("Halve green for 2000"));
                queue.Color(2000, green.PadBlack(2));

                queue.Queue(new Feedback("Full green for 2000"));
                queue.Color(2000, green);

                queue.Queue(new Feedback("Ready!"));
                queue.Loop();

                queue.Connect(stick, 8);
                queue.Start();

                try
                {
                    Console.ReadLine();
                }
                finally
                {
                    queue.Stop(true);
                }
            }
        }
    }
}