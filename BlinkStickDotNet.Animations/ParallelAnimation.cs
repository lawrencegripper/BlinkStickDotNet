﻿using BlinkStickDotNet.Animations.Processors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkStickDotNet.Animations
{
    /// <summary>
    /// This animation queue will run its animations parallel.
    /// </summary>
    public class ParallelAnimation : AnimatorBase, IAnimation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelAnimation"/> class.
        /// </summary>
        public ParallelAnimation(IAnimationQueue owner = null): base(owner)
        {
        }
        
        /// <summary>
        /// Starts the animation.
        /// </summary>
        /// <param name="processor">The processor.</param>
        public void Start(ILedProcessor processor)
        {
            var actions = Animations
                .Select(a => new Action(() => a.Start(processor)))
                .ToArray();

            Parallel.Invoke(actions);
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        /// <param name="processor">The processor.</param>
        public void Start(IColorProcessor processor)
        {
            Start(new LedProcessor(processor));
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// The clone.
        /// </returns>
        public IAnimation Clone()
        {
            var animation = new ParallelAnimation(Owner);
            animation.Animations.AddRange(Animations);
            return animation;
        }
    }
}